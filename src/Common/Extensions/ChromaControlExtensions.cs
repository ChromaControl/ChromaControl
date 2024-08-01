// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Keys;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NReco.Logging.File;
using System.Net.Sockets;
using System.Reflection;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// Chroma Control extension methods.
/// </summary>
public static class ChromaControlExtensions
{
    private readonly static string s_appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly static string s_dataPath = Path.Combine(s_appDataPath, "ChromaControl");
    private readonly static string s_environmentPath = Path.Combine(s_dataPath, "environment");
    private readonly static string s_logsPath = Path.Combine(s_environmentPath, "logs");
    private readonly static string s_runtimePath = Path.Combine(s_dataPath, "runtime");
    private readonly static string s_socketPath = Path.Combine(s_runtimePath, "ChromaControl.sock");
    private readonly static string s_databasePath = Path.Combine(s_environmentPath, "ChromaControl.db");

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

        logging.AddFile(logFilePath, true);

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

        if (!Directory.Exists(s_environmentPath))
        {
            Directory.CreateDirectory(s_environmentPath);
        }

        if (!Directory.Exists(s_logsPath))
        {
            Directory.CreateDirectory(s_logsPath);
        }

        if (!Directory.Exists(s_runtimePath))
        {
            Directory.CreateDirectory(s_runtimePath);
        }

        var paths = config.GetSection("ChromaControl").GetSection("Path");
        var connectionStrings = config.GetSection("ConnectionStrings");

        paths["DATA"] = s_dataPath;
        paths["ENVIRONMENT"] = s_environmentPath;
        paths["LOGS"] = s_logsPath;
        paths["RUNTIME"] = s_runtimePath;
        paths["SOCKET"] = s_socketPath;

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
    /// 
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddChromaControlGrpcClient<TClient>(this IServiceCollection services) where TClient : ClientBase
    {
        services.AddGrpcClient<TClient>(options =>
        {
            options.Address = new Uri("http://localhost");
            options.ChannelOptionsActions.Add(channel =>
            {
                var endPoint = new UnixDomainSocketEndPoint(s_socketPath);

                channel.HttpHandler = new SocketsHttpHandler
                {
                    ConnectCallback = async (_, cancellationToken) =>
                    {
                        var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

                        try
                        {
                            await socket.ConnectAsync(endPoint, cancellationToken).ConfigureAwait(false);
                            return new NetworkStream(socket, true);
                        }
                        catch
                        {
                            socket.Dispose();
                            throw;
                        }
                    }
                };
            });
        });

        return services;
    }
}
