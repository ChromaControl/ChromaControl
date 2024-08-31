// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Queries;

/// <summary>
/// Gets info for a device.
/// </summary>
public class GetDeviceInfo
{
    /// <summary>
    /// Gets info for a device.
    /// </summary>
    /// <param name="DeviceIndex">The device index to get info for.</param>
    public record struct Query(int DeviceIndex) : IQuery<DeviceInfoResponse>;

    /// <summary>
    /// Handles getting the device info.
    /// </summary>
    public class Handler : IQueryHandler<Query, DeviceInfoResponse>
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
        public async Task<Result<DeviceInfoResponse, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _lightingClient.GetDeviceInfoAsync(new() { DeviceIndex = request.DeviceIndex }, cancellationToken: cancellationToken);

            return Success(response);
        }
    }
}
