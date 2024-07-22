// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos;
using Grpc.Core;

namespace ChromaControl.Service.Home.Services;

/// <summary>
/// The home service.
/// </summary>
public class HomeService : Common.Protos.Home.HomeBase
{
    /// <summary>
    /// Gets home.
    /// </summary>
    /// <param name="request">The <see cref="GetHomeRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="GetHomeReponse"/>.</returns>
    public override Task<GetHomeReponse> GetHome(GetHomeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GetHomeReponse()
        {
            Content = "Hello Chroma Control!"
        });
    }
}
