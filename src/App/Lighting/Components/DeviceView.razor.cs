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
/// The device view component.
/// </summary>
public partial class DeviceView : IDisposable
{
    /// <summary>
    /// The name of the group.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Group { get; set; }

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
    /// Occurs when the active device changes.
    /// </summary>
    public event EventHandler<Device>? ActiveDeviceChanged;

    private RepeatedField<Common.Protos.Lighting.Device> _devices = [];
    private Common.Protos.Lighting.Device? _activeDevice;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        EventService.DevicesUpdated += OnDevicesUpdated;
    }

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        await UpdateDevicesList();
    }

    /// <summary>
    /// Changes the active device.
    /// </summary>
    /// <param name="newItem">The new active device.</param>
    public void ChangeActiveDevice(Device newItem)
    {
        _activeDevice = _devices.First(d => d.Index == newItem.Index);
        ActiveDeviceChanged?.Invoke(this, newItem);

        StateHasChanged();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        EventService.DevicesUpdated -= OnDevicesUpdated;

        GC.SuppressFinalize(this);
    }

    private async Task UpdateDevicesList()
    {
        var response = await Mediator.Send(new GetGroupDevices.Query(Group));

        if (response.IsSuccess(out var devices))
        {
            _devices = devices;
        }
        else if (response.IsFailure(out var error))
        {
            _devices = [];

            DialogService.ShowError(error);
        }

        if (_activeDevice != null && !_devices.Any(d => d.Index == _activeDevice.Index))
        {
            _activeDevice = null;
        }
    }

    private async void OnDevicesUpdated()
    {
        await UpdateDevicesList();
        await InvokeAsync(StateHasChanged);
    }
}
