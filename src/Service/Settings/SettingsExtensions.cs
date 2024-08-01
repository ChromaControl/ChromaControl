// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Settings.Services;

namespace ChromaControl.Service.Settings;

/// <summary>
/// Settings extension methods.
/// </summary>
public static class SettingsExtensions
{
    /// <summary>
    /// Adds settings middleware to a <see cref="WebApplication"/>.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to add middleware to.</param>
    /// <returns>The <see cref="WebApplication"/> to continue adding middleware to.</returns>
    public static WebApplication UseSettings(this WebApplication app)
    {
        app.MapGrpcService<SettingsService>();

        return app;
    }
}
