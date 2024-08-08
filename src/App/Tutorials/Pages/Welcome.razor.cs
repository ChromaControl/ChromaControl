// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Tutorials.Pages;

/// <summary>
/// The welcome page.
/// </summary>
public partial class Welcome
{
    /// <summary>
    /// The <see cref="NavigationManager"/>.
    /// </summary>
    [Inject]
    public required NavigationManager Navigation { get; set; }

    private void NavigateForward()
    {
        Navigation.NavigateTo("/tutorials/vendors");
    }
}
