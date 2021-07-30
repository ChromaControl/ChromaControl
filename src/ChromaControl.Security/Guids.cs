// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Windows.Storage;

namespace ChromaControl.Security
{
    /// <summary>
    /// Secure GUIDs class
    /// </summary>
    public static class Guids
    {
        /// <summary>
        /// Gets a secure GUID
        /// </summary>
        /// <param name="name">The name of the guid to get</param>
        /// <returns>The guid</returns>
        public static Guid GetSecureGuid(string name)
        {
            var storageFile = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/ChromaControl.dat")).AsTask().GetAwaiter().GetResult();

            var guids = new Dictionary<string, Guid>();

            using var fileStream = storageFile.OpenStreamForReadAsync().GetAwaiter().GetResult();
            using var aes = Aes.Create();

            var key = new byte[32];
            var iv = new byte[16];

            fileStream.Read(key, 0, key.Length);
            fileStream.Read(iv, 0, iv.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            using var binReader = new BinaryReader(cryptoStream);

            var count = binReader.ReadInt32();

            for (var i = 0; i < count; i++)
                guids.Add(binReader.ReadString(), Guid.Parse(binReader.ReadString()));

            return guids[name];
        }
    }
}
