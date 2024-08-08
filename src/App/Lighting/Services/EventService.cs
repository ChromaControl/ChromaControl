// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.App.Lighting.Services;

/// <summary>
/// The event service.
/// </summary>
public class EventService
{
    /// <summary>
    /// Occurs when the devices are updated.
    /// </summary>
    public event Action? DevicesUpdated;

    /// <summary>
    /// Raises an event.
    /// </summary>
    public void RaiseEvent(EventType type)
    {
        switch (type)
        {
            case EventType.DevicesUpdated:
                DevicesUpdated?.Invoke();
                break;
            default:
                break;
        }
    }
}
