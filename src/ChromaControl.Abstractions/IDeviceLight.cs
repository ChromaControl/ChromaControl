// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;

namespace ChromaControl.Abstractions
{
    /// <summary>
    /// A device light interface
    /// </summary>
    public interface IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        Color Color { get; set; }
    }
}
