// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The menu bar dropdown item component.
/// </summary>
public partial class MenuBarDropdownItem
{
    /// <summary>
    /// The label of the item.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Label { get; set; }

    /// <summary>
    /// The shortcut of the item.
    /// </summary>
    [Parameter]
    public string? Shortcut { get; set; }

    /// <summary>
    /// Any additional values.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];
}
