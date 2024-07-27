// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaControl.App.Updater.Services;

/// <summary>
/// The update worker.
/// </summary>
public class UpdateWorker : IHostedService
{
    private readonly UpdateService _updateService;

    /// <summary>
    /// Creates an <see cref="UpdateWorker"/> instance.
    /// </summary>
    /// <param name="updateService">The <see cref="UpdateService"/>.</param>
    public UpdateWorker(UpdateService updateService)
    {
        _updateService = updateService;
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _updateService.StartService();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
