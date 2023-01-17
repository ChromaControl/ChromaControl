// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using ChromaControl.Common.Keys;
using Grpc.Net.Client;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// Extension methods for Chroma Control.
/// </summary>
public static class ChromaControlExtensions
{
    /// <summary>
    /// Allocates a console window.
    /// </summary>
    [DllImport("kernel32")]
    private static extern void AllocConsole();

    /// <summary>
    /// Adds Chroma Control services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
    /// <returns>The <see cref="IServiceCollection"/> to allow for method chaining.</returns>
    /// <param name="args">The command line arguments.</param>
    public static IServiceCollection AddChromaControl(this IServiceCollection services, string[] args)
    {
        if (!args.Contains("--no-console"))
        {
            AllocConsole();
            Console.Title = ChromaControlConstants.ExecutingAssemblyName;
        }

        if (!Directory.Exists(ChromaControlConstants.DataDirectory))
        {
            Directory.CreateDirectory(ChromaControlConstants.DataDirectory);
        }

        if (!Directory.Exists(ChromaControlConstants.LogDirectory))
        {
            Directory.CreateDirectory(ChromaControlConstants.LogDirectory);
        }

        services.AddLogging(builder =>
        {
            builder.AddFile(Path.Combine(ChromaControlConstants.LogDirectory, $"{ChromaControlConstants.ExecutingAssemblyName}.log"), true);
        });

        var mutex = new AppMutex();

        if (mutex.Success)
        {
            services.AddSingleton(mutex);
        }
        else
        {
            Environment.Exit(0);
        }

        if (!Debugger.IsAttached)
        {
            AppCenter.Start(KeyData.AppCenterKey, typeof(Analytics), typeof(Crashes));
        }

        return services;
    }

    /// <summary>
    /// Adds a chroma control client to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TClient">The client to add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the client to.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddChromaControlClient<TClient>(this IServiceCollection services) where TClient : class
    {
        var channel = GrpcChannel.ForAddress("http://localhost");
        services.AddGrpcClient<TClient>(o =>
        {
            o.Address = new Uri("http://localhost");

            o.ChannelOptionsActions.Add(channel =>
            {
                var udsEndPoint = new UnixDomainSocketEndPoint(ChromaControlConstants.SocketPath);
                var connectionFactory = new UnixDomainSocketConnectionFactory(udsEndPoint);
                var socketsHttpHandler = new SocketsHttpHandler
                {
                    ConnectCallback = connectionFactory.ConnectAsync
                };

                channel.HttpHandler = socketsHttpHandler;
            });
        });

        return services;
    }
}
