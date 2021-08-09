// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChromaControl.Providers.Corsair
{
    /// <summary>
    /// Corsair provider extensions
    /// </summary>
    public static class CorsairProviderExtensions
    {
        /// <summary>
        /// Uses the Corsair device provider
        /// </summary>
        /// <param name="hostBuilder">The host builder</param>
        /// <returns>The host builder</returns>
        public static IHostBuilder UseCorsairProvider(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton(typeof(IDeviceProvider), typeof(CorsairDeviceProvider));
            });
        }
    }
}
