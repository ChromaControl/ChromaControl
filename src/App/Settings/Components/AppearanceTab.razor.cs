// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Settings.Components;

/// <summary>
/// The appearance tab.
/// </summary>
public partial class AppearanceTab
{
    private ThemeService.Theme _theme;
    private bool _loading = true;

    /// <summary>
    /// The <see cref="ThemeService"/>.
    /// </summary>
    [Inject]
    public required ThemeService ThemeService { get; set; }

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        _theme = await ThemeService.GetCurrentTheme();

        _loading = false;
    }

    private async Task ChooseLightTheme()
    {
        _theme = ThemeService.Theme.Light;

        await ThemeService.ChangeTheme(_theme);
    }

    private async Task ChooseDarkTheme()
    {
        _theme = ThemeService.Theme.Dark;

        await ThemeService.ChangeTheme(_theme);
    }

    private async Task ChooseSystemTheme()
    {
        _theme = ThemeService.Theme.System;

        await ThemeService.ChangeTheme(_theme);
    }
}
