// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Lighting.Queries;
using ChromaControl.App.Shell.Services;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device info tab.
/// </summary>
public partial class DeviceInfoTab
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

    private Common.Protos.Lighting.DeviceInfoResponse? _deviceInfo;

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        await UpdateDeviceInfo();
    }

    private async Task UpdateDeviceInfo()
    {
        var response = await Mediator.Send(new GetDeviceInfo.Query(DeviceIndex));

        if (response.IsSuccess(out var deviceInfo))
        {
            _deviceInfo = deviceInfo;
        }
        else if (response.IsFailure(out var error))
        {
            _deviceInfo = null;

            DialogService.ShowError(error);
        }
    }
}
