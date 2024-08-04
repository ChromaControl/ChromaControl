// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Shell.Commands;

/// <summary>
/// Changes the theme.
/// </summary>
public class ChangeTheme
{
    /// <summary>
    /// Changes the theme.
    /// </summary>
    /// <param name="Theme">The theme to change to.</param>
    public record struct Command(string Theme) : ICommand;

    /// <summary>
    /// Handles changing the theme.
    /// </summary>
    public class Handler : ICommandHandler<Command>
    {
        private readonly SettingsGrpc.SettingsGrpcClient _settingsClient;

        /// <summary>
        /// Creates a <see cref="Handler"/> instance.
        /// </summary>
        /// <param name="settingsClient">The <see cref="SettingsGrpc.SettingsGrpcClient"/>.</param>
        public Handler(SettingsGrpc.SettingsGrpcClient settingsClient)
        {
            _settingsClient = settingsClient;
        }

        /// <inheritdoc/>
        public async Task<Result<int, string>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _settingsClient.SetStringAsync(new()
            {
                Module = "Shell",
                Name = "Theme",
                Value = request.Theme
            }, cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
