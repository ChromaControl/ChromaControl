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
    private readonly List<TaskCompletionSource<dynamic>> _tasks = [];

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
    /// Shows a info dialog.
    /// </summary>
    /// <param name="message">The info message.</param>
    public void ShowInfo(string message)
    {
        AddDialog<InfoDialog>(new() { { "Message", message } });
    }

    /// <summary>
    /// Shows an error dialog.
    /// </summary>
    /// <param name="message">The error message.</param>
    public void ShowError(string message)
    {
        AddDialog<ErrorDialog>(new() { { "Message", message } });
    }

    /// <summary>
    /// Shows a warning dialog.
    /// </summary>
    /// <param name="message">The warning message.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task<dynamic> ShowWarning(string message)
    {
        AddDialog<WarningDialog>(new() { { "Message", message } });

        var task = new TaskCompletionSource<dynamic>();
        _tasks.Add(task);

        return task.Task;
    }

    /// <summary>
    /// Closes the current dialog.
    /// </summary>
    public void Close(dynamic? result = null)
    {
        _dialogs.RemoveAt(_dialogs.Count - 1);

        var task = _tasks.LastOrDefault();

        if (task != null && task.Task != null && !task.Task.IsCompleted)
        {
            _tasks.Remove(task);
            task.SetResult(result);
        }

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
