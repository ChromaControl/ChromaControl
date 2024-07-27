// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Services;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Scaffolds;

/// <summary>
/// The notification scaffold.
/// </summary>
public partial class NotificationScaffold
{
    /// <summary>
    /// The <see cref="NotificationService"/>.
    /// </summary>
    [Inject]
    public required NotificationService NotificationService { get; set; }

    /// <summary>
    /// The content to be rendered.
    /// </summary>
    [Parameter]
    public required RenderFragment ChildContent { get; set; }
}
