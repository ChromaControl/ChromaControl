// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Wpf;
using ChromaControl.App.Settings.Components;
using ChromaControl.App.Shell.Services;
using ChromaControl.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window menu bar component.
/// </summary>
public partial class WindowMenuBar
{
    /// <summary>
    /// The window.
    /// </summary>
    [Inject]
    public required BlazorDesktopWindow Window { get; set; }

    /// <summary>
    /// The configuration.
    /// </summary>
    [Inject]
    public required IConfiguration Configuration { get; set; }

    /// <summary>
    /// The dialog service.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    private static void NotImplemented()
    {
        MessageBox.Show("This function has not been implemented yet.");
    }

    private void ShowSettings()
    {
        DialogService.Open<SettingsDialog>();
    }

    private static void Exit()
    {
        Application.Current.Shutdown();
    }

    private void ToggleFullScreen()
    {
        Window.ToggleFullScreen();
    }

    private void ResetZoom()
    {
        Window.ResetZoom();
    }

    private void ZoomIn()
    {
        Window.ZoomIn();
    }

    private void ZoomOut()
    {
        Window.ZoomOut();
    }

    private static void ReportIssue()
    {
        OpenUrlOrPath("https://github.com/ChromaControl/ChromaControl/issues/new/choose");
    }

    private static void ContactSupport()
    {
        OpenUrlOrPath("https://discord.gg/6xGy7cycrt");
    }

    private async Task CreateSupportBundle()
    {
        var (shouldSave, fileName) = await Task.Run<(bool result, string fileName)>(() =>
        {
            var dialog = new SaveFileDialog
            {
                FileName = $"SupportBundle-{DateTime.Now:yyyy-MM-dd_hh-mm-ss}",
                DefaultExt = ".zip",
                Filter = "Zip archives (.zip)|*.zip"
            };

            return (dialog.ShowDialog() ?? false, dialog.FileName);
        });

        if (shouldSave)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using var zipStream = new FileStream(fileName, FileMode.Create);
            using var zip = new ZipArchive(zipStream, ZipArchiveMode.Create);

            var dataPath = Configuration.GetChromaControlPath("data");

            foreach (var file in Directory.EnumerateFiles(dataPath, "*.*", SearchOption.AllDirectories))
            {
                var entryName = file[dataPath.Length..].TrimStart(Path.DirectorySeparatorChar);
                var entry = zip.CreateEntry(entryName);

                entry.LastWriteTime = File.GetLastWriteTime(file);

                using var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var entryStream = entry.Open();

                await fileStream.CopyToAsync(entryStream);
            }
        }

        DialogService.ShowInfo($"Support bundle saved to: {fileName}");
    }

    private static void ShowUserGuides()
    {
        OpenUrlOrPath("https://chromacontrol.github.io/docs");
    }

    private void ShowLogs()
    {
        OpenUrlOrPath(Configuration.GetChromaControlPath("logs"));
    }

    private void ShowAbout()
    {
        DialogService.Open<AboutDialog>();
    }

    private static void OpenUrlOrPath(string urlOrPath)
    {
        var startInfo = new ProcessStartInfo(urlOrPath)
        {
            UseShellExecute = true
        };

        Process.Start(startInfo);
    }
}
