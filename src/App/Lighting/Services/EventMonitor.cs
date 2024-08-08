// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;
using Grpc.Core;

namespace ChromaControl.App.Lighting.Services;

/// <summary>
/// Monitors for notifications.
/// </summary>
public class EventMonitor : BackgroundService
{
    private readonly LightingGrpc.LightingGrpcClient _lightingClient;
    private readonly EventService _eventService;

    /// <summary>
    /// Creates a <see cref="EventMonitor"/> instance.
    /// </summary>
    /// <param name="lightingClient">The <see cref="LightingGrpc.LightingGrpcClient"/>.</param>
    /// <param name="eventService">The <see cref="EventService"/>.</param>
    public EventMonitor(LightingGrpc.LightingGrpcClient lightingClient, EventService eventService)
    {
        _lightingClient = lightingClient;
        _eventService = eventService;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var call = _lightingClient.StreamEvents(new(), cancellationToken: stoppingToken);

                await foreach (var eventInstance in call.ResponseStream.ReadAllAsync(cancellationToken: stoppingToken))
                {
                    _eventService.RaiseEvent(eventInstance.Type);
                }

                await Task.Delay(100, stoppingToken);
            }
            catch { }
        }
    }
}
