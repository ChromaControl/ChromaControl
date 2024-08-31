// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;
using Google.Protobuf.Collections;

namespace ChromaControl.App.Lighting.Queries;

/// <summary>
/// Gets the device groups.
/// </summary>
public class GetDeviceGroups
{
    /// <summary>
    /// Gets the device groups.
    /// </summary>
    public record struct Query() : IQuery<RepeatedField<DeviceGroup>>;

    /// <summary>
    /// Handles getting the device groups.
    /// </summary>
    public class Handler : IQueryHandler<Query, RepeatedField<DeviceGroup>>
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
        public async Task<Result<RepeatedField<DeviceGroup>, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _lightingClient.GetDeviceGroupsAsync(new(), cancellationToken: cancellationToken);

            return Success(response.Groups);
        }
    }
}
