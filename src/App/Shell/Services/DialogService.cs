// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Shell.Components;
using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The dialog service.
/// </summary>
public class DialogService
{
    private readonly List<KeyValuePair<Guid, RenderFragment>> _dialogs = [];

    /// <summary>
    /// The dialogs.
    /// </summary>
    public IEnumerable<KeyValuePair<Guid, RenderFragment>> Dialogs => _dialogs;

    /// <summary>
    /// Occurs when the dialogs changes.
    /// </summary>
    public event Action? DialogsChanged;

    /// <summary>
    /// Opens a dialog.
    /// </summary>
    /// <typeparam name="TDialog">The dialog type.</typeparam>
    public void Open<TDialog>() where TDialog : IComponent
    {
        AddDialog<TDialog>([]);
    }

    /// <summary>
    /// Shows an error dialog.
    /// </summary>
    /// <param name="message"></param>
    public void ShowError(string message)
    {
        AddDialog<ErrorDialog>(new() { { "Message", message } });
    }

    /// <summary>
    /// Closes the current dialog.
    /// </summary>
    public void Close()
    {
        _dialogs.RemoveAt(_dialogs.Count - 1);

        DialogsChanged?.Invoke();
    }

    private void AddDialog<TDialog>(Dictionary<string, object> parameters) where TDialog : IComponent
    {
        _dialogs.Add(new(Guid.NewGuid(), builder =>
        {
            builder.OpenComponent<TDialog>(0);

            foreach (var parameter in parameters)
            {
                builder.AddComponentParameter(1, parameter.Key, parameter.Value);
            }

            builder.CloseComponent();
        }));

        DialogsChanged?.Invoke();
    }
}
