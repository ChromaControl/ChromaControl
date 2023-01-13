// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using ChromaControl.Abstractions;

namespace ChromaControl.Providers.ORGB
{
    /// <summary>
    /// An ORGB device light
    /// </summary>
    public class ORGBDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light
        /// </summary>
        internal OpenRGB.NET.Models.Color _deviceLight;

        /// <summary>
        /// Creates an ORGB device light
        /// </summary>
        /// <param name="deviceLight">The device light</param>
        internal ORGBDeviceLight(OpenRGB.NET.Models.Color deviceLight)
        {
            _deviceLight = deviceLight;
        }

        /// <summary>
        /// Gets the device light color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.R, _deviceLight.G, _deviceLight.B);
        }

        /// <summary>
        /// Sets the device light color
        /// </summary>
        /// <param name="value">The color to set it to</param>
        private void SetColor(Color value)
        {
            _deviceLight.R = value.R;
            _deviceLight.G = value.G;
            _deviceLight.B = value.B;
        }
    }
}
