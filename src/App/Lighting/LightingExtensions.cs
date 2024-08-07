// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.App.Lighting.Services;
using ChromaControl.Common.Extensions;
using ChromaControl.Common.Protos.Lighting;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChromaControl.App.Lighting;

/// <summary>
/// Lighting extension methods.
/// </summary>
public static class LightingExtensions
{
    /// <summary>
    /// Adds lighting configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureLighting(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.AddChromaControlGrpcClient<LightingGrpc.LightingGrpcClient>();
        builder.Services.TryAddSingleton<LightingService>();
        builder.Services.AddHostedService<NotificationMonitor>();

        return builder;
    }
}
