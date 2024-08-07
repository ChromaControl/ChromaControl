// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Devices.Services;

namespace ChromaControl.Service.Devices;

/// <summary>
/// Devices extension methods.
/// </summary>
public static class DevicesExtensions
{
    /// <summary>
    /// Adds devices middleware to a <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to add middleware to.</param>
    /// <returns>The <see cref="WebApplication"/> to continue adding middleware to.</returns>
    public static WebApplication UseDevices(this WebApplication app)
    {
        app.MapGrpcService<DevicesService>();

        return app;
    }
}
