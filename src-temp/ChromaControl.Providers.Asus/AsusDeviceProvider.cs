// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AuraServiceLib;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Providers.Asus
{
    /// <summary>
    /// The Asus device provider
    /// </summary>
    public class AsusDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name => "Asus";

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices => _devices;

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private readonly List<AsusDevice> _devices;

        /// <summary>
        /// The provider sdk
        /// </summary>
        private readonly IAuraSdk _sdk;

        /// <summary>
        /// If the provider is in exclusive mode
        /// </summary>
        private readonly bool _inExclusiveMode;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<AsusDeviceProvider> _logger;

        /// <summary>
        /// Creates an Asus device provider
        /// </summary>
        /// <param name="logger">The logger</param>
        public AsusDeviceProvider(ILogger<AsusDeviceProvider> logger)
        {
            _devices = new List<AsusDevice>();
            _sdk = new AuraSdk();
            _inExclusiveMode = false;
            _logger = logger;
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            Thread.Sleep(30000);

            foreach (IAuraSyncDevice device in _sdk.Enumerate(0))
                _devices.Add(new AsusDevice(device, _logger));
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;

            while (!lightingServiceRunning)
            {
                Thread.Sleep(1000);
                lightingServiceRunning = Process.GetProcessesByName("LightingService").Length != 0;
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            if (!_inExclusiveMode)
                _sdk.SwitchMode();
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            if (_inExclusiveMode)
                _sdk.SwitchMode();
        }
    }
}
