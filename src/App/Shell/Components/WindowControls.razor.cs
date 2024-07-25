// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Wpf;
using Microsoft.AspNetCore.Components;
using System.Windows;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window controls component.
/// </summary>
public partial class WindowControls
{
    /// <summary>
    /// The window.
    /// </summary>
    [Inject]
    public required BlazorDesktopWindow Window { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        Window.StateChanged += OnWindowStateChanged;
    }

    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        StateHasChanged();
    }

    private void OnMinimize()
    {
        Window.WindowState = WindowState.Minimized;
    }

    private void OnRestore()
    {
        Window.WindowState = WindowState.Normal;
    }

    private void OnMaximize()
    {
        Window.WindowState = WindowState.Maximized;
    }

    private void OnClose()
    {
        Window.Close();
    }
}
