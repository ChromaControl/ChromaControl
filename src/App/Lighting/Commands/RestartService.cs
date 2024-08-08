// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Commands;

/// <summary>
/// Restarts the lighting service.
/// </summary>
public class RestartService
{
    /// <summary>
    /// Restarts the service.
    /// </summary>
    public record struct Command() : ICommand;

    /// <summary>
    /// Handles restarting the service.
    /// </summary>
    public class Handler : ICommandHandler<Command>
    {
        private readonly LightingGrpc.LightingGrpcClient _lightingClient;

        /// <summary>
        /// Creates a <see cref="Handler"/> instance.
        /// </summary>
        /// <param name="lightingClient">The <see cref="LightingGrpc.LightingGrpcClient"/>.</param>
        public Handler(LightingGrpc.LightingGrpcClient lightingClient)
        {
            _lightingClient = lightingClient;
        }

        /// <inheritdoc/>
        public async Task<Result<int, string>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _lightingClient.RestartServiceAsync(new(), cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
