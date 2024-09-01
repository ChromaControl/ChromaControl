// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Commands;

/// <summary>
/// Resizes a devices zone.
/// </summary>
public class ResizeDeviceZone
{
    /// <summary>
    /// Resizes a devices zone.
    /// </summary>
    /// <param name="DeviceIndex">The device index.</param>
    /// <param name="ZoneIndex">The zone index.</param>
    /// <param name="NewSize">The new size of the zone.</param>
    public record struct Command(int DeviceIndex, int ZoneIndex, int NewSize) : ICommand;

    /// <summary>
    /// Handles resizing the device zone.
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
            await _lightingClient.ResizeDeviceZoneAsync(new()
            {
                DeviceIndex = request.DeviceIndex,
                ZoneIndex = request.ZoneIndex,
                NewSize = request.NewSize
            }, cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
