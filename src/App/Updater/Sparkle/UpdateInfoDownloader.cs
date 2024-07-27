// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetSparkleUpdater.Interfaces;
using System.Net.Http;
using System.Text;

namespace ChromaControl.App.Updater.Sparkle;

/// <summary>
/// The update info downloader.
/// </summary>
public class UpdateInfoDownloader : IAppCastDataDownloader
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Creates an <see cref="UpdateInfoDownloader"/> instance.
    /// </summary>
    /// <param name="httpClient">The <see cref="HttpClient"/> used to download info.</param>
    public UpdateInfoDownloader(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public string DownloadAndGetAppCastData(string url)
    {
        var result = _httpClient.GetAsync(url).Result;

        return result.Content.ReadAsStringAsync().Result!;
    }

    /// <inheritdoc/>
    public Encoding GetAppCastEncoding()
    {
        return Encoding.UTF8;
    }
}
