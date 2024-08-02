// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Devices.Entities;
using ChromaControl.Service.Settings.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Data;

/// <summary>
/// The app database context.
/// </summary>
public class AppDbContext : DbContext
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
    /// Creates an <see cref="AppDbContext"/> instance.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{TContext}"/>.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
