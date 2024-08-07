// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Services;

/// <summary>
/// The notification dispatcher.
/// </summary>
public class NotificationDispatcher
{
    /// <summary>
    /// Occurs when the devices are updated.
    /// </summary>
    public event Action? DevicesUpdated;

    /// <summary>
    /// Raised <see cref="DevicesUpdated"/>.
    /// </summary>
    public void RaiseNotification(NotificationType type)
    {
        switch (type)
        {
            case NotificationType.DevicesUpdated:
                DevicesUpdated?.Invoke();
                break;
            default:
                break;
        }
    }
}
