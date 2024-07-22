// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.Common.Extensions;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace ChromaControl.App.Core;

/// <summary>
/// Core extension methods.
/// </summary>
public static class CoreExtensions
{
    /// <summary>
    /// Adds core configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureCore(this BlazorDesktopHostBuilder builder)
    {
        builder.ConfigureChromaControl()
            .ConfigureTelemetry()
            .ConfigureHttpClient()
            .ConfigureDeveloperTools();

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureChromaControl(this BlazorDesktopHostBuilder builder)
    {
        builder.Configuration.AddChromaControlConfiguration();
        builder.Services.AddChromaControlServices();
        builder.Logging.AddChromaControlLogging();

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureTelemetry(this BlazorDesktopHostBuilder builder)
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
                    .AddHttpClientInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddHttpClientInstrumentation();
            });

        if (!string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]))
        {
            builder.Services.AddOpenTelemetry()
                .UseOtlpExporter();
        }

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureHttpClient(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
        });

        builder.Services.AddHttpClient();

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureDeveloperTools(this BlazorDesktopHostBuilder builder)
    {
        if (builder.HostEnvironment.IsDevelopment())
        {
            builder.UseDeveloperTools();
        }

        return builder;
    }
}
