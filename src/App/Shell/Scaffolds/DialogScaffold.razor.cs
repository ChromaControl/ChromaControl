// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Scaffolds;

/// <summary>
/// The dialog scaffold.
/// </summary>
public partial class DialogScaffold
{
    /// <summary>
    /// The <see cref="DialogService"/>.
    /// </summary>
    [Inject]
    public required DialogService DialogService { get; set; }

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public RenderFragment? Content { get; set; }

    /// <summary>
    /// The additional buttons to have in the footer.
    /// </summary>
    [Parameter]
    public RenderFragment? AdditionalButtons { get; set; }

    /// <summary>
    /// The width of the dialog.
    /// </summary>
    [Parameter]
    public int Width { get; set; } = 400;

    /// <summary>
    /// The title of the dialog.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// The cancel text of the dialog.
    /// </summary>
    [Parameter]
    public string? CancelText { get; set; } = "CANCEL";

    /// <summary>
    /// If the dialog is scrollable.
    /// </summary>
    [Parameter]
    public bool Scrollable { get; set; }

    /// <summary>
    /// The <see cref="PaddingType"/> to use for the dialog.
    /// </summary>
    [Parameter]
    public PaddingType Padding { get; set; } = PaddingType.Standard;

    private string PaddingClass => $"dialog-content-padding-{Padding.ToString().ToLower()}";
    private string ScrollableClass => Scrollable ? "dialog-content-scrollable" : "dialog-content-static";

    /// <summary>
    /// The padding type the dialog can use.
    /// </summary>
    public enum PaddingType
    {
        /// <summary>
        /// The standard dialog padding.
        /// </summary>
        Standard,

        /// <summary>
        /// No dialog padding.
        /// </summary>
        None
    }
}
