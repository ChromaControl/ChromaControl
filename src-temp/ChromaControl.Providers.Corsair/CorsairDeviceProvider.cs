// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ChromaControl.Abstractions;
using CUESDK;

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
        /// Creates a Corsair device provider
        /// </summary>
        public CorsairDeviceProvider()
        {
            _devices = new List<CorsairDevice>();
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(30000);

            for (var i = 0; i < CorsairLightingSDK.GetDeviceCount(); i++)
                _devices.Add(new CorsairDevice(i));
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var cueRunning = Process.GetProcessesByName("iCUE").Length != 0;

            while (!cueRunning)
            {
                Thread.Sleep(1000);
                cueRunning = Process.GetProcessesByName("iCUE").Length != 0;
            }

            CorsairLightingSDK.GetDeviceCount();
            var error = CorsairLightingSDK.GetLastError();

            while (error == CorsairError.ServerNotFound || error == CorsairError.ProtocolHandshakeMissing)
            {
                CorsairLightingSDK.PerformProtocolHandshake();
                Thread.Sleep(1000);
                error = CorsairLightingSDK.GetLastError();
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            CorsairLightingSDK.RequestControl(CorsairAccessMode.ExclusiveLightingControl);
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            CorsairLightingSDK.ReleaseControl(CorsairAccessMode.ExclusiveLightingControl);
        }
    }
}
