// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The dialog service.
/// </summary>
public class DialogService
{
    private readonly Stack<RenderFragment> _dialogs = [];

    /// <summary>
    /// Occurs when the current dialog changes.
    /// </summary>
    public event Action? CurrentDialogChanged;

    /// <summary>
    /// Opens a dialog.
    /// </summary>
    /// <typeparam name="TDialog">The dialog type.</typeparam>
    public void Open<TDialog>() where TDialog : IComponent
    {
        _dialogs.Push(new(builder =>
        {
            builder.OpenComponent<TDialog>(0);
            builder.CloseComponent();
        }));

        CurrentDialogChanged?.Invoke();
    }

    /// <summary>
    /// Closes the current dialog.
    /// </summary>
    public void Close()
    {
        _dialogs.Pop();

        CurrentDialogChanged?.Invoke();
    }

    /// <summary>
    /// If there are any dialogs open.
    /// </summary>
    /// <returns>If there are any dialogs open.</returns>
    public bool Any()
    {
        return _dialogs.Count != 0;
    }

    /// <summary>
    /// Gets the current dialog.
    /// </summary>
    /// <returns>The dialog.</returns>
    public RenderFragment? GetCurrentDialog()
    {
        _dialogs.TryPeek(out var dialog);

        return dialog;
    }
}
