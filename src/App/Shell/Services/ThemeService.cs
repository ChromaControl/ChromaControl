// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The theme service.
/// </summary>
public class ThemeService
{
    private Theme _currentTheme = Theme.System;

    /// <summary>
    /// Occurs when the theme changes.
    /// </summary>
    public event Action<Theme>? ThemeChanged;

    /// <summary>
    /// The supported themes.
    /// </summary>
    public enum Theme
    {
        /// <summary>
        /// The system theme.
        /// </summary>
        System,

        /// <summary>
        /// The light theme.
        /// </summary>
        Light,

        /// <summary>
        /// The dark theme.
        /// </summary>
        Dark
    }

    /// <summary>
    /// Gets the current theme.
    /// </summary>
    /// <returns>The current theme.</returns>
    public async Task<Theme> GetCurrentTheme()
    {
        await Task.Delay(1);

        return _currentTheme;
    }

    /// <summary>
    /// Changes the current theme.
    /// </summary>
    /// <param name="theme">The theme to change to.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public async Task ChangeTheme(Theme theme)
    {
        _currentTheme = theme;

        ThemeChanged?.Invoke(theme);

        await Task.Delay(1);
    }
}
