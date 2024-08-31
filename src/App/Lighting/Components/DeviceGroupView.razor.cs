// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Lighting.Queries;
using ChromaControl.App.Lighting.Services;
using ChromaControl.App.Shell.Services;
using Google.Protobuf.Collections;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device group view component.
/// </summary>
public partial class DeviceGroupView : IDisposable
{
    /// <summary>
    /// The <see cref="EventService"/>.
    /// </summary>
    [Inject]
    public required EventService EventService { get; set; }

    /// <summary>
    /// The <see cref="IMediator"/>.
    /// </summary>
    [Inject]
    public required IMediator Mediator { get; set; }

    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <summary>
    /// Occurs when the active group changes.
    /// </summary>
    public event EventHandler<DeviceGroup>? ActiveGroupChanged;

    private RepeatedField<Common.Protos.Lighting.DeviceGroup> _groups = [];
    private string? _activeGroup;

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        await UpdateGroupsList();

        EventService.DevicesUpdated += OnDevicesUpdated;
    }

    /// <summary>
    /// Changes the active group.
    /// </summary>
    /// <param name="newItem">The new active group.</param>
    public void ChangeActiveGroup(DeviceGroup newItem)
    {
        ActiveGroupChanged?.Invoke(this, newItem);
        _activeGroup = newItem.Name;

        StateHasChanged();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        EventService.DevicesUpdated -= OnDevicesUpdated;

        GC.SuppressFinalize(this);
    }

    private async Task UpdateGroupsList()
    {
        var response = await Mediator.Send(new GetDeviceGroups.Query());

        if (response.IsSuccess(out var groups))
        {
            _groups = groups;
        }
        else if (response.IsFailure(out var error))
        {
            _groups = [];

            DialogService.ShowError(error);
        }

        if (_activeGroup != null && !_groups.Any(g => g.Name == _activeGroup))
        {
            _activeGroup = null;
        }
    }

    private async void OnDevicesUpdated()
    {
        await UpdateGroupsList();
        await InvokeAsync(StateHasChanged);
    }
}
