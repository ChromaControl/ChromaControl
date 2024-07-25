// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The menu bar dropdown component.
/// </summary>
public partial class MenuBarDropdown
{
    /// <summary>
    /// The title of the dropdown.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Title { get; set; }

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// The <see cref="MenuBar"/> this component is nested in.
    /// </summary>
    [CascadingParameter(Name = "MenuBar")]
    public required MenuBar MenuBar { get; set; }

    private bool Expanded { get; set; }
    private string StateClass => Expanded ? "open" : "closed";
    private string AriaExpanded => Expanded.ToString().ToLower();

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        MenuBar.ActiveMenuChanged += ActiveMenuChanged;
    }

    private void ActiveMenuChanged(object? sender, MenuBarDropdown? e)
    {
        if (e == null && Expanded)
        {
            Expanded = false;
            StateHasChanged();
        }

        if (e == this && !Expanded)
        {
            Expanded = true;
            StateHasChanged();
        }

        if (e != this && Expanded)
        {
            Expanded = false;
            StateHasChanged();
        }
    }

    private void OnOpen()
    {
        if (Expanded)
        {
            MenuBar.ChangeActiveMenu(null);
        }
        else
        {
            MenuBar.ChangeActiveMenu(this);
        }
    }

    private void OnClose()
    {
        MenuBar.ChangeActiveMenu(null);
    }

    private void OnMouseEnter()
    {
        if (MenuBar.MenuActive)
        {
            MenuBar.ChangeActiveMenu(this);
        }
    }
}
