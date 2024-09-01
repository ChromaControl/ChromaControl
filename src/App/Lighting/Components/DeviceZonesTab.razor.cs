// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Lighting.Commands;
using ChromaControl.App.Lighting.Queries;
using ChromaControl.App.Shell.Services;
using ChromaControl.Common.Protos.Lighting;
using Google.Protobuf.Collections;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device zones tab.
/// </summary>
public partial class DeviceZonesTab
{
    /// <summary>
    /// The index of the device.
    /// </summary>
    [CascadingParameter(Name = "DeviceIndex")]
    public required int DeviceIndex { get; set; }

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

    private RepeatedField<DeviceZone> _zones = [];

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        await UpdateDeviceZones();
    }

    private async Task UpdateDeviceZones()
    {
        var response = await Mediator.Send(new GetDeviceZones.Query(DeviceIndex));

        if (response.IsSuccess(out var zones))
        {
            _zones = zones;
        }
        else if (response.IsFailure(out var error))
        {
            _zones = [];

            DialogService.ShowError(error);
        }
    }

    private async Task OnZoneChanged(DeviceZone zone)
    {
        var result = await Mediator.Send(new ResizeDeviceZone.Command(DeviceIndex, zone.Index, zone.LedCount));

        if (result.IsFailure(out var error))
        {
            DialogService.ShowError(error);
        }
    }
}
