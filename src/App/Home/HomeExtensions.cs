// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.Common.Extensions;

namespace ChromaControl.App.Home;

/// <summary>
/// Home extension methods.
/// </summary>
public static class HomeExtensions
{
    /// <summary>
    /// Adds home configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureHome(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.AddChromaControlGrpcClient<Common.Protos.Home.HomeClient>();

        return builder;
    }
}
