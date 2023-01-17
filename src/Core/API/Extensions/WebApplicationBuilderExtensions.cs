// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ChromaControl.Core.API.Extensions;

/// <summary>
/// <see cref="WebApplicationBuilder"/> extension methods.
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Use the chroma control unix socket.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance.</param>
    /// <returns>The same <see cref="WebApplicationBuilder"/> instance.</returns>
    public static WebApplicationBuilder UseChromaControlSocket(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            if (File.Exists(ChromaControlConstants.SocketPath))
            {
                File.Delete(ChromaControlConstants.SocketPath);
            }

            options.ListenUnixSocket(ChromaControlConstants.SocketPath, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });

        return builder;
    }
}
