// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ChromaBroadcast;
using ChromaControl.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Hosting
{
    /// <summary>
    /// A Chroma Control module service
    /// </summary>
    public class ModuleService : BackgroundService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ModuleService> _logger;

        /// <summary>
        /// The device provider
        /// </summary>
        private readonly IDeviceProvider _deviceProvider;

        /// <summary>
        /// Creates the module service
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="deviceProvider">The device provider</param>
        public ModuleService(ILogger<ModuleService> logger, IDeviceProvider deviceProvider)
        {
            _logger = logger;
            _deviceProvider = deviceProvider;
        }

        /// <summary>
        /// Executes the module service
        /// </summary>
        /// <param name="stoppingToken">The stopping token</param>
        /// <returns>A task</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1, stoppingToken);

            if (_deviceProvider == null)
            {
                _logger.LogError("No Device Provider Set, Cannot Initialize!!!");
                return;
            }

            var version = Assembly.GetEntryAssembly().GetName().Version.ToString().Substring(0, 5);

            _logger.LogInformation($"Initializing Chroma Control - {_deviceProvider.Name} v{version}...");
            _logger.LogInformation("Initializing Device Provider...");

            _deviceProvider.Initialize();
            _deviceProvider.RequestControl();

            foreach (var device in _deviceProvider.Devices)
            {
                _logger.LogInformation($"Found Device: {device.Name} - {device.Lights.Count()} Lights");
            }

            _logger.LogInformation("Initializing Razer Chroma Broadcast SDK...");

            RzChromaBroadcastAPI.Init(_deviceProvider.GetGuid());

            RzChromaBroadcastAPI.RegisterEventNotification(OnChromaBroadcastEvent);

            _logger.LogInformation($"Chroma Control - {_deviceProvider.Name} Started Succesfully!");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            RzChromaBroadcastAPI.UnRegisterEventNotification();
            RzChromaBroadcastAPI.UnInit();
        }

        /// <summary>
        /// Occurs when a callback is recieved from the Chroma Broadcast API
        /// </summary>
        /// <param name="type">The callback type</param>
        /// <param name="status">Status data</param>
        /// <param name="effect">Effect data</param>
        /// <returns>A razer result</returns>
        private RzResult OnChromaBroadcastEvent(RzChromaBroadcastType type, RzChromaBroadcastStatus? status, RzChromaBroadcastEffect? effect)
        {
            if (type == RzChromaBroadcastType.BroadcastEffect)
            {
                var currentColor = 0;

                foreach (var device in _deviceProvider.Devices)
                {
                    foreach (var light in device.Lights)
                    {
                        var color = currentColor switch
                        {
                            0 => effect.Value.ChromaLink1,
                            1 => effect.Value.ChromaLink2,
                            2 => effect.Value.ChromaLink3,
                            3 => effect.Value.ChromaLink4,
                            4 => effect.Value.ChromaLink5,
                            _ => Color.FromArgb(0, 0, 0)
                        };

                        if (currentColor == 4)
                            currentColor = 0;
                        else
                            currentColor++;

                        light.Color = color;
                    }

                    device.ApplyLights();
                }
            }
            else if (type == RzChromaBroadcastType.BroadcastStatus)
            {
                if (status == RzChromaBroadcastStatus.Live)
                    _logger.LogInformation("Razer Chroma Broadcast API Connected");
                else if (status == RzChromaBroadcastStatus.NotLive)
                    _logger.LogInformation("Razer Chroma Broadcast API Disconnected");
            }

            return RzResult.Success;
        }
    }
}
