// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.Common.Extensions;
using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Settings;

/// <summary>
/// Settings extension methods.
/// </summary>
public static class SettingsExtensions
{
    /// <summary>
    /// Adds settings configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureSettings(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.AddChromaControlGrpcClient<SettingsGrpc.SettingsGrpcClient>();

        return builder;
    }
}
