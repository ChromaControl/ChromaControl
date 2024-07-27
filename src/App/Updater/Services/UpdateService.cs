// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetSparkleUpdater;

namespace ChromaControl.App.Updater.Services;

/// <summary>
/// The update service.
/// </summary>
public class UpdateService
{
    private bool _started;
    private readonly SparkleUpdater _updater;

    /// <summary>
    /// Creates a, <see cref="UpdateService"/> instance.
    /// </summary>
    public UpdateService(SparkleUpdater updater)
    {
        _updater = updater;
    }

    /// <summary>
    /// Starts the update service.
    /// </summary>
    public void StartService()
    {
        if (!_started)
        {
            _updater.StartLoop(true, true, TimeSpan.FromMinutes(15));
            _started = true;
        }
    }
}
