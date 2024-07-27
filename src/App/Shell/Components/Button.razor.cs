// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The button component.
/// </summary>
public partial class Button
{
    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Any additional values.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = [];

    /// <summary>
    /// The <see cref="StyleType"/> to use for the button.
    /// </summary>
    [Parameter]
    public StyleType Style { get; set; } = StyleType.Secondary;

    private string StyleClass => $"button-{Style.ToString().ToLower()}";

    /// <summary>
    /// The style type the button can use.
    /// </summary>
    public enum StyleType
    {
        /// <summary>
        /// A primary button.
        /// </summary>
        Primary,

        /// <summary>
        /// A secondary button.
        /// </summary>
        Secondary
    }
}
