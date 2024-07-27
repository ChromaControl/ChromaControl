// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using ChromaControl.App.Updater.Services;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.IO;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The about dialog.
/// </summary>
public partial class AboutDialog
{
    private string? _version;
    private string? _hash;

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <summary>
    /// The <see cref="UpdateService"/>.
    /// </summary>
    [Inject]
    public required UpdateService UpdateService { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        UpdateVersionInfo();
    }

    private void UpdateVersionInfo()
    {
        var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var appExecutable = $"{AppDomain.CurrentDomain.FriendlyName}.exe";
        var appPath = Path.Combine(appDirectory, appExecutable);
        var appVersionInfo = FileVersionInfo.GetVersionInfo(appPath);
        var appVersion = appVersionInfo.ProductVersion;

        if (appVersion is null)
        {
            _version = "Unknown";

            return;
        }

        var hashIndex = appVersion.IndexOf('+');

        if (hashIndex == -1)
        {
            _version = appVersion;
            return;
        }
        else
        {
            _version = appVersion[..hashIndex];
            var hash = appVersion[(hashIndex + 1)..];

            if (hash.Length > 7)
            {
                _hash = $"({hash[..7]})";
            }
            else
            {
                _hash = $"({hash})";
            }

            return;
        }
    }

    private void OpenLicenseInfo()
    {
        DialogService.Open<LicenseDialog>();
    }
}
