// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Reflection;
using System.Threading;

namespace ChromaControl.Hosting
{
    /// <summary>
    /// A module mutex
    /// </summary>
    public class ModuleMutex
    {
        /// <summary>
        /// The modules mutex
        /// </summary>
        private readonly Mutex _mutex;

        /// <summary>
        /// If the mutex creation was successful
        /// </summary>
        private readonly bool _success;

        public Mutex Mutex => _mutex;

        /// <summary>
        /// If the mutex creation was successful
        /// </summary>
        public bool Success => _success;

        /// <summary>
        /// Creates a module mutex
        /// </summary>
        public ModuleMutex()
        {
            _mutex = new Mutex(true, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location), out var result);
            _success = result;
        }
    }
}
