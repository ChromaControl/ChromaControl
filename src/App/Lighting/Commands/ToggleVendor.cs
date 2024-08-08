// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Commands;

/// <summary>
/// Toggles a vendor.
/// </summary>
public class ToggleVendor
{
    /// <summary>
    /// Toggles a vendor.
    /// </summary>
    /// <param name="Name">The vendor name.</param>
    /// <param name="Enabled">If the vendor is enabled.</param>
    public record struct Command(string Name, bool Enabled) : ICommand;

    /// <summary>
    /// Handles toggling the vendor.
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
            await _lightingClient.ToggleVendorAsync(new()
            {
                Name = request.Name,
                Enabled = request.Enabled
            }, cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
