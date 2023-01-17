// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// The unix domain socket connection factory.
/// </summary>
internal class UnixDomainSocketConnectionFactory
{
    /// <summary>
    /// The connection endpoint.
    /// </summary>
    private readonly EndPoint _endPoint;

    /// <summary>
    /// Creates a <see cref="UnixDomainSocketConnectionFactory"/> instance.
    /// </summary>
    /// <param name="endPoint">The <see cref="EndPoint"/>.</param>
    public UnixDomainSocketConnectionFactory(EndPoint endPoint)
    {
        _endPoint = endPoint;
    }

    /// <summary>
    /// Connects to the socket endpoint.
    /// </summary>
    /// <param name="_">The <see cref="SocketsHttpConnectionContext"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Stream"/>.</returns>
    public async ValueTask<Stream> ConnectAsync(SocketsHttpConnectionContext _, CancellationToken cancellationToken = default)
    {
        var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

        try
        {
            await socket.ConnectAsync(_endPoint, cancellationToken).ConfigureAwait(false);
            return new NetworkStream(socket, true);
        }
        catch
        {
            socket.Dispose();
            throw;
        }
    }
}
