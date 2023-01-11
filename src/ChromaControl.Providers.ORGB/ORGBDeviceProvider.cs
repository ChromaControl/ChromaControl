// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Logging;
using OpenRGB.NET;

namespace ChromaControl.Providers.ORGB
{
    /// <summary>
    /// The ORGB device provider
    /// </summary>
    public class ORGBDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name => "ORGB";

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices => _devices;

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private readonly List<ORGBDevice> _devices;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private OpenRGBClient _sdk;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private bool _sdkInitialized;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ORGBDeviceProvider> _logger;

        /// <summary>
        /// Creates an ORGB device provider
        /// </summary>
        /// <param name="logger">The logger</param>
        public ORGBDeviceProvider(ILogger<ORGBDeviceProvider> logger)
        {
            _logger = logger;
            _devices = new List<ORGBDevice>();
            _sdkInitialized = false;
            /*try
            {
                _logger.LogInformation("Connecting to OpenRGB...");
                _sdk = new OpenRGBClient(name: "Chroma Control OpenRGB Client", autoconnect: true, timeout: 1000);
                _sdkInitialized = true;
            }
            catch (TimeoutException ex)
            {
                //_logger.LogError(ex, "Can't connect to OpenRGB");
                _logger.LogInformation("Can't connect to OpenRGB. Retrying in 3 seconds...");
            }*/
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            /*PerformHealthCheck();

            Thread.Sleep(30000);*/

            PerformHealthCheck();

            Thread.Sleep(3000);

            var deviceCount = _sdk.GetControllerCount();
            var devices = _sdk.GetAllControllerData();

            int i = 0;
            foreach (OpenRGB.NET.Models.Device device in devices)
            {
                _devices.Add(new ORGBDevice(device, _logger, i, _sdk));
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
        public void PerformHealthCheck()
        {
            while (!_sdkInitialized)
            {
                Thread.Sleep(5000);
                try
                {
                    _logger.LogInformation("Connecting to OpenRGB...");
                    _sdk = new OpenRGBClient(name: "Chroma Control OpenRGB Client", autoconnect: true, timeout: 1000);
                    _sdkInitialized = true;
                }
                catch(TimeoutException ex)
                {
                    //_logger.LogError(ex, "Can't connect to OpenRGB");
                    _logger.LogInformation("Can't connect to OpenRGB. Retrying in 3 seconds...");
                }
            }
        }

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
