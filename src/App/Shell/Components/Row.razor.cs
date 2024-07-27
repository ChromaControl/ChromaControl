// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The row component.
/// </summary>
public partial class Row
{
    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// If the contents should be centered.
    /// </summary>
    [Parameter]
    public bool Center { get; set; }

    private string CenterClass => $"row-{(Center ? "center" : "left")}";

    /// <summary>
    /// The <see cref="MarginType"/> to use for the row.
    /// </summary>
    [Parameter]
    public MarginType Margin { get; set; } = MarginType.Standard;

    private string MarginClass => $"row-margin-{Margin.ToString().ToLower()}";

    /// <summary>
    /// The margin type the dialog can use.
    /// </summary>
    public enum MarginType
    {
        /// <summary>
        /// The standard dialog margin.
        /// </summary>
        Standard,

        /// <summary>
        /// No dialog margin.
        /// </summary>
        None
    }
}
