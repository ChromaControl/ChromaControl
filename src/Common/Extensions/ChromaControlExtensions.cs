// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using ChromaControl.Common.Keys;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// Extension methods for Chroma Control.
/// </summary>
public static class ChromaControlExtensions
{
    /// <summary>
    /// Allocates a console window.
    /// </summary>
    [DllImport("kernel32")]
    private static extern void AllocConsole();

    /// <summary>
    /// Adds Chroma Control services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
    /// <returns>The <see cref="IServiceCollection"/> to allow for method chaining.</returns>
    /// <param name="args">The command line arguments.</param>
    public static IServiceCollection AddChromaControl(this IServiceCollection services, string[] args)
    {
        if (Debugger.IsAttached || args.Contains("--debug"))
        {
            AllocConsole();
            Console.Title = ChromaControlConstants.ExecutingAssemblyName;
        }

        if (!Directory.Exists(ChromaControlConstants.DataDirectory))
        {
            Directory.CreateDirectory(ChromaControlConstants.DataDirectory);
        }

        var mutex = new AppMutex();

        if (mutex.Success)
        {
            services.AddSingleton(mutex);
        }
        else
        {
            Environment.Exit(0);
        }

        if (!Debugger.IsAttached)
        {
            AppCenter.Start(KeyData.AppCenterKey, typeof(Analytics), typeof(Crashes));
        }

        return services;
    }

    /// <summary>
    /// Adds Chroma Control logging to the specified <see cref="ILoggingBuilder"/>.
    /// </summary>
    /// <param name="logging">The <see cref="ILoggingBuilder"/> for adding logging.</param>
    /// <returns>The <see cref="ILoggingBuilder"/> to allow for method chaining.</returns>
    public static ILoggingBuilder AddChromaControl(this ILoggingBuilder logging)
    {
        if (!Directory.Exists(ChromaControlConstants.LogDirectory))
        {
            Directory.CreateDirectory(ChromaControlConstants.LogDirectory);
        }

        logging.AddFile(Path.Combine(ChromaControlConstants.LogDirectory, $"{ChromaControlConstants.ExecutingAssemblyName}.log"), true);

        return logging;
    }
}
