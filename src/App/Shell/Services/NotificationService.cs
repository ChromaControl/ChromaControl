// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace ChromaControl.App.Shell.Services;

/// <summary>
/// The notification service.
/// </summary>
public class NotificationService
{
    private readonly Stack<RenderFragment> _notifications = [];

    /// <summary>
    /// Occurs when the current notification changes.
    /// </summary>
    public event Action? CurrentNotificationChanged;

    /// <summary>
    /// Posts a notification.
    /// </summary>
    /// <typeparam name="TDialog">The notification type.</typeparam>
    public void Post<TDialog>() where TDialog : IComponent
    {
        _notifications.Push(new(builder =>
        {
            builder.OpenComponent<TDialog>(0);
            builder.CloseComponent();
        }));

        CurrentNotificationChanged?.Invoke();
    }

    /// <summary>
    /// Closes the current notification.
    /// </summary>
    public void Close()
    {
        _notifications.Pop();

        CurrentNotificationChanged?.Invoke();
    }

    /// <summary>
    /// If there are any notifications open.
    /// </summary>
    /// <returns>If there are any notifications open.</returns>
    public bool Any()
    {
        return _notifications.Count != 0;
    }

    /// <summary>
    /// Gets the current notification.
    /// </summary>
    /// <returns>The notification.</returns>
    public RenderFragment? GetCurrentNotification()
    {
        _notifications.TryPeek(out var notification);

        return notification;
    }
}
