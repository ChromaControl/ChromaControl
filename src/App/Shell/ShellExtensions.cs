// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.App.Shell.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ChromaControl.App.Shell;

/// <summary>
/// Shell extension methods.
/// </summary>
public static class ShellExtensions
{
    /// <summary>
    /// Adds shell configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureShell(this BlazorDesktopHostBuilder builder)
    {
        builder.ConfigureWindow()
            .ConfigureRootComponents();

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureWindow(this BlazorDesktopHostBuilder builder)
    {
        builder.Window.UseTitle("Chroma Control");
        builder.Window.UseResizable(true);

        builder.Window.UseFrame(false);

        builder.Window.UseHeight(720);
        builder.Window.UseWidth(1280);

        builder.Window.UseMinHeight(500);
        builder.Window.UseMinWidth(600);

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureRootComponents(this BlazorDesktopHostBuilder builder)
    {
        builder.RootComponents.Add<Routes>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        return builder;
    }
}
