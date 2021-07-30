// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

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
        /// Gets the device provider guid
        /// </summary>
        /// <returns>The guid</returns>
        Guid GetGuid()
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "ChromaControl.dat");

            using var fileStream = new FileStream(filePath, FileMode.Open);
            using var aes = Aes.Create();

            var key = new byte[32];
            var iv = new byte[16];

            fileStream.Read(key, 0, key.Length);
            fileStream.Read(iv, 0, iv.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            using var binReader = new BinaryReader(cryptoStream);

            var count = binReader.ReadInt32();

            for (int i = 0; i < count; i++)
            {
                if (binReader.ReadString() == Name)
                    return Guid.Parse(binReader.ReadString());
                else
                    binReader.ReadString();
            }

            return Guid.Empty;
        }

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
