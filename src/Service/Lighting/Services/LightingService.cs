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
    private readonly NotificationDispatcher _notificationManager;

    /// <summary>
    /// Creates a <see cref="LightingService"/> instance.
    /// </summary>
    /// <param name="notificationManager">The <see cref="NotificationDispatcher"/>.</param>
    public LightingService(NotificationDispatcher notificationManager)
    {
        _notificationManager = notificationManager;
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

        _notificationManager.EventTriggered += EventAction;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(5000, context.CancellationToken);
        }

        _notificationManager.EventTriggered -= EventAction;
    }
}
