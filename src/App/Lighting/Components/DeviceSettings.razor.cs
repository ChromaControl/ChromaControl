// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Lighting.Components;

/// <summary>
/// The device settings component.
/// </summary>
public partial class DeviceSettings
{
    /// <summary>
    /// The index of the device.
    /// </summary>
    [Parameter, EditorRequired]
    public required int DeviceIndex { get; set; }
}
