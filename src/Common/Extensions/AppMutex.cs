// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaControl.Common.Extensions;

/// <summary>
/// An app mutex.
/// </summary>
public class AppMutex
{
    /// <summary>
    /// The app mutex.
    /// </summary>
    public Mutex Mutex { get; }

    /// <summary>
    /// If the mutex creation was successful.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Creates a app mutex.
    /// </summary>
    public AppMutex()
    {
        Mutex = new Mutex(true, ChromaControlConstants.ExecutingAssemblyName, out var result);
        Success = result;
    }
}
