// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The theme service.
/// </summary>
public class ThemeService
{
    private readonly SettingsGrpc.SettingsGrpcClient _settingsClient;

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
    /// Creates a <see cref="ThemeService"/> instance.
    /// </summary>
    /// <param name="settingsClient">The <see cref="SettingsGrpc.SettingsGrpcClient"/>.</param>
    public ThemeService(SettingsGrpc.SettingsGrpcClient settingsClient)
    {
        _settingsClient = settingsClient;
    }

    /// <summary>
    /// Gets the current theme.
    /// </summary>
    /// <returns>The current theme.</returns>
    public async Task<Theme> GetCurrentTheme()
    {
        var result = await _settingsClient.GetStringAsync(new()
        {
            Module = "shell",
            SettingName = "theme"
        });

        Enum.TryParse<Theme>(result.Value, true, out var theme);

        return theme;
    }

    /// <summary>
    /// Changes the current theme.
    /// </summary>
    /// <param name="theme">The theme to change to.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public async Task ChangeTheme(Theme theme)
    {
        ThemeChanged?.Invoke(theme);

        await _settingsClient.SetStringAsync(new()
        {
            Module = "shell",
            SettingName = "theme",
            SettingValue = theme.ToString()
        });
    }
}
