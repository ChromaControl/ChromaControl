// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Wpf;
using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window title bar component.
/// </summary>
public partial class WindowTitleBar
{
    private readonly Dictionary<string, object> _rootAttributes = [];

    /// <summary>
    /// The window.
    /// </summary>
    [Inject]
    public required BlazorDesktopWindow Window { get; set; }

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <summary>
    /// The <see cref="StyleType"/> to use for the title bar.
    /// </summary>
    [Parameter, EditorRequired]
    public StyleType Style { get; set; }

    private string StyleClass => $"window-title-bar-{Style.ToString().ToLower()}";

    /// <summary>
    /// The style types the title bar can be.
    /// </summary>
    public enum StyleType
    {
        /// <summary>
        /// A full title bar.
        /// </summary>
        Full,

        /// <summary>
        /// A light title bar.
        /// </summary>
        Light
    }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        Window.OnFullscreenChanged += OnFullscreenChanged;
        DialogService.CurrentDialogChanged += CurrentDialogChanged;
    }

    private void OnFullscreenChanged(object? sender, bool e)
    {
        StateHasChanged();
    }

    private void CurrentDialogChanged()
    {
        if (DialogService.Any())
        {
            _rootAttributes.Add("inert", "");
        }
        else
        {
            _rootAttributes.Clear();
        }

        StateHasChanged();
    }
}
