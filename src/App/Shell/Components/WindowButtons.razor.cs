// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using System.Windows;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window buttons component.
/// </summary>
public partial class WindowButtons
{
    [Inject]
    private Window Window { get; set; } = default!;

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
