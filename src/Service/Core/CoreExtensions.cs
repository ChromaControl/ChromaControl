// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace ChromaControl.Service.Core;

/// <summary>
/// Core extension methods.
/// </summary>
public static class CoreExtensions
{
    /// <summary>
    /// Adds core configuration to a <see cref="IHostApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> to continue adding configuration to.</returns>
    public static IHostApplicationBuilder ConfigureCore(this IHostApplicationBuilder builder)
    {
        builder.ConfigureChromaControl()
            .ConfigureTelemetry()
            .ConfigureGrpc()
            .ConfigureUnixSocket();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureChromaControl(this IHostApplicationBuilder builder)
    {
        builder.Configuration.AddChromaControlConfiguration();
        builder.Services.AddChromaControlServices();
        builder.Logging.AddChromaControlLogging();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
            });

        if (!string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            builder.Services.AddOpenTelemetry()
                .UseOtlpExporter();
        }

        return builder;
    }

    private static IHostApplicationBuilder ConfigureGrpc(this IHostApplicationBuilder builder)
    {
        builder.Services.AddGrpc();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureUnixSocket(this IHostApplicationBuilder builder)
    {
        if (builder is not WebApplicationBuilder webAppBuilder)
        {
            throw new ArgumentException("Unix sockets can only be configured on a WebApplicationBuilder.");
        }

        var socketPath = builder.Configuration.GetChromaControlPath("socket");

        webAppBuilder.WebHost.ConfigureKestrel(options =>
        {
            if (File.Exists(socketPath))
            {
                File.Delete(socketPath);
            }

            options.ListenUnixSocket(socketPath, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        return builder;
    }
}
