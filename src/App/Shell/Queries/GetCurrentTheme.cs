// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Shell.Queries;

/// <summary>
/// Gets the current theme.
/// </summary>
public class GetCurrentTheme
{
    /// <summary>
    /// Queries for the current theme.
    /// </summary>
    public record struct Query : IQuery<string>;

    /// <summary>
    /// Handles getting the current theme.
    /// </summary>
    public class Handler : IQueryHandler<Query, string>
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
        public async Task<Result<string, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _settingsClient.GetStringAsync(new()
            {
                Module = "Shell",
                Name = "Theme"
            }, cancellationToken: cancellationToken);

            return Success(result.Value);
        }
    }
}
