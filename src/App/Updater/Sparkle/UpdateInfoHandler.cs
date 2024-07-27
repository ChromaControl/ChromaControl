// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using NetSparkleUpdater;
using NetSparkleUpdater.AppCastHandlers;
using NetSparkleUpdater.Configurations;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChromaControl.App.Updater.Sparkle;

/// <summary>
/// The update info parser.
/// </summary>
public class UpdateInfoHandler : IAppCastHandler
{
    private IAppCastDataDownloader? _downloader;
    private string? _downloadUrl;
    private Configuration? _configuration;
    private NetSparkleUpdater.Interfaces.ILogger? _logger;
    private readonly List<AppCastItem> _items = [];

    /// <inheritdoc/>
    public void SetupAppCastHandler(IAppCastDataDownloader dataDownloader, string castUrl, Configuration config, ISignatureVerifier signatureVerifier, NetSparkleUpdater.Interfaces.ILogger? logWriter = null)
    {
        _downloader = dataDownloader;
        _downloadUrl = castUrl;
        _configuration = config;
        _logger = logWriter;
    }

    /// <inheritdoc/>
    public bool DownloadAndParse()
    {
        try
        {
            _logger?.PrintMessage("Downloading app cast data...");

            var info = _downloader!.DownloadAndGetAppCastData(_downloadUrl!);

            _logger?.PrintMessage("Parsing app cast data...");

            ParseReleaseData(info);

            return true;
        }
        catch (Exception ex)
        {
            _logger?.PrintMessage("Error reading app cast {0}: {1}", _downloadUrl, ex.Message);
        }

        _logger?.PrintMessage("App cast is not valid");

        return false;
    }

    /// <inheritdoc/>
    public List<AppCastItem> GetAvailableUpdates()
    {
        var version = _configuration!.InstalledVersion;
        var hashIndex = version.IndexOf('+');

        if (hashIndex != -1)
        {
            version = version[..hashIndex];
        }

        var installed = SemVerLike.Parse(version);

        return _items.Where(x =>
        {
            if (FilterAppCastItem(installed, x) == FilterItemResult.Valid)
            {
                _logger?.PrintMessage(
                    "Item with version {0} ({1}) is a valid update! It can be downloaded at {2}",
                    x.Version,
                    x.ShortVersion,
                    x.DownloadLink);

                return true;
            }

            return false;
        }).ToList();
    }

    private FilterItemResult FilterAppCastItem(SemVerLike installed, AppCastItem item)
    {
        var installedSuffix = installed.AllSuffixes;
        var itemSuffix = item.SemVerLikeVersion.AllSuffixes;

        if (string.IsNullOrWhiteSpace(installedSuffix))
        {
            if (!string.IsNullOrWhiteSpace(itemSuffix))
            {
                return FilterItemResult.SomeOtherProblem;
            }
        }

        if (!string.IsNullOrWhiteSpace(installedSuffix))
        {
            if (string.IsNullOrWhiteSpace(itemSuffix))
            {
                return FilterItemResult.SomeOtherProblem;
            }
        }

        if (SemVerLike.Parse(item.Version).CompareTo(installed) <= 0)
        {
            _logger?.PrintMessage(
                "Rejecting update for {0} ({1}, {2}) because it is older than our current version of {3}",
                item.Version,
                item.ShortVersion,
                item.Title,
                installed);

            return FilterItemResult.VersionIsOlderThanCurrent;
        }

        return FilterItemResult.Valid;
    }

    private void ParseReleaseData(string data)
    {
        _items.Clear();

        var installed = _configuration!.InstalledVersion;
        var hashIndex = installed.IndexOf('+');

        if (hashIndex != -1)
        {
            installed = installed[..hashIndex];
        }

        var items = JsonSerializer.Deserialize<ReleaseItem[]>(data)!
            .Select(r =>
            {
                r.Assets = r.Assets.Where(a => a.Name == $"ChromaControlSetup-{r.TagName}.exe")
                    .ToArray();

                return r;
            })
            .Where(r => r.Assets.Length == 1)
            .Select(r => new AppCastItem
            {
                AppName = _configuration!.ApplicationName,
                AppVersionInstalled = installed,
                Description = r.Body,
                DownloadLink = r.Assets.First().BrowserDownloadUrl,
                DownloadSignature = string.Empty,
                IsCriticalUpdate = false,
                MIMEType = r.Assets.First().ContentType,
                OperatingSystemString = "win",
                PublicationDate = r.PublishedAt,
                ReleaseNotesLink = string.Empty,
                ReleaseNotesSignature = string.Empty,
                ShortVersion = r.TagName,
                Title = r.Name,
                UpdateSize = r.Assets.First().Size,
                Version = r.TagName
            });

        _items.AddRange(items);
    }

    private sealed class ReleaseItem
    {
        [JsonPropertyName("body")]
        public required string Body { get; set; }

        [JsonPropertyName("assets")]
        public ReleaseAsset[] Assets { get; set; } = [];

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("tag_name")]
        public required string TagName { get; set; }
    }

    private sealed class ReleaseAsset
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public required string BrowserDownloadUrl { get; set; }

        [JsonPropertyName("content_type")]
        public required string ContentType { get; set; }

        [JsonPropertyName("size")]
        public required long Size { get; set; }
    }
}
