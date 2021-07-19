// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Hosting;
using ChromaControl.Providers.Asus;
using Microsoft.Extensions.Hosting;

namespace ChromaControl.Asus
{
    /// <summary>
    /// The main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The program entry point
        /// </summary>
        /// <param name="args">The command line arguments</param>
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .UseChromaControl()
                .UseAsusProvider()
                .RunWithMutex();
        }
    }
}
