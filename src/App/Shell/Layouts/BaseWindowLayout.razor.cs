// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

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

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        ThemeService.ThemeChanged += OnThemeChanged;

        var theme = await ThemeService.GetCurrentTheme();
        _currentTheme = theme.ToString().ToLower();
    }

    private void OnThemeChanged(ThemeService.Theme theme)
    {
        _currentTheme = theme.ToString().ToLower();

        StateHasChanged();
    }
}
