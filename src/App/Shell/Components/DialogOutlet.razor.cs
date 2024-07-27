// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The dialog outlet component.
/// </summary>
public partial class DialogOutlet
{
    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        DialogService.CurrentDialogChanged += CurrentDialogChanged;
    }

    private void CurrentDialogChanged()
    {
        StateHasChanged();
    }
}
