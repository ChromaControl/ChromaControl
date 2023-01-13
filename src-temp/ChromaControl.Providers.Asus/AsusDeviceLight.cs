// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Drawing;
using AuraServiceLib;
using ChromaControl.Abstractions;

namespace ChromaControl.Providers.Asus
{
    /// <summary>
    /// An Asus device light
    /// </summary>
    public class AsusDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light
        /// </summary>
        internal IAuraRgbLight _deviceLight;

        /// <summary>
        /// Creates an Asus device light
        /// </summary>
        /// <param name="deviceLight">The device light</param>
        internal AsusDeviceLight(IAuraRgbLight deviceLight)
        {
            _deviceLight = deviceLight;
        }

        /// <summary>
        /// Gets the device light color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.Red, _deviceLight.Green, _deviceLight.Blue);
        }

        /// <summary>
        /// Sets the device light color
        /// </summary>
        /// <param name="value">The color to set it to</param>
        private void SetColor(Color value)
        {
            _deviceLight.Red = value.R;
            _deviceLight.Green = value.G;
            _deviceLight.Blue = value.B;
        }
    }
}
