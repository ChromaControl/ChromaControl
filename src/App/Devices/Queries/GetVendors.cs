// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Devices;
using Google.Protobuf.Collections;

namespace ChromaControl.App.Devices.Queries;

/// <summary>
/// Gets the vendors.
/// </summary>
public class GetVendors
{
    /// <summary>
    /// Gets the vendors.
    /// </summary>
    public record struct Query() : IQuery<RepeatedField<Vendor>>;

    /// <summary>
    /// Handles getting the vendors..
    /// </summary>
    public class Handler : IQueryHandler<Query, RepeatedField<Vendor>>
    {
        private readonly DevicesGrpc.DevicesGrpcClient _devicesClient;

        /// <summary>
        /// Creates a <see cref="Handler"/> instance.
        /// </summary>
        /// <param name="devicesClient">The <see cref="DevicesGrpc.DevicesGrpcClient"/>.</param>
        public Handler(DevicesGrpc.DevicesGrpcClient devicesClient)
        {
            _devicesClient = devicesClient;
        }

        /// <inheritdoc/>
        public async Task<Result<RepeatedField<Vendor>, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _devicesClient.GetVendorsAsync(new(), cancellationToken: cancellationToken);

            return Success(response.Vendors);
        }
    }
}
