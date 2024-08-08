// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Settings;

namespace ChromaControl.App.Tutorials.Queries;

/// <summary>
/// Gets the tutorials status.
/// </summary>
public class GetTutorialsStatus
{
    /// <summary>
    /// Queries for the tutorial status.
    /// </summary>
    public record struct Query : IQuery<bool>;

    /// <summary>
    /// Handles getting the tutorial status.
    /// </summary>
    public class Handler : IQueryHandler<Query, bool>
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
        public async Task<Result<bool, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _settingsClient.GetBoolAsync(new()
            {
                Module = "Shell",
                Name = "TutorialsComplete"
            }, cancellationToken: cancellationToken);

            return Success(result.Value);
        }
    }
}
