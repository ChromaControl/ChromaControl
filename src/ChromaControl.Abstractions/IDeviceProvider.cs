// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace ChromaControl.Abstractions
{
    /// <summary>
    /// A device provider interface
    /// </summary>
    public interface IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        IEnumerable<IDevice> Devices { get; }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        void Initialize();

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        void PerformHealthCheck();

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        void RequestControl();

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        void ReleaseControl();
    }
}
