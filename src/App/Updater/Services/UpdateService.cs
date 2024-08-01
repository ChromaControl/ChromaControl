// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using ChromaControl.App.Updater.Components;
using MarkdownSharp;
using Microsoft.AspNetCore.Components;
using NetSparkleUpdater;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ChromaControl.App.Updater.Services;

/// <summary>
/// The update service.
/// </summary>
public class UpdateService
{
    private bool _started;
    private string? _currentVersion;
    private string? _currentHash;
    private bool _isPrerelease;
    private bool _updateCheckRunning;
    private AppCastItem? _latestUpdate;
    private readonly SparkleUpdater _updater;
    private readonly NotificationService _notificationService;
    private readonly IWebHostEnvironment _hostEnvironment;

    /// <summary>
    /// Occurs when the updater state changes.
    /// </summary>
    public event Action? StateChanged;

    /// <summary>
    /// Creates an <see cref="UpdateService"/> instance.
    /// </summary>
    /// <param name="updater">The <see cref="SparkleUpdater"/>.</param>
    /// <param name="notificationService">The <see cref="NotificationService"/>.</param>
    /// <param name="hostEnvironment">The <see cref="IWebHostEnvironment"/></param>
    public UpdateService(SparkleUpdater updater, NotificationService notificationService, IWebHostEnvironment hostEnvironment)
    {
        _updater = updater;
        _notificationService = notificationService;
        _hostEnvironment = hostEnvironment;

        _updater.LoopStarted += UpdateLoopStarted;
        _updater.LoopFinished += UpdateLoopFinished;
        _updater.UpdateCheckStarted += UpdateCheckStarted;
        _updater.UpdateCheckFinished += UpdateCheckFinished;
        _updater.UpdateDetected += UpdateDetected;
        _updater.DownloadFinished += DownloadFinished;
        _updater.CloseApplication += CloseApplication;
    }

    /// <summary>
    /// Starts the update service.
    /// </summary>
    public void StartService()
    {
        if (!_started && !IsDevelopmentMode())
        {
            _updater.StartLoop(true, true, TimeSpan.FromMinutes(15));
            _started = true;
        }
    }

    /// <summary>
    /// Gets the current version of the application.
    /// </summary>
    /// <returns></returns>
    public (string version, string? hash, bool prerelease) GetCurrentVersion()
    {
        if (_currentVersion != null)
        {
            return (_currentVersion, _currentHash, _isPrerelease);
        }

        var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var appExecutable = $"{AppDomain.CurrentDomain.FriendlyName}.exe";
        var appPath = Path.Combine(appDirectory, appExecutable);
        var appVersionInfo = FileVersionInfo.GetVersionInfo(appPath);
        var appVersion = appVersionInfo.ProductVersion;

        if (appVersion is null)
        {
            _currentVersion = "Unknown";

            return (_currentVersion, _currentHash, _isPrerelease);
        }

        _isPrerelease = appVersion.Contains('-');

        var hashIndex = appVersion.IndexOf('+');

        if (hashIndex == -1)
        {
            _currentVersion = appVersion;

            return (_currentVersion, _currentHash, _isPrerelease);
        }
        else
        {
            _currentVersion = appVersion[..hashIndex];

            var hash = appVersion[(hashIndex + 1)..];

            if (hash.Length > 7)
            {
                _currentHash = $"({hash[..7]})";
            }
            else
            {
                _currentHash = $"({hash})";
            }

            return (_currentVersion, _currentHash, _isPrerelease);
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
    /// Checks if an update is being checked for.
    /// </summary>
    /// <returns>If an update is being checked for.</returns>
    public bool IsCheckingForUpdate()
    {
        return _updateCheckRunning;
    }

    /// <summary>
    /// Checks if an update is being downloaded.
    /// </summary>
    /// <returns>If an update is being downloaded.</returns>
    public bool IsDownloadingUpdate()
    {
        if (_latestUpdate != null)
        {
            return _updater.IsDownloadingItem(_latestUpdate);
        }

        return false;
    }

    /// <summary>
    /// Checks if we are in development mode.
    /// </summary>
    /// <returns>If we are in development mode.</returns>
    public bool IsDevelopmentMode()
    {
        return _hostEnvironment.IsDevelopment();
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

    private void UpdateLoopStarted(object sender)
    {
        _updateCheckRunning = true;

        StateChanged?.Invoke();
    }

    private void UpdateLoopFinished(object sender, bool updateRequired)
    {
        _updateCheckRunning = false;

        StateChanged?.Invoke();
    }

    private void UpdateCheckStarted(object sender)
    {
        _updateCheckRunning = true;

        StateChanged?.Invoke();
    }

    private void UpdateCheckFinished(object sender, NetSparkleUpdater.Enums.UpdateStatus status)
    {
        _updateCheckRunning = false;

        StateChanged?.Invoke();
    }

    private void UpdateDetected(object sender, NetSparkleUpdater.Events.UpdateDetectedEventArgs e)
    {
        _latestUpdate = e.LatestVersion;
        _updater.InitAndBeginDownload(e.LatestVersion);

        StateChanged?.Invoke();
    }

    private void DownloadFinished(AppCastItem item, string path)
    {
        _notificationService.Post<UpdateNotification>();

        StateChanged?.Invoke();
    }

    private void CloseApplication()
    {
        Application.Current.Shutdown();
    }
}
