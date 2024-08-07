// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;
using Grpc.Core;

namespace ChromaControl.App.Lighting.Services;

/// <summary>
/// Monitors for notifications.
/// </summary>
public class NotificationMonitor : BackgroundService
{
    private readonly LightingGrpc.LightingGrpcClient _lightingClient;
    private readonly LightingService _lightingService;

    /// <summary>
    /// Creates a <see cref="NotificationMonitor"/> instance.
    /// </summary>
    /// <param name="lightingClient">The <see cref="LightingGrpc.LightingGrpcClient"/>.</param>
    /// <param name="lightingService">The <see cref="LightingService"/>.</param>
    public NotificationMonitor(LightingGrpc.LightingGrpcClient lightingClient, LightingService lightingService)
    {
        _lightingClient = lightingClient;
        _lightingService = lightingService;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var call = _lightingClient.StreamNotifications(new(), cancellationToken: stoppingToken);

                await foreach (var notification in call.ResponseStream.ReadAllAsync(cancellationToken: stoppingToken))
                {
                    _lightingService.RaiseNotification(notification.Type);
                }

                await Task.Delay(100, stoppingToken);
            }
            catch { }
        }
    }
}
