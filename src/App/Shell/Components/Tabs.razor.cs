// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The tabs component.
/// </summary>
public partial class Tabs
{
    private RenderFragment? _tabContent;

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    /// <summary>
    /// Occurs when the active tab changes.
    /// </summary>
    public event EventHandler<TabItem>? ActiveTabChanged;

    /// <summary>
    /// Changes the active tab.
    /// </summary>
    /// <param name="newItem">The new active item.</param>
    public void ChangeActiveTab(TabItem newItem)
    {
        _tabContent = newItem.RenderContent();

        ActiveTabChanged?.Invoke(this, newItem);

        StateHasChanged();
    }
}
