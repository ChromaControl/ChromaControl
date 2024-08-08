// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Lighting;
using Google.Protobuf.Collections;

namespace ChromaControl.App.Lighting.Queries;

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
    /// Handles getting the vendors.
    /// </summary>
    public class Handler : IQueryHandler<Query, RepeatedField<Vendor>>
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
        public async Task<Result<RepeatedField<Vendor>, string>> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await _lightingClient.GetVendorsAsync(new(), cancellationToken: cancellationToken);

            return Success(response.Vendors);
        }
    }
}
