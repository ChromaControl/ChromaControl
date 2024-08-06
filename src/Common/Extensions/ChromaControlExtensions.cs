// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Services;
using ChromaControl.Keys;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;
using System.Diagnostics;
using System.IO.Pipes;
using System.Reflection;
using System.Security.Principal;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// Chroma Control extension methods.
/// </summary>
public static class ChromaControlExtensions
{
    private readonly static string s_appPath = AppContext.BaseDirectory;
    private readonly static string s_appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly static string s_dataPath = Path.Combine(s_appDataPath, "ChromaControl");
    private readonly static string s_logsPath = Path.Combine(s_dataPath, "logs");
    private readonly static string s_databasePath = Path.Combine(s_dataPath, "ChromaControl.db");

    /// <summary>
    /// Adds Chroma Control services to an <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> to continue adding services to.</returns>
    public static IServiceCollection AddChromaControlServices(this IServiceCollection services)
    {
        var mutex = new Mutex(true, Assembly.GetEntryAssembly()!.GetName().Name, out var success);

        if (success)
        {
            services.AddSingleton(mutex);
        }
        else
        {
            Environment.Exit(1);
        }

        services.AddHostedService<StartupService>();

        return services;
    }

    /// <summary>
    /// Adds Chroma Control logging to an <see cref="ILoggingBuilder"/>.
    /// </summary>
    /// <param name="logging">The <see cref="ILoggingBuilder"/> to add logging to.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> to continue adding logging to.</returns>
    public static ILoggingBuilder AddChromaControlLogging(this ILoggingBuilder logging)
    {
        var logFilePath = Path.Combine(s_logsPath, $"{Assembly.GetEntryAssembly()!.GetName().Name}.log");

        logging.AddFile(logFilePath, config =>
        {
            config.Append = true;
            config.FileSizeLimitBytes = 1024 * 1024 * 64;
            config.MaxRollingFiles = 2;
        });

        return logging;
    }

    /// <summary>
    /// Adds Chroma Control configuration to an <see cref="IConfigurationManager"/>.
    /// </summary>
    /// <param name="config">The <see cref="IConfigurationManager"/> to add configuration to.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> to continue adding configuration to.</returns>
    public static IConfigurationManager AddChromaControlConfiguration(this IConfigurationManager config)
    {
        if (!Directory.Exists(s_dataPath))
        {
            Directory.CreateDirectory(s_dataPath);
        }

        if (!Directory.Exists(s_logsPath))
        {
            Directory.CreateDirectory(s_logsPath);
        }

        var chromaControl = config.GetSection("ChromaControl");
        var paths = chromaControl.GetSection("Path");
        var connectionStrings = config.GetSection("ConnectionStrings");

        var appExecutable = $"{AppDomain.CurrentDomain.FriendlyName}.exe";
        var appPath = Path.Combine(s_appPath, appExecutable);
        var appVersionInfo = FileVersionInfo.GetVersionInfo(appPath);

        chromaControl["VERSION"] = appVersionInfo.ProductVersion;

        paths["APP"] = s_appPath;
        paths["DATA"] = s_dataPath;
        paths["LOGS"] = s_logsPath;

        connectionStrings["Database"] = $"Data Source={s_databasePath}";

        config.AddChromaControlKeys();

        return config;
    }

    /// <summary>
    /// Gets a Chroma Control path from the configuration.
    /// </summary>
    /// <param name="config">The <see cref="IConfigurationManager"/> to get the path from.</param>
    /// <param name="pathName">The name of the path requested.</param>
    /// <returns>The requested path.</returns>
    public static string GetChromaControlPath(this IConfiguration config, string pathName)
    {
        var result = config.GetSection("ChromaControl").GetSection("Path")[pathName.ToUpper()]
            ?? throw new InvalidOperationException($"The requested path '{pathName}` could not be found.");

        return result;
    }

    /// <summary>
    /// Adds a Chroma Control Grpc client.
    /// </summary>
    /// <typeparam name="TClient">The client type to add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The original <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddChromaControlGrpcClient<TClient>(this IServiceCollection services) where TClient : ClientBase
    {
        services.AddGrpcClient<TClient>(options =>
        {
            options.Address = new Uri("http://localhost");
            options.ChannelOptionsActions.Add(channel =>
            {
                channel.HttpHandler = new SocketsHttpHandler
                {
                    ConnectCallback = async (_, cancellationToken) =>
                    {
                        var clientStream = new NamedPipeClientStream(
                            serverName: ".",
                            pipeName: "ChromaControl",
                            direction: PipeDirection.InOut,
                            options: PipeOptions.WriteThrough | PipeOptions.Asynchronous,
                            impersonationLevel: TokenImpersonationLevel.Anonymous);

                        try
                        {
                            await clientStream.ConnectAsync(10, cancellationToken).ConfigureAwait(false);
                            return clientStream;
                        }
                        catch
                        {
                            clientStream.Dispose();
                            throw;
                        }
                    }
                };
            });
        });

        return services;
    }
}
