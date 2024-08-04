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
    private ThemeService.Theme _currentTheme;

    private string ThemeName => _currentTheme.ToString().ToLower();

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

        if (!string.IsNullOrWhiteSpace(cachedTheme))
        {
            Enum.TryParse(cachedTheme, true, out _currentTheme);
        }

        ThemeService.Initialize(_currentTheme);
    }

    private async void OnThemeChanged(ThemeService.Theme theme)
    {
        _currentTheme = theme;

        await JSRuntime.LocalStorageSetItem("shell.theme", ThemeName);

        await InvokeAsync(StateHasChanged);
    }
}
