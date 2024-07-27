// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetSparkleUpdater;
using NetSparkleUpdater.Events;
using NetSparkleUpdater.Interfaces;
using System.ComponentModel;
using System.IO;
using System.Net.Http;

namespace ChromaControl.App.Updater.Sparkle;

/// <summary>
/// The update info downloader.
/// </summary>
public class UpdateDownloader : IUpdateDownloader, IDisposable
{
    private string? _downloadFileLocation;
    private CancellationTokenSource _cts;
    private readonly HttpClient _httpClient;
    private readonly NetSparkleUpdater.Interfaces.ILogger _logger;

    /// <inheritdoc/>
    public bool IsDownloading { get; private set; }

    /// <inheritdoc/>
    public event DownloadProgressEvent? DownloadProgressChanged;

    /// <inheritdoc/>
    public event AsyncCompletedEventHandler? DownloadFileCompleted;

    /// <summary>
    /// Creates an <see cref="UpdateDownloader"/> instance.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> used to download updates.</param>
    /// <param name="logger">The logger to use.</param>
    public UpdateDownloader(HttpClient httpClient, NetSparkleUpdater.Interfaces.ILogger logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _cts = new();
    }

    /// <inheritdoc/>
    public Task<string> RetrieveDestinationFileNameAsync(AppCastItem item)
    {
        // Not supported for GitHub.
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async void StartFileDownload(Uri uri, string downloadFilePath)
    {
        _logger?.PrintMessage("IUpdateDownloader: Starting file download from {0} to {1}", uri, downloadFilePath);

        await StartFileDownloadAsync(uri, downloadFilePath);
    }

    /// <inheritdoc/>
    public void CancelDownload()
    {
        _logger?.PrintMessage("IUpdateDownloader: Canceling download");

        try
        {
            _cts.Cancel();
            _httpClient.CancelPendingRequests();
        }
        catch { }

        if (File.Exists(_downloadFileLocation))
        {
            try
            {
                File.Delete(_downloadFileLocation);
            }
            catch { }
        }

        IsDownloading = false;

        _cts.Dispose();
        _cts = new CancellationTokenSource();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _cts.Dispose();

        GC.SuppressFinalize(this);
    }

    private async Task StartFileDownloadAsync(Uri uri, string downloadFilePath)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, _cts.Token);

            _downloadFileLocation = downloadFilePath;

            using (var fileStream = new FileStream(downloadFilePath, FileMode.Create, FileAccess.Write, FileShare.Read, 8192, true))
            using (var contentStream = await response.Content.ReadAsStreamAsync())
            {
                DownloadProgressChanged?.Invoke(this, new ItemDownloadProgressEventArgs(0, null, 0, 1));

                await contentStream.CopyToAsync(fileStream, _cts.Token);

                DownloadProgressChanged?.Invoke(this, new ItemDownloadProgressEventArgs(100, null, 1, 1));
            }

            IsDownloading = false;

            DownloadFileCompleted?.Invoke(this, new AsyncCompletedEventArgs(null, false, null));
        }
        catch (Exception ex)
        {
            _logger?.PrintMessage("Error: {0}", ex.Message);

            IsDownloading = false;

            DownloadFileCompleted?.Invoke(this, new AsyncCompletedEventArgs(ex, false, null));
        }
    }
}
