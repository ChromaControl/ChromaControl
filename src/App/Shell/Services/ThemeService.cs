// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Commands;
using ChromaControl.App.Shell.Queries;
using MediatR;

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The theme service.
/// </summary>
public class ThemeService
{
    private Theme _cachedTheme;
    private readonly IMediator _mediator;
    private readonly DialogService _dialogService;

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
    /// <param name="mediator">The <see cref="IMediator"/>.</param>
    /// <param name="dialogService">The <see cref="DialogService"/>.</param>
    public ThemeService(IMediator mediator, DialogService dialogService)
    {
        _mediator = mediator;
        _dialogService = dialogService;
    }

    /// <summary>
    /// Initializes the theme service.
    /// </summary>
    /// <param name="initialTheme">The initial theme to use.</param>
    public void Initialize(Theme initialTheme)
    {
        _cachedTheme = initialTheme;

        Task.Run(async () =>
        {
            var theme = await GetCurrentTheme();
            await ChangeTheme(theme, false);
        });
    }

    /// <summary>
    /// Gets the current theme.
    /// </summary>
    /// <returns>The current theme.</returns>
    public async Task<Theme> GetCurrentTheme()
    {
        var result = await _mediator.Send(new GetCurrentTheme.Query());

        if (result.IsSuccess(out var response))
        {
            Enum.TryParse<Theme>(response, true, out var theme);

            return theme;
        }
        else
        {
            return _cachedTheme;
        }
    }

    /// <summary>
    /// Changes the current theme.
    /// </summary>
    /// <param name="theme">The theme to change to.</param>
    /// <param name="showErrors">If errors should be show.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public async Task ChangeTheme(Theme theme, bool showErrors = true)
    {
        ThemeChanged?.Invoke(theme);

        var result = await _mediator.Send(new ChangeTheme.Command(theme.ToString()));

        if (result.IsFailure(out var error) && showErrors)
        {
            _dialogService.ShowError(error);
        }
    }
}
