// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Drawing;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Logging;
using OpenRGB.NET;

namespace ChromaControl.Providers.Corsair
{
    /// <summary>
    /// The Corsair device provider
    /// </summary>
    public class CorsairDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name => "Corsair";

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices => _devices;

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private readonly List<CorsairDevice> _devices;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private readonly OpenRGBClient _sdk;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<CorsairDeviceProvider> _logger;

        /// <summary>
        /// Creates an Corsair device provider
        /// </summary>
        /// <param name="logger">The logger</param>
        public CorsairDeviceProvider(ILogger<CorsairDeviceProvider> logger)
        {
            _devices = new List<CorsairDevice>();
            _sdk = new OpenRGBClient(name: "My Corsair Client", autoconnect: true, timeout: 1000);
            _logger = logger;
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            /*PerformHealthCheck();

            Thread.Sleep(30000);*/

            var deviceCount = _sdk.GetControllerCount();
            var devices = _sdk.GetAllControllerData();

            int i = 0;
            foreach (OpenRGB.NET.Models.Device device in devices)
            {
                _devices.Add(new CorsairDevice(device, _logger, i, _sdk));
                i++;
            }

            /*foreach (var device in Devices)
            {
                foreach (var light in device.Lights)
                {
                    light.Color = Color.FromArgb(255, 0, 0);
                }

                device.ApplyLights();
            }*/
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck() { }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl() { }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl() { }
    }
}
