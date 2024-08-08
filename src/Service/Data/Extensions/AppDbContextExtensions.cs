// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace ChromaControl.Service.Data.Extensions;

/// <summary>
/// Extension methods for <see cref="AppDbContext"/>.
/// </summary>
public static class AppDbContextExtensions
{
    /// <summary>
    /// Generates a config from a <see cref="AppDbContext"/>.
    /// </summary>
    /// <param name="context">The <see cref="AppDbContext"/>.</param>
    /// <returns>A <see cref="JsonNode"/> config.</returns>
    public static async Task<JsonNode> GenerateConfig(this AppDbContext context)
    {
        var config = JsonNode.Parse("{\"Detectors\":{\"detectors\":{}}}")!;

        var detectors = await context.DeviceDetectors
            .Select(d => new
            {
                d.Name,
                d.Vendor.Enabled
            })
            .ToListAsync();

        foreach (var detector in detectors)
        {
            config["Detectors"]!["detectors"]![detector.Name] = detector.Enabled;
        }

        return config;
    }
}
