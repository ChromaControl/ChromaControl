// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.Service.Lighting.Services;

/// <summary>
/// The notification dispatcher.
/// </summary>
public class NotificationDispatcher
{
    /// <summary>
    /// Occurs when any event happens.
    /// </summary>
    public event Action<NotificationType>? EventTriggered;

    /// <summary>
    /// Raised <see cref="EventTriggered"/>.
    /// </summary>
    public void RaiseDevicesUpdated()
    {
        EventTriggered?.Invoke(NotificationType.DevicesUpdated);
    }
}
