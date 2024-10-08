﻿// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;

namespace ChromaControl.Service.Lighting.Services;

/// <summary>
/// The event dispatcher.
/// </summary>
public class EventDispatcher
{
    /// <summary>
    /// Occurs when any event happens.
    /// </summary>
    public event Action<EventType>? EventTriggered;

    /// <summary>
    /// If a zone has been resized.
    /// </summary>
    public bool ZoneResized { get; set; }

    /// <summary>
    /// Raises <see cref="EventType.DevicesUpdated"/>.
    /// </summary>
    public void RaiseDevicesUpdated()
    {
        if (ZoneResized)
        {
            ZoneResized = false;
        }
        else
        {
            EventTriggered?.Invoke(EventType.DevicesUpdated);
        }
    }
}
