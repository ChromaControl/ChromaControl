// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.



using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using ChromaControl.Abstractions;
using LedCSharp;


namespace ChromaControl.Providers.GHUB
{

    /// <summary>
    /// The GHUB device provider
    /// </summary>
    public class GHUBDeviceProvider : IDeviceProvider
    {


        /// <summary>
        /// The provider name
        /// </summary>
        public string Name => "Gigabyte";

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices => _devices;

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private readonly List<GHUBDevice> _devices;

        /// <summary>
        /// Creates a GHUB device provider
        /// </summary>
        public GHUBDeviceProvider()
        {
            _devices = new List<GHUBDevice>();
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        /// 

        public void Initialize()
        {
            PerformHealthCheck();

            try
            {
                Console.WriteLine("Init logitech SDK: " + LogitechGSDK.LogiLedInit().ToString());
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine("DLL NOT FOUND, This SDK will not work without it");
            }

            Thread.Sleep(1000);

            int major = 0;
            int minor = 0;
            int build = 0;

            LogitechGSDK.LogiLedGetSdkVersion(ref major, ref minor, ref build);
            Console.WriteLine("Logitec SDK Version " + major.ToString() + "." + minor.ToString() + "." + build.ToString());

            // Just add all device types. TODO check on the OS which devices were found and act accordingly
            _devices.Add(new GHUBDevice("Logitech Keyboard", LedCSharp.DeviceType.Keyboard));
            _devices.Add(new GHUBDevice("Logitech Headset", LedCSharp.DeviceType.Headset));
            _devices.Add(new GHUBDevice("Logitech Speaker", LedCSharp.DeviceType.Speaker));
            _devices.Add(new GHUBDevice("Logitech Mouse", LedCSharp.DeviceType.Mouse));
            _devices.Add(new GHUBDevice("Logitech MouseMat", LedCSharp.DeviceType.Mousemat));
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            Console.WriteLine("Checking process");
            var GHUBRunning = Process.GetProcessesByName("LGHUB").Length != 0;
            while (!GHUBRunning)
            {
                Console.WriteLine("Process was not found, check again after 10 seconds");
                Thread.Sleep(10000);
                GHUBRunning = Process.GetProcessesByName("LGHUB").Length != 0;
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            // Could not find a technique to exclusively get control over the GHUB devcies
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            // Could not find a technique to exclusively get control over the GHUB devcies
        }
    }
}
