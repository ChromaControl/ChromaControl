// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Reflection;

namespace ChromaControl.Common.Extensions;

/// <summary>
/// Contains a set of constants representing default configurations.
/// </summary>
public static class ChromaControlConstants
{
    /// <summary>
    /// The path to the data directory.
    /// </summary>
    public static readonly string DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ChromaControl");

    /// <summary>
    /// The path to the log directory.
    /// </summary>
    public static readonly string LogDirectory = Path.Combine(DataDirectory, "logs");

    /// <summary>
    /// The path to the chroma control socket.
    /// </summary>
    public static readonly string SocketPath = Path.Combine(DataDirectory, "ChromaControl.sock");

    /// <summary>
    /// The executing assembly name.
    /// </summary>
    public static readonly string ExecutingAssemblyName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly()!.Location);
}
