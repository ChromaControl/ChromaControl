// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The tab item component.
/// </summary>
public partial class TabItem
{
    /// <summary>
    /// The content of the tab item.
    /// </summary>
    [Parameter, EditorRequired]
    public required Type Content { get; set; }

    /// <summary>
    /// The label of the tab item.
    /// </summary>
    [Parameter, EditorRequired]
    public required string Label { get; set; }

    /// <summary>
    /// If this is the default tab.
    /// </summary>
    [Parameter]
    public bool DefaultItem { get; set; }

    /// <summary>
    /// The icon of the tab item.
    /// </summary>
    [Parameter]
    public Icon.IconStyle Icon { get; set; } = Components.Icon.IconStyle.None;

    /// <summary>
    /// The <see cref="Tabs"/> this item is nested in.
    /// </summary>
    [CascadingParameter(Name = "Tabs")]
    public required Tabs Tabs { get; set; }

    private bool Active { get; set; }
    private string StateClass => Active ? "active" : "inactive";
    private string AriaSelected => Active.ToString().ToLower();
    private string TabIndex => Active ? "0" : "-1";

    /// <summary>
    /// Renders the tab content.
    /// </summary>
    /// <returns></returns>
    public RenderFragment RenderContent()
    {
        return new(builder =>
        {
            builder.OpenComponent(0, Content);
            builder.CloseComponent();
        });
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        Tabs.ActiveTabChanged += ActiveTabChanged;

        if (DefaultItem)
        {
            Tabs.ChangeActiveTab(this);
        }
    }

    private void ActiveTabChanged(object? sender, TabItem e)
    {
        if (e == this && !Active)
        {
            Active = true;
            StateHasChanged();
        }

        if (e != this && Active)
        {
            Active = false;
            StateHasChanged();
        }
    }

    private void OnSelect()
    {
        if (!Active)
        {
            Tabs.ChangeActiveTab(this);
        }
    }
}
