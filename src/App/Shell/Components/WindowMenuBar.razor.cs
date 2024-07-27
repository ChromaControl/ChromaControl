// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Wpf;
using ChromaControl.App.Shell.Services;
using ChromaControl.Common.Extensions;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Windows;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window menu bar component.
/// </summary>
public partial class WindowMenuBar
{
    /// <summary>
    /// The window.
    /// </summary>
    [Inject]
    public required BlazorDesktopWindow Window { get; set; }

    /// <summary>
    /// The configuration.
    /// </summary>
    [Inject]
    public required IConfiguration Configuration { get; set; }

    /// <summary>
    /// The dialog service.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    private static void NotImplemented()
    {
        MessageBox.Show("This function has not been implemented yet.");
    }

    private static void Exit()
    {
        Application.Current.Shutdown();
    }

    private void ToggleFullScreen()
    {
        Window.ToggleFullScreen();
    }

    private void ResetZoom()
    {
        Window.ResetZoom();
    }

    private void ZoomIn()
    {
        Window.ZoomIn();
    }

    private void ZoomOut()
    {
        Window.ZoomOut();
    }

    private static void ReportIssue()
    {
        OpenUrlOrPath("https://github.com/ChromaControl/ChromaControl/issues/new/choose");
    }

    private static void ContactSupport()
    {
        OpenUrlOrPath("https://discord.gg/6xGy7cycrt");
    }

    private static void ShowUserGuides()
    {
        OpenUrlOrPath("https://chromacontrol.github.io/docs");
    }

    private void ShowLogs()
    {
        OpenUrlOrPath(Configuration.GetChromaControlPath("logs"));
    }

    private void ShowAbout()
    {
        DialogService.Open<AboutDialog>();
    }

    private static void OpenUrlOrPath(string urlOrPath)
    {
        var startInfo = new ProcessStartInfo(urlOrPath)
        {
            UseShellExecute = true
        };

        Process.Start(startInfo);
    }
}
