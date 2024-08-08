// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;
using Grpc.Core;

namespace ChromaControl.Service.Lighting.Services;

/// <summary>
/// The lighting service
/// </summary>
public class LightingService : LightingGrpc.LightingGrpcBase
{
    private readonly NotificationDispatcher _notificationDispatcher;

    /// <summary>
    /// Creates a <see cref="LightingService"/> instance.
    /// </summary>
    /// <param name="notificationDispatcher">The <see cref="NotificationDispatcher"/>.</param>
    public LightingService(NotificationDispatcher notificationDispatcher)
    {
        _notificationDispatcher = notificationDispatcher;
    }

    /// <summary>
    /// Streams notifications.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="responseStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task StreamNotifications(EmptyMessage request, IServerStreamWriter<Notification> responseStream, ServerCallContext context)
    {
        async void EventAction(NotificationType type)
        {
            await responseStream.WriteAsync(new() { Type = type });
        }

        _notificationDispatcher.EventTriggered += EventAction;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(5000, context.CancellationToken);
        }

        _notificationDispatcher.EventTriggered -= EventAction;
    }
}
