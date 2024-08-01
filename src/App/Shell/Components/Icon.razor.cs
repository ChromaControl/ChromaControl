// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The icon component.
/// </summary>
public partial class Icon
{
    /// <summary>
    /// The <see cref="IconStyle"/> to use for the icon.
    /// </summary>
    [Parameter, EditorRequired]
    public IconStyle Style { get; set; }

    /// <summary>
    /// Any additional values.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

    /// <summary>
    /// The icon types.
    /// </summary>
    public enum IconStyle
    {
        /// <summary>
        /// No icon.
        /// </summary>
        None,

        /// <summary>
        /// The paintbrush icon.
        /// </summary>
        Paintbrush,

        /// <summary>
        /// The package icon.
        /// </summary>
        Package
    }
}
