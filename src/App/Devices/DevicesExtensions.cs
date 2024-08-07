// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.Common.Extensions;
using ChromaControl.Common.Protos.Devices;

namespace ChromaControl.App.Devices;

/// <summary>
/// Devices extension methods.
/// </summary>
public static class DevicesExtensions
{
    /// <summary>
    /// Adds devices configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureDevices(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.AddChromaControlGrpcClient<DevicesGrpc.DevicesGrpcClient>();

        return builder;
    }
}
