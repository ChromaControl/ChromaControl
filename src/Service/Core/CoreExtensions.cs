// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;

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
            .ConfigureNamedPipe();

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
                tracing.AddAspNetCoreInstrumentation()
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    options.SetDbStatementForText = true;
                    options.SetDbStatementForStoredProcedure = true;
                });
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

    private static IHostApplicationBuilder ConfigureNamedPipe(this IHostApplicationBuilder builder)
    {
        if (builder is not WebApplicationBuilder webAppBuilder)
        {
            throw new ArgumentException("Named pipes can only be configured on a WebApplicationBuilder.");
        }

        webAppBuilder.WebHost.UseNamedPipes(options =>
        {
            options.CurrentUserOnly = false;
            options.PipeSecurity = new();

            options.PipeSecurity.AddAccessRule(new(
                identity: new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null),
                rights: PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance,
                type: AccessControlType.Allow));
        });

        webAppBuilder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenNamedPipe("ChromaControl", listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        return builder;
    }
}
