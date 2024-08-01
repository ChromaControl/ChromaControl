// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Extensions;
using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ChromaControl.App.Shell.Layouts;

/// <summary>
/// The base window layout.
/// </summary>
public partial class BaseWindowLayout
{
    private string? _currentTheme;

    /// <summary>
    /// The <see cref="ThemeService"/>.
    /// </summary>
    [Inject]
    public required ThemeService ThemeService { get; set; }

    /// <summary>
    /// The <see cref="IJSRuntime"/>.
    /// </summary>
    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        ThemeService.ThemeChanged += OnThemeChanged;

        var cachedTheme = await JSRuntime.LocalStorageGetItem("shell.theme");

        if (cachedTheme != null)
        {
            _currentTheme = cachedTheme;
        }

        ThemeService.Initialize();
    }

    private async void OnThemeChanged(ThemeService.Theme theme)
    {
        _currentTheme = theme.ToString().ToLower();

        await JSRuntime.LocalStorageSetItem("shell.theme", _currentTheme);

        await InvokeAsync(StateHasChanged);
    }
}
