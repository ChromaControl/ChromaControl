// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Settings.Components;
using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The blank slate component.
/// </summary>
public partial class BlankSlate
{
    /// <summary>
    /// The dialog service.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    private void ShowSettings()
    {
        DialogService.Open<SettingsDialog>();
    }
}
