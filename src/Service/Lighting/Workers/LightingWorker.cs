// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.SDK.OpenRGB;
using ChromaControl.SDK.Synapse;
using ChromaControl.SDK.Synapse.Enums;
using ChromaControl.Service.Data;
using ChromaControl.Service.Data.Extensions;
using ChromaControl.Service.Lighting.Services;
using System.Drawing;

namespace ChromaControl.Service.Lighting.Workers;

/// <summary>
/// The lighting worker.
/// </summary>
public class LightingWorker : IHostedService
{
    private readonly ILogger<LightingWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly EventDispatcher _eventDispatcher;
    private readonly IOpenRGBService _openRGBService;
    private readonly ISynapseService _synapseService;
    private IReadOnlyList<SDK.OpenRGB.Structs.OpenRGBDevice> _devices = [];

    /// <summary>
    /// Creates a <see cref="LightingWorker"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/>.</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
    /// <param name="eventDispatcher">The <see cref="EventDispatcher"/>.</param>
    /// <param name="openRGBService">The <see cref="IOpenRGBService"/>.</param>
    /// <param name="synapseService">The <see cref="ISynapseService"/>.</param>
    public LightingWorker(ILogger<LightingWorker> logger, IServiceProvider serviceProvider, EventDispatcher eventDispatcher, IOpenRGBService openRGBService, ISynapseService synapseService)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _eventDispatcher = eventDispatcher;
        _openRGBService = openRGBService;
        _synapseService = synapseService;

        _openRGBService.DeviceListUpdated += DeviceListUpdated;
        _synapseService.StatusChanged += OnStatusChanged;
        _synapseService.ColorsReceived += OnColorsReceived;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await WriteConfiguration();
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void DeviceListUpdated(object? sender, IReadOnlyList<SDK.OpenRGB.Structs.OpenRGBDevice> e)
    {
        _devices = e;
        _eventDispatcher.RaiseDevicesUpdated();
    }

    private async Task WriteConfiguration()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var config = await context.GenerateConfig();

        _openRGBService.UpdateConfiguration(config);
    }

    private void OnStatusChanged(object? sender, SynapseStatus e)
    {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
        if (e == SynapseStatus.Live)
        {
            _logger.LogInformation("Connected to Synapse...");

        }
        else if (e == SynapseStatus.NotLive)
        {
            _logger.LogInformation("Disconnected from Synapse...");
        }
#pragma warning restore CA1848 // Use the LoggerMessage delegates
    }

    private async void OnColorsReceived(object? sender, Color[] e)
    {
        var currentColor = 0;

        foreach (var device in _devices)
        {
            var buffer = device.CreateColorBuffer();

            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = currentColor switch
                {
                    0 => e[0],
                    1 => e[1],
                    2 => e[2],
                    3 => e[3],
                    4 => e[4],
                    _ => Color.FromArgb(0, 0, 0)
                };

                if (currentColor == 4)
                {
                    currentColor = 0;
                }
                else
                {
                    currentColor++;
                }
            }

            await _openRGBService.UpdateLedsAsync(device, buffer);
        }
    }
}
