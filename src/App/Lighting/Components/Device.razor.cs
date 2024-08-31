// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device component.
/// </summary>
public partial class Device : IDisposable
{
    /// <summary>
    /// The index of the device.
    /// </summary>
    [Parameter, EditorRequired]
    public required int Index { get; set; }

    /// <summary>
    /// The name of the device.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Name { get; set; }

    /// <summary>
    /// If this is the default device.
    /// </summary>
    [Parameter]
    public bool DefaultItem { get; set; }

    /// <summary>
    /// The <see cref="DeviceView"/> this item is nested in.
    /// </summary>
    [CascadingParameter(Name = "DeviceView")]
    public required DeviceView DeviceView { get; set; }

    private bool Active { get; set; }
    private string StateClass => Active ? "active" : "inactive";
    private string AriaSelected => Active.ToString().ToLower();
    private string TabIndex => Active ? "0" : "-1";

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        DeviceView.ActiveDeviceChanged += ActiveDeviceChanged;
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (DefaultItem)
        {
            DeviceView.ChangeActiveDevice(this);
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        DeviceView.ActiveDeviceChanged -= ActiveDeviceChanged;

        GC.SuppressFinalize(this);
    }

    private void ActiveDeviceChanged(object? sender, Device e)
    {
        if (e == this && !Active)
        {
            Active = true;
            StateHasChanged();
        }

        if (e != this && Active)
        {
            Active = false;
            StateHasChanged();
        }
    }

    private void OnSelect()
    {
        if (!Active)
        {
            DeviceView.ChangeActiveDevice(this);
        }
    }
}
