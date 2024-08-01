// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The notification outlet component.
/// </summary>
public partial class NotificationOutlet
{
    private readonly Dictionary<string, object> _rootAttributes = [];

    /// <summary>
    /// The <see cref="NotificationService"/>.
    /// </summary>
    [Inject]
    public required NotificationService NotificationService { get; set; }

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        NotificationService.CurrentNotificationChanged += CurrentNotificationChanged;
        DialogService.CurrentDialogChanged += CurrentDialogChanged;
    }

    private async void CurrentNotificationChanged()
    {
        await InvokeAsync(StateHasChanged);
    }

    private void CurrentDialogChanged()
    {
        if (DialogService.Any())
        {
            if (_rootAttributes.Count == 0)
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
