// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Updater.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Updater.Components;

/// <summary>
/// The release notes dialog.
/// </summary>
public partial class ReleaseNotesDialog
{
    private string? LatestVersion => $"VERSION {UpdateService.GetLatestVersion()}";

    /// <summary>
    /// The <see cref="UpdateService"/>.
    /// </summary>
    [Inject]
    public required UpdateService UpdateService { get; set; }
}
