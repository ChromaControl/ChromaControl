// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using ChromaControl.Abstractions;
using CUESDK;

namespace ChromaControl.Providers.Corsair
{
    /// <summary>
    /// A Corsair device
    /// </summary>
    public class CorsairDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name => _device.Model;

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights => _lights;

        /// <summary>
        /// The number of lights on the device
        /// </summary>
        public int NumberOfLights => _device.LedsCount;

        /// <summary>
        /// The device index
        /// </summary>
        private readonly int _deviceIndex;

        /// <summary>
        /// The device
        /// </summary>
        private readonly CorsairDeviceInfo _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private readonly List<CorsairDeviceLight> _lights;

        /// <summary>
        /// Creates a Corsair device
        /// </summary>
        /// <param name="device">The device index</param>
        internal CorsairDevice(int deviceIndex)
        {
            _deviceIndex = deviceIndex;
            _device = CorsairLightingSDK.GetDeviceInfo(_deviceIndex);
            _lights = new List<CorsairDeviceLight>();

            var positions = CorsairLightingSDK.GetLedPositionsByDeviceIndex(_deviceIndex);

            foreach (var position in positions.LedPosition)
            {
                _lights.Add(new CorsairDeviceLight(new CorsairLedColor() { LedId = position.LedId }));
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            if (NumberOfLights > 0)
            {
                var buffer = new CorsairLedColor[_lights.Count];

                for (var i = 0; i < _lights.Count; i++)
                    buffer[i] = _lights[i]._deviceLight;

                CorsairLightingSDK.SetLedsColorsBufferByDeviceIndex(_deviceIndex, buffer);
                CorsairLightingSDK.SetLedsColorsFlushBuffer();
            }
        }
    }
}
