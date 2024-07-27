// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window content component.
/// </summary>
public partial class WindowContent
{
    private readonly Dictionary<string, object> _rootAttributes = [];

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        DialogService.CurrentDialogChanged += CurrentDialogChanged;
    }

    private void CurrentDialogChanged()
    {
        if (DialogService.Any())
        {
            if (_rootAttributes.Count != 0)
            {
                _rootAttributes.Add("inert", "");
            }
        }
        else
        {
            _rootAttributes.Clear();
        }

        StateHasChanged();
    }
}
