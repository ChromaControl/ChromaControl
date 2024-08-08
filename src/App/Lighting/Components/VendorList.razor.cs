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
/// The vendor list component.
/// </summary>
public partial class VendorList
{
    private RepeatedField<Vendor> _vendors = [];

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

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new GetVendors.Query());

        if (response.IsSuccess(out var vendors))
        {
            _vendors = vendors;
        }
        else if (response.IsFailure(out var error))
        {
            DialogService.ShowError(error);
        }
    }

    private async Task ToggleCheckbox(Vendor vendor)
    {
        vendor.Enabled = !vendor.Enabled;

        await OnVendorChanged(vendor);
    }

    private async Task OnVendorChanged(Vendor vendor)
    {
        if (vendor.Enabled && vendor.Dangerous)
        {
            var warnResult = await DialogService.ShowWarning("This device vendor has an increased risk of bricking hardware, are you sure you want to enable it?");

            if (warnResult == null)
            {
                vendor.Enabled = false;
            }
        }

        var result = await Mediator.Send(new ToggleVendor.Command(vendor.Name, vendor.Enabled));

        if (result.IsFailure(out var error))
        {
            DialogService.ShowError(error);
        }
    }
}
