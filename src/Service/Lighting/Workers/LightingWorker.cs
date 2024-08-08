// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.SDK.OpenRGB;
using ChromaControl.Service.Data;
using ChromaControl.Service.Data.Extensions;
using ChromaControl.Service.Lighting.Services;

namespace ChromaControl.Service.Lighting.Workers;

/// <summary>
/// The lighting worker.
/// </summary>
public class LightingWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly NotificationDispatcher _notificationDispatcher;
    private readonly IOpenRGBService _openRGBService;

    /// <summary>
    /// Creates a <see cref="LightingWorker"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
    /// <param name="notificationDispatcher">The <see cref="NotificationDispatcher"/>.</param>
    /// <param name="openRGBService">The <see cref="IOpenRGBService"/>.</param>
    public LightingWorker(IServiceProvider serviceProvider, NotificationDispatcher notificationDispatcher, IOpenRGBService openRGBService)
    {
        _serviceProvider = serviceProvider;
        _notificationDispatcher = notificationDispatcher;
        _openRGBService = openRGBService;
        _openRGBService.DeviceListUpdated += DeviceListUpdated;
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
        _notificationDispatcher.RaiseDevicesUpdated();
    }

    private async Task WriteConfiguration()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var config = await context.GenerateConfig();

        _openRGBService.UpdateConfiguration(config);
    }
}
