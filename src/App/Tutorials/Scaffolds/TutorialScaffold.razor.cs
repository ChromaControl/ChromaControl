// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Tutorials.Scaffolds;

/// <summary>
/// The tutorial scaffold.
/// </summary>
public partial class TutorialScaffold
{
    /// <summary>
    /// The title of the tutorial.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Title { get; set; }

    /// <summary>
    /// The extended title of the tutorial.
    /// </summary>
    [Parameter]
    public string? TitleExtended { get; set; }

    /// <summary>
    /// The description of the tutorial.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Description { get; set; }

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// The buttons to be rendered.
    /// </summary>
    [Parameter]
    public RenderFragment? Buttons { get; set; }
}
