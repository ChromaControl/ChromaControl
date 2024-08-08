// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Tutorials.Commands;

/// <summary>
/// Finishes the tutorials.
/// </summary>
public class FinishTutorials
{
    /// <summary>
    /// Finishes the tutorials.
    /// </summary>
    public record struct Command() : ICommand;

    /// <summary>
    /// Handles finishing the tutorials.
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
            await _settingsClient.SetBoolAsync(new()
            {
                Module = "Shell",
                Name = "TutorialsComplete",
                Value = true
            }, cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
