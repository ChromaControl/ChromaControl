// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using ChromaControl.App.Updater.Components;
using MarkdownSharp;
using Microsoft.AspNetCore.Components;
using NetSparkleUpdater;
using System.Windows;

namespace ChromaControl.App.Updater.Services;

/// <summary>
/// The update service.
/// </summary>
public class UpdateService
{
    private bool _started;
    private AppCastItem? _latestUpdate;
    private readonly SparkleUpdater _updater;
    private readonly NotificationService _notificationService;

    /// <summary>
    /// Creates an <see cref="UpdateService"/> instance.
    /// </summary>
    /// <param name="updater">The <see cref="SparkleUpdater"/>.</param>
    /// <param name="notificationService">The <see cref="NotificationService"/>.</param>
    public UpdateService(SparkleUpdater updater, NotificationService notificationService)
    {
        _updater = updater;
        _notificationService = notificationService;

        _updater.UpdateDetected += UpdateDetected;
        _updater.DownloadFinished += DownloadFinished;
        _updater.CloseApplication += CloseApplication;
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

    /// <summary>
    /// Gets the latest release notes.
    /// </summary>
    /// <returns>The release notes as a <see cref="RenderFragment"/>.</returns>
    public RenderFragment? GetLatestReleaseNotes()
    {
        if (_latestUpdate != null)
        {
            var markdown = new Markdown();
            var markdownString = markdown.Transform(_latestUpdate.Description);

            return new(builder =>
            {
                builder.AddMarkupContent(0, markdownString);
            });
        }

        return null;
    }

    /// <summary>
    /// Gets the latest release notes.
    /// </summary>
    /// <returns>The version.</returns>
    public string? GetLatestVersion()
    {
        if (_latestUpdate != null)
        {
            return _latestUpdate.SemVerLikeVersion.ToString();
        }

        return null;
    }

    /// <summary>
    /// Installs the latest update.
    /// </summary>
    public async Task InstallLatestUpdate()
    {
        await _updater.InstallUpdate(_latestUpdate);
    }

    /// <summary>
    /// Checks for updates.
    /// </summary>
    /// <returns>A <see cref="Task"/>.</returns>
    public async Task CheckForUpdates()
    {
        await _updater.CheckForUpdatesQuietly();
    }

    private void UpdateDetected(object sender, NetSparkleUpdater.Events.UpdateDetectedEventArgs e)
    {
        _latestUpdate = e.LatestVersion;
        _updater.InitAndBeginDownload(e.LatestVersion);
    }

    private void DownloadFinished(AppCastItem item, string path)
    {
        _notificationService.Post<UpdateNotification>();
    }

    private void CloseApplication()
    {
        Application.Current.Shutdown();
    }
}
