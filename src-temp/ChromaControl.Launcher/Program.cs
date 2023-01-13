// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.IO;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.Storage;

namespace ChromaControl.Launcher
{
    /// <summary>
    /// The main program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The package directory
        /// </summary>
        static string PackageDirectory => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\");

        /// <summary>
        /// The program entry point
        /// </summary>
        /// <param name="args">The command line arguments</param>
        public static void Main(string[] args)
        {
            if (args.Length > 2)
                ToggleModule(args[2]);
        }

        /// <summary>
        /// Toggles a module on or off
        /// </summary>
        /// <param name="moduleName">The module name</param>
        public static void ToggleModule(string moduleName)
        {
            var startupTask = StartupTask.GetAsync(moduleName).GetResults();
            var executableName = $"ChromaControl.{moduleName}";

            if ((string)ApplicationData.Current.LocalSettings.Values["LauncherCommand"] == "Enable")
            {
                if (startupTask.State == StartupTaskState.Disabled)
                    _ = startupTask.RequestEnableAsync();

                Process.Start(Path.Combine(PackageDirectory, $"{executableName}\\{executableName}.exe"));
            }
            else
            {
                startupTask.Disable();

                foreach (var process in Process.GetProcessesByName(executableName))
                    process.Kill();
            }
        }
    }
}
