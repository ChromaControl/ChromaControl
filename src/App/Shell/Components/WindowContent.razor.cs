// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The window content component.
/// </summary>
public partial class WindowContent
{
    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
}
