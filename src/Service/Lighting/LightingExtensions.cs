// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.SDK.OpenRGB.Extensions;
using ChromaControl.SDK.Synapse.Extensions;
using ChromaControl.Service.Lighting.Services;
using ChromaControl.Service.Lighting.Workers;

namespace ChromaControl.Service.Lighting;

/// <summary>
/// Lighting extension methods.
/// </summary>
public static class LightingExtensions
{
    /// <summary>
    /// Adds lighting configuration to a <see cref="IHostApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> to continue adding configuration to.</returns>
    public static IHostApplicationBuilder ConfigureLighting(this IHostApplicationBuilder builder)
    {
        builder.ConfigureServices()
            .ConfigureSynapse()
            .ConfigureOpenRGB();

        return builder;
    }

    /// <summary>
    /// Adds lighting middleware to a <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to add middleware to.</param>
    /// <returns>The <see cref="WebApplication"/> to continue adding middleware to.</returns>
    public static WebApplication UseLighting(this WebApplication app)
    {
        app.MapGrpcService<LightingService>();

        return app;
    }

    private static IHostApplicationBuilder ConfigureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<EventDispatcher>();
        builder.Services.AddHostedService<LightingWorker>();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureSynapse(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSynapseSDK();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureOpenRGB(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenRGBSDK();

        return builder;
    }
}
