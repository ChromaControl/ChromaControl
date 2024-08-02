// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Settings.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Data;

/// <summary>
/// The database.
/// </summary>
public class Database : DbContext
{
    /// <summary>
    /// The settings.
    /// </summary>
    public DbSet<Setting> Settings => Set<Setting>();

    /// <summary>
    /// The device vendors.
    /// </summary>
    public DbSet<DeviceVendor> DeviceVendors => Set<DeviceVendor>();

    /// <summary>
    /// The device detectors.
    /// </summary>
    public DbSet<DeviceDetector> DeviceDetectors => Set<DeviceDetector>();

    /// <summary>
    /// Creates a <see cref="Database"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{TContext}"/>.</param>
    public Database(DbContextOptions<Database> options) : base(options) { }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Database).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
