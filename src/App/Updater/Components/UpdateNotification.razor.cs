// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using ChromaControl.App.Updater.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Updater.Components;

/// <summary>
/// The update notification.
/// </summary>
public partial class UpdateNotification
{
    /// <summary>
    /// The <see cref="UpdateService"/>.
    /// </summary>
    [Inject]
    public required UpdateService UpdateService { get; set; }

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    private void ShowReleaseNotes()
    {
        DialogService.Open<ReleaseNotesDialog>();
    }
}
