// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Tutorials.Queries;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Components;

/// <summary>
/// The routes component.
/// </summary>
public partial class Routes
{
    private bool _ready;

    /// <summary>
    /// The <see cref="IMediator"/>.
    /// </summary>
    [Inject]
    public required IMediator Mediator { get; set; }

    /// <summary>
    /// The <see cref="NavigationManager"/>.
    /// </summary>
    [Inject]
    public required NavigationManager Navigation { get; set; }

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        var result = await Mediator.Send(new GetTutorialsStatus.Query());

        if (result.IsSuccess(out var tutorialsCompleted))
        {
            if (!tutorialsCompleted)
            {
                Navigation.NavigateTo("/tutorials/welcome");
            }
        }

        _ready = true;
    }
}
