// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChromaControl.Service.Settings.Entities;

/// <summary>
/// A device vendor.
/// </summary>
public class DeviceVendor : IEntityTypeConfiguration<DeviceVendor>
{
    /// <summary>
    /// The vendor id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The vendor name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// If this vendor is hidden from users.
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// If this vendor is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// If this vendor is dangerous to enable.
    /// </summary>
    public bool Dangerous { get; set; }

    /// <summary>
    /// The device detectors.
    /// </summary>
    public ICollection<DeviceDetector> Detectors { get; set; } = [];

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DeviceVendor> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.HasData([
            new() { Id = 1, Name = "Internal", Hidden = true },
            new() { Id = 2, Name = "Razer", Hidden = true },
            new() { Id = 3, Name = "ASUS" },
            new() { Id = 4, Name = "Corsair" },
            new() { Id = 5, Name = "Gigabyte" },
            new() { Id = 6, Name = "MSI", Dangerous = true },
            new() { Id = 7, Name = "EVGA" },
            new() { Id = 8, Name = "Logitech" },
            new() { Id = 9, Name = "Lian Li" },
            new() { Id = 10, Name = "SteelSeries" },
            new() { Id = 11, Name = "Thermaltake" },
            new() { Id = 12, Name = "PNY" },
            new() { Id = 13, Name = "Cooler Master" },
            new() { Id = 14, Name = "NZXT" },
            new() { Id = 15, Name = "Lenovo" },
            new() { Id = 16, Name = "Sapphire" },
            new() { Id = 17, Name = "ZOTAC" },
            new() { Id = 18, Name = "Palit" },
            new() { Id = 19, Name = "Roccat" },
            new() { Id = 20, Name = "Wooting" },
            new() { Id = 21, Name = "Cherry" },
            new() { Id = 22, Name = "Glorious" },
            new() { Id = 23, Name = "HyperX" },
            new() { Id = 24, Name = "Colorful" },
            new() { Id = 25, Name = "Redragon" },
            new() { Id = 26, Name = "Gainward" },
            new() { Id = 27, Name = "Alienware" },
            new() { Id = 28, Name = "ASRock" },
            new() { Id = 29, Name = "AOC" },
            new() { Id = 30, Name = "Arctic" },
            new() { Id = 31, Name = "HYTE" },
            new() { Id = 32, Name = "Zalman" },
            new() { Id = 33, Name = "Cougar" },
            new() { Id = 34, Name = "Creative" },
            new() { Id = 35, Name = "Crucial" },
            new() { Id = 36, Name = "Elgato" },
            new() { Id = 37, Name = "EVision" },
            new() { Id = 38, Name = "EKWB" },
            new() { Id = 39, Name = "GaiZhongGai" },
            new() { Id = 40, Name = "DRGB" },
            new() { Id = 41, Name = "Red Square" },
            new() { Id = 42, Name = "ZET" },
            new() { Id = 43, Name = "Nollie" },
            new() { Id = 44, Name = "Ducky" },
            new() { Id = 45, Name = "Das Keyboard" },
            new() { Id = 46, Name = "Epomaker" },
            new() { Id = 47, Name = "GALAX" },
            new() { Id = 48, Name = "ViewSonic" },
            new() { Id = 49, Name = "Philips" },
            new() { Id = 50, Name = "Seagate" },
            new() { Id = 51, Name = "NVIDIA" },
            new() { Id = 52, Name = "Sony" },
            new() { Id = 53, Name = "Patriot" },
            new() { Id = 54, Name = "SRGBMods" },
            new() { Id = 55, Name = "Valkyrie" },
            new() { Id = 56, Name = "Trust" },
            new() { Id = 57, Name = "LIFX" },
            new() { Id = 58, Name = "LG" },
            new() { Id = 59, Name = "Kingston" },
            new() { Id = 60, Name = "Intel" },
            new() { Id = 61, Name = "KFA2" },
            new() { Id = 62, Name = "PC Specialist" },
            new() { Id = 63, Name = "Holtek" },
            new() { Id = 64, Name = "Genesis" },
            new() { Id = 65, Name = "XPG" },
            new() { Id = 66, Name = "TP-Link" },
            new() { Id = 67, Name = "Lego" },
            new() { Id = 68, Name = "A4Tech" },
            new() { Id = 69, Name = "AMD" },
            new() { Id = 70, Name = "Acer" },
            new() { Id = 71, Name = "Anko" },
            new() { Id = 72, Name = "Advance" },
            new() { Id = 73, Name = "HP" },
            new() { Id = 74, Name = "Dell" },
            new() { Id = 75, Name = "Endorfy" },
            new() { Id = 76, Name = "Hexcore" },
            new() { Id = 77, Name = "LightSalt" },
            new() { Id = 78, Name = "Dygma" },
            new() { Id = 79, Name = "Yeelight" },
            new() { Id = 80, Name = "Winbond" },
            new() { Id = 81, Name = "ThingM" },
            new() { Id = 82, Name = "Tecknet" },
            new() { Id = 83, Name = "Skyloong" },
            new() { Id = 84, Name = "OKS" },
            new() { Id = 85, Name = "Lexip" },
            new() { Id = 86, Name = "Nanoleaf" },
            new() { Id = 87, Name = "Mountain" },
            new() { Id = 88, Name = "Keychron" },
            new() { Id = 89, Name = "JSAUX" },
            new() { Id = 90, Name = "JGINYUE" },
            new() { Id = 91, Name = "PNC Partner" },
            new() { Id = 92, Name = "Espurna" },
            new() { Id = 93, Name = "Dark Project" },
            new() { Id = 94, Name = "DMX" },
            new() { Id = 95, Name = "CRYORIG" },
            new() { Id = 96, Name = "Blinkinlabs" },
            new() { Id = 97, Name = "Attack Shark" },
            new() { Id = 98, Name = "FanBus" },
            new() { Id = 99, Name = "E1.31" },
            new() { Id = 100, Name = "N5312A" },
            new() { Id = 101, Name = "LED Strip" },
            new() { Id = 102, Name = "ENE DRAM" }
        ]);
    }
}
