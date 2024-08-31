// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device group component.
/// </summary>
public partial class DeviceGroup
{
    /// <summary>
    /// The name of the group.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Name { get; set; }

    /// <summary>
    /// The vendor of the group.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Vendor { get; set; }

    /// <summary>
    /// The type of the group.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Type { get; set; }

    /// <summary>
    /// The <see cref="DeviceGroupView"/> this item is nested in.
    /// </summary>
    [CascadingParameter(Name = "DeviceGroupView")]
    public required DeviceGroupView GroupView { get; set; }

    private bool Active { get; set; }
    private string StateClass => Active ? "active" : "inactive";
    private string AriaSelected => Active.ToString().ToLower();
    private string TabIndex => Active ? "0" : "-1";

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        GroupView.ActiveGroupChanged += ActiveGroupChanged;
    }

    private void ActiveGroupChanged(object? sender, DeviceGroup e)
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
            GroupView.ChangeActiveGroup(this);
        }
    }
}
