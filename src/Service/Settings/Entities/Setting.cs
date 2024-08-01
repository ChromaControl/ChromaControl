// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChromaControl.Service.Settings.Entities;

/// <summary>
/// A setting.
/// </summary>
public class Setting : IEntityTypeConfiguration<Setting>
{
    /// <summary>
    /// The settings module.
    /// </summary>
    public required string Module { get; set; }

    /// <summary>
    /// The setting name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The string type value.
    /// </summary>
    public string? StringValue { get; set; }

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(e => new { e.Module, e.Name });
    }
}
