// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;
using ChromaControl.SDK.OpenRGB;
using ChromaControl.Service.Data;
using ChromaControl.Service.Data.Extensions;
using Grpc.Core;

namespace ChromaControl.Service.Lighting.Services;

/// <summary>
/// The lighting service
/// </summary>
public class LightingService : LightingGrpc.LightingGrpcBase
{
    private readonly AppDbContext _context;
    private readonly IOpenRGBService _openRGBService;
    private readonly NotificationDispatcher _notificationDispatcher;

    /// <summary>
    /// Creates a <see cref="LightingService"/> instance.
    /// </summary>
    /// <param name="context">The <see cref="AppDbContext"/>.</param>
    /// <param name="openRGBService">The <see cref="IOpenRGBService"/>.</param>
    /// <param name="notificationDispatcher">The <see cref="NotificationDispatcher"/>.</param>
    public LightingService(AppDbContext context, IOpenRGBService openRGBService, NotificationDispatcher notificationDispatcher)
    {
        _context = context;
        _openRGBService = openRGBService;
        _notificationDispatcher = notificationDispatcher;
    }

    /// <summary>
    /// Restarts the service.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> RestartService(EmptyMessage request, ServerCallContext context)
    {
        var config = await _context.GenerateConfig();

        _openRGBService.UpdateConfiguration(config);
        await _openRGBService.Restart();

        return request;
    }

    /// <summary>
    /// Streams notifications.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="responseStream">The <see cref="IServerStreamWriter{T}"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
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
