// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protobufs;
using Grpc.Core;

namespace ChromaControl.Core.API.Services;

/// <summary>
/// The drivers service.
/// </summary>
public class DriversService : Drivers.DriversBase
{
    /// <summary>
    /// Gets a list of available drivers.
    /// </summary>
    /// <param name="request">The <see cref="GetDriversRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="GetDriversResponse"/>.</returns>
    public override Task<GetDriversResponse> GetDrivers(GetDriversRequest request, ServerCallContext context)
    {
        var response = new GetDriversResponse();

        response.Drivers.Add(new Driver()
        {
            Name = "Asus"
        });

        response.Drivers.Add(new Driver()
        {
            Name = "Corsair"
        });

        return Task.FromResult(response);
    }
}
