// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace ChromaControl.Abstractions
{
    /// <summary>
    /// A device interface
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        IEnumerable<IDeviceLight> Lights { get; }

        /// <summary>
        /// The number of lights on the device
        /// </summary>
        int NumberOfLights { get; }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        void ApplyLights();
    }
}
