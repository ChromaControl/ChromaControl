// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ChromaControl.Abstractions;
using ChromaControl.Security;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Windows.Storage;

namespace ChromaControl.Hosting
{
    /// <summary>
    /// Chroma Control host builder extensions
    /// </summary>
    public static class ChromaControlHostBuilderExtensions
    {
        /// <summary>
        /// Allocates a console window
        /// </summary>
        [DllImport("kernel32")]
        private static extern void AllocConsole();

        /// <summary>
        /// Configures the host builder for Chroma Control
        /// </summary>
        /// <param name="hostBuilder">The host builder</param>
        /// <returns>The host builder</returns>
        public static IHostBuilder UseChromaControl(this IHostBuilder hostBuilder)
        {
            var debugMode = ApplicationData.Current.LocalSettings.Values["DebugMode"];

            if (debugMode != null && (bool)debugMode)
            {
                AllocConsole();
                Console.Title = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
            }

            var logFolder = Path.Combine(ApplicationData.Current.LocalFolder.Path, "logs");

            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            return hostBuilder
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                .ConfigureLogging((hostingContext, logging) =>
                {
                    // Disabling logging due to IOException, this only started in 0.1.2 which is weird.
                    //logging.AddFile(Path.Combine(logFolder, $"{Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)}.log"), append: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ModuleService>();
                });
        }

        /// <summary>
        /// Configures the host to use app center
        /// </summary>
        /// <param name="hostBuilder">The host builder</param>
        /// <returns>The host builder</returns>
        public static IHostBuilder UseAppCenter(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureServices((hostContext, services) =>
                {
#if !DEBUG
                    var sp = services.BuildServiceProvider();
                    var deviceProvider = sp.GetService<IDeviceProvider>();

                    AppCenter.Start(Guids.GetSecureGuid($"ChromaControl.{deviceProvider.Name}.AppCenter").ToString(), typeof(Analytics), typeof(Crashes));
#endif
                });
        }

        /// <summary>
        /// Builds and runs the host with a mutex
        /// </summary>
        /// <param name="hostBuilder">The host builder</param>
        public static void RunWithMutex(this IHostBuilder hostBuilder)
        {
            var mutex = new ModuleMutex();

            if (mutex.Success)
                hostBuilder.ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(mutex);
                }).Build().Run();
        }
    }
}
