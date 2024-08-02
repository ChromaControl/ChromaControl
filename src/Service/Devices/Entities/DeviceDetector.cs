// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChromaControl.Service.Settings.Entities;

/// <summary>
/// A device detector.
/// </summary>
public class DeviceDetector : IEntityTypeConfiguration<DeviceDetector>
{
    /// <summary>
    /// The vendor id.
    /// </summary>
    public int VendorId { get; set; }

    /// <summary>
    /// The vendor.
    /// </summary>
    public DeviceVendor Vendor { get; set; } = default!;

    /// <summary>
    /// The detector name.
    /// </summary>
    public required string Name { get; set; }

    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DeviceDetector> builder)
    {
        builder.HasKey(e => new { e.VendorId, e.Name });

        builder.HasOne(e => e.Vendor)
            .WithMany(e => e.Detectors)
            .HasPrincipalKey(e => e.Id);

        builder.HasData([
            // -------------------------
            // Internal
            // -------------------------
            new() { VendorId = 1, Name = "Debug Controllers" },

            // -------------------------
            // Razer
            // -------------------------
            new() { VendorId = 2, Name = "Lian Li O11 Dynamic - Razer Edition" },
            new() { VendorId = 2, Name = "Razer Abyssus Elite D.Va Edition" },
            new() { VendorId = 2, Name = "Razer Abyssus Essential" },
            new() { VendorId = 2, Name = "Razer Base Station Chroma" },
            new() { VendorId = 2, Name = "Razer Base Station V2 Chroma" },
            new() { VendorId = 2, Name = "Razer Basilisk" },
            new() { VendorId = 2, Name = "Razer Basilisk Essential" },
            new() { VendorId = 2, Name = "Razer Basilisk Ultimate (Wired)" },
            new() { VendorId = 2, Name = "Razer Basilisk Ultimate (Wireless)" },
            new() { VendorId = 2, Name = "Razer Basilisk V2" },
            new() { VendorId = 2, Name = "Razer Basilisk V3" },
            new() { VendorId = 2, Name = "Razer Basilisk V3 Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Basilisk V3 Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Basilisk V3 X HyperSpeed" },
            new() { VendorId = 2, Name = "Razer Blackwidow 2019" },
            new() { VendorId = 2, Name = "Razer Blackwidow Chroma" },
            new() { VendorId = 2, Name = "Razer Blackwidow Chroma Tournament Edition" },
            new() { VendorId = 2, Name = "Razer Blackwidow Chroma V2" },
            new() { VendorId = 2, Name = "Razer Blackwidow Elite" },
            new() { VendorId = 2, Name = "Razer Blackwidow Overwatch" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3 Mini (Wired)" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3 Mini (Wireless)" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3 Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3 Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Blackwidow V3 TKL" },
            new() { VendorId = 2, Name = "Razer Blackwidow V4" },
            new() { VendorId = 2, Name = "Razer Blackwidow V4 Pro" },
            new() { VendorId = 2, Name = "Razer Blackwidow V4 X" },
            new() { VendorId = 2, Name = "Razer Blackwidow X Chroma" },
            new() { VendorId = 2, Name = "Razer Blackwidow X Chroma Tournament Edition" },
            new() { VendorId = 2, Name = "Razer Blade (2016)" },
            new() { VendorId = 2, Name = "Razer Blade (Late 2016)" },
            new() { VendorId = 2, Name = "Razer Blade 14 (2021)" },
            new() { VendorId = 2, Name = "Razer Blade 14 (2022)" },
            new() { VendorId = 2, Name = "Razer Blade 14 (2023)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2018 Advanced)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2018 Base)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2018 Mercury)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2019 Advanced)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2019 Base)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2019 Mercury)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2019 Studio)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2020 Advanced)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2020 Base)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2021 Advanced)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2021 Base)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (2022)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (Late 2020)" },
            new() { VendorId = 2, Name = "Razer Blade 15 (Late 2021 Advanced)" },
            new() { VendorId = 2, Name = "Razer Blade Pro (2016)" },
            new() { VendorId = 2, Name = "Razer Blade Pro (2017 FullHD)" },
            new() { VendorId = 2, Name = "Razer Blade Pro (2017)" },
            new() { VendorId = 2, Name = "Razer Blade Pro (2019)" },
            new() { VendorId = 2, Name = "Razer Blade Pro (Late 2019)" },
            new() { VendorId = 2, Name = "Razer Blade Pro 17 (2020)" },
            new() { VendorId = 2, Name = "Razer Blade Pro 17 (2021)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (2016)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (2017)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (2019)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (2020)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (Late 2016)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (Late 2017)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (Late 2019)" },
            new() { VendorId = 2, Name = "Razer Blade Stealth (Late 2020)" },
            new() { VendorId = 2, Name = "Razer Book 13 (2020)" },
            new() { VendorId = 2, Name = "Razer Charging Pad Chroma" },
            new() { VendorId = 2, Name = "Razer Chroma Addressable RGB Controller" },
            new() { VendorId = 2, Name = "Razer Chroma HDK" },
            new() { VendorId = 2, Name = "Razer Chroma Mug Holder" },
            new() { VendorId = 2, Name = "Razer Chroma PC Case Lighting Kit" },
            new() { VendorId = 2, Name = "Razer Cobra" },
            new() { VendorId = 2, Name = "Razer Cobra Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Cobra Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Core" },
            new() { VendorId = 2, Name = "Razer Core X" },
            new() { VendorId = 2, Name = "Razer Cynosa Chroma" },
            new() { VendorId = 2, Name = "Razer Cynosa Chroma V2" },
            new() { VendorId = 2, Name = "Razer Cynosa Lite" },
            new() { VendorId = 2, Name = "Razer Deathadder Chroma" },
            new() { VendorId = 2, Name = "Razer Deathadder Elite" },
            new() { VendorId = 2, Name = "Razer Deathadder Essential" },
            new() { VendorId = 2, Name = "Razer Deathadder Essential V2" },
            new() { VendorId = 2, Name = "Razer Deathadder Essential White Edition" },
            new() { VendorId = 2, Name = "Razer Deathadder V2" },
            new() { VendorId = 2, Name = "Razer Deathadder V2 Mini" },
            new() { VendorId = 2, Name = "Razer Deathadder V2 Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Deathadder V2 Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Deathstalker Chroma" },
            new() { VendorId = 2, Name = "Razer Deathstalker V2" },
            new() { VendorId = 2, Name = "Razer Deathstalker V2 Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Deathstalker V2 Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Deathstalker V2 Pro TKL (Wired)" },
            new() { VendorId = 2, Name = "Razer Deathstalker V2 Pro TKL (Wireless)" },
            new() { VendorId = 2, Name = "Razer Diamondback" },
            new() { VendorId = 2, Name = "Razer Firefly" },
            new() { VendorId = 2, Name = "Razer Firefly Hyperflux" },
            new() { VendorId = 2, Name = "Razer Firefly V2" },
            new() { VendorId = 2, Name = "Razer Goliathus" },
            new() { VendorId = 2, Name = "Razer Goliathus Chroma 3XL" },
            new() { VendorId = 2, Name = "Razer Goliathus Extended" },
            new() { VendorId = 2, Name = "Razer Huntsman" },
            new() { VendorId = 2, Name = "Razer Huntsman Elite" },
            new() { VendorId = 2, Name = "Razer Huntsman Mini" },
            new() { VendorId = 2, Name = "Razer Huntsman Mini Analog" },
            new() { VendorId = 2, Name = "Razer Huntsman Tournament Edition" },
            new() { VendorId = 2, Name = "Razer Huntsman V2" },
            new() { VendorId = 2, Name = "Razer Huntsman V2 Analog" },
            new() { VendorId = 2, Name = "Razer Huntsman V2 TKL" },
            new() { VendorId = 2, Name = "Razer Kraken 7.1" },
            new() { VendorId = 2, Name = "Razer Kraken 7.1 Chroma" },
            new() { VendorId = 2, Name = "Razer Kraken 7.1 V2" },
            new() { VendorId = 2, Name = "Razer Kraken Kitty Black Edition" },
            new() { VendorId = 2, Name = "Razer Kraken Kitty Black Edition V2" },
            new() { VendorId = 2, Name = "Razer Kraken Kitty Edition" },
            new() { VendorId = 2, Name = "Razer Kraken Ultimate" },
            new() { VendorId = 2, Name = "Razer Lancehead 2017 (Wired)" },
            new() { VendorId = 2, Name = "Razer Lancehead 2017 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Lancehead 2019 (Wired)" },
            new() { VendorId = 2, Name = "Razer Lancehead 2019 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Lancehead Tournament Edition" },
            new() { VendorId = 2, Name = "Razer Laptop Stand Chroma" },
            new() { VendorId = 2, Name = "Razer Laptop Stand Chroma V2" },
            new() { VendorId = 2, Name = "Razer Leviathan V2" },
            new() { VendorId = 2, Name = "Razer Leviathan V2 X" },
            new() { VendorId = 2, Name = "Razer Mamba 2012 (Wired)" },
            new() { VendorId = 2, Name = "Razer Mamba 2012 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Mamba 2015 (Wired)" },
            new() { VendorId = 2, Name = "Razer Mamba 2015 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Mamba 2018 (Wired)" },
            new() { VendorId = 2, Name = "Razer Mamba 2018 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Mamba Elite" },
            new() { VendorId = 2, Name = "Razer Mamba Hyperflux (Wired)" },
            new() { VendorId = 2, Name = "Razer Mamba Tournament Edition" },
            new() { VendorId = 2, Name = "Razer Mouse Bungee V3 Chroma" },
            new() { VendorId = 2, Name = "Razer Mouse Dock Chroma" },
            new() { VendorId = 2, Name = "Razer Mouse Dock Pro" },
            new() { VendorId = 2, Name = "Razer Naga Chroma" },
            new() { VendorId = 2, Name = "Razer Naga Classic" },
            new() { VendorId = 2, Name = "Razer Naga Epic Chroma" },
            new() { VendorId = 2, Name = "Razer Naga Hex V2" },
            new() { VendorId = 2, Name = "Razer Naga Left Handed" },
            new() { VendorId = 2, Name = "Razer Naga Pro (Wired)" },
            new() { VendorId = 2, Name = "Razer Naga Pro (Wireless)" },
            new() { VendorId = 2, Name = "Razer Naga Pro V2 (Wired)" },
            new() { VendorId = 2, Name = "Razer Naga Pro V2 (Wireless)" },
            new() { VendorId = 2, Name = "Razer Naga Trinity" },
            new() { VendorId = 2, Name = "Razer Nommo Chroma" },
            new() { VendorId = 2, Name = "Razer Nommo Pro" },
            new() { VendorId = 2, Name = "Razer Orbweaver Chroma" },
            new() { VendorId = 2, Name = "Razer Ornata Chroma" },
            new() { VendorId = 2, Name = "Razer Ornata Chroma V2" },
            new() { VendorId = 2, Name = "Razer Ornata V3" },
            new() { VendorId = 2, Name = "Razer Ornata V3 Rev2" },
            new() { VendorId = 2, Name = "Razer Ornata V3 TKL" },
            new() { VendorId = 2, Name = "Razer Ornata V3 X" },
            new() { VendorId = 2, Name = "Razer Ornata V3 X Rev2" },
            new() { VendorId = 2, Name = "Razer Seiren Emote" },
            new() { VendorId = 2, Name = "Razer Strider Chroma" },
            new() { VendorId = 2, Name = "Razer Tartarus Chroma" },
            new() { VendorId = 2, Name = "Razer Tartarus Pro" },
            new() { VendorId = 2, Name = "Razer Tartarus V2" },
            new() { VendorId = 2, Name = "Razer Thunderbolt 4 Dock Chroma" },
            new() { VendorId = 2, Name = "Razer Tiamat 7.1 V2" },
            new() { VendorId = 2, Name = "Razer Viper" },
            new() { VendorId = 2, Name = "Razer Viper 8kHz" },
            new() { VendorId = 2, Name = "Razer Viper Mini" },
            new() { VendorId = 2, Name = "Razer Viper Ultimate (Wired)" },
            new() { VendorId = 2, Name = "Razer Viper Ultimate (Wireless)" },

            // -------------------------
            // ASUS
            // -------------------------
            new() { VendorId = 3, Name = "ASUS AREZ Strix RX Vega 56 O8G" },
            new() { VendorId = 3, Name = "ASUS Aura Addressable" },
            new() { VendorId = 3, Name = "ASUS Aura Core" },
            new() { VendorId = 3, Name = "ASUS Aura Motherboard" },
            new() { VendorId = 3, Name = "ASUS Aura SMBus Motherboard" },
            new() { VendorId = 3, Name = "ASUS Cerberus Mech" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3060 O12G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3060 OC O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3060 Ti O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3060 Ti O8G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3070 O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS KO RTX 3070 O8G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG AURA Terminal" },
            new() { VendorId = 3, Name = "ASUS ROG Ally" },
            new() { VendorId = 3, Name = "ASUS ROG Arion" },
            new() { VendorId = 3, Name = "ASUS ROG Azoth 2.4GHz" },
            new() { VendorId = 3, Name = "ASUS ROG Azoth USB" },
            new() { VendorId = 3, Name = "ASUS ROG Balteus" },
            new() { VendorId = 3, Name = "ASUS ROG Balteus Qi" },
            new() { VendorId = 3, Name = "ASUS ROG Chakram (Wireless)" },
            new() { VendorId = 3, Name = "ASUS ROG Claymore" },
            new() { VendorId = 3, Name = "ASUS ROG Falchion (Wired)" },
            new() { VendorId = 3, Name = "ASUS ROG Falchion (Wireless)" },
            new() { VendorId = 3, Name = "ASUS ROG GTX 1060 Strix" },
            new() { VendorId = 3, Name = "ASUS ROG GTX 1060 Strix 6G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG GTX 1070 Strix Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG GTX 1070 Strix OC" },
            new() { VendorId = 3, Name = "ASUS ROG GTX 1080 Strix OC" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II Core" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II Origin" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II Origin COD" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II Origin PNK LTD" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius II Wireless" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III Wireless 2.4Ghz" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III Wireless AimPoint 2.4Ghz" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III Wireless AimPoint USB" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III Wireless Bluetooth" },
            new() { VendorId = 3, Name = "ASUS ROG Gladius III Wireless USB" },
            new() { VendorId = 3, Name = "ASUS ROG Keris" },
            new() { VendorId = 3, Name = "ASUS ROG Keris Wireless 2.4Ghz" },
            new() { VendorId = 3, Name = "ASUS ROG Keris Wireless AimPoint 2.4Ghz" },
            new() { VendorId = 3, Name = "ASUS ROG Keris Wireless AimPoint USB" },
            new() { VendorId = 3, Name = "ASUS ROG Keris Wireless Bluetooth" },
            new() { VendorId = 3, Name = "ASUS ROG Keris Wireless USB" },
            new() { VendorId = 3, Name = "ASUS ROG MATRIX PLATINUM RTX 4090 24G" },
            new() { VendorId = 3, Name = "ASUS ROG PG32UQ" },
            new() { VendorId = 3, Name = "ASUS ROG Pugio" },
            new() { VendorId = 3, Name = "ASUS ROG Pugio II (Wired)" },
            new() { VendorId = 3, Name = "ASUS ROG Pugio II (Wireless)" },
            new() { VendorId = 3, Name = "ASUS ROG RTX 3080 10G GUNDAM EDITION" },
            new() { VendorId = 3, Name = "ASUS ROG RX 5600 XT Strix O6G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG RX 570 Strix O4G Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG RX 570 Strix O8G Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG RX 5700 XT Strix 08G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG RX 5700 XT Strix Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG RX 580 Strix Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG RX 580 Strix Gaming TOP" },
            new() { VendorId = 3, Name = "ASUS ROG Ryuo AIO" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3060 12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3060 O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3060 O12G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3060 Ti O8G OC" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3060 Ti O8G V2" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 O8G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 O8G V2 White" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 O8G White" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 OC" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3070 Ti O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 10G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 10G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 O10G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 O10G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 O10G V2 WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 O10G WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3080 Ti O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3090 24G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3090 GUNDAM EDITION" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3090 O24G EVA" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3090 O24G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX 3090 O24G GAMING White OC" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC 3080 Ti O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RTX 3090 Ti O24G OC GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RTX 4090 O24G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RX 6800 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RX 6900 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RX 6900 XT O16G GAMING TOP" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX LC RX 6950 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 EVO Gaming 6G" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 O6G EVO Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 O6G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 SUPER 8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 SUPER A8G EVO Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 SUPER A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2060 SUPER O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2070 A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2070 O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2070 SUPER 8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2070 SUPER A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2070 SUPER O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 O8G V2 Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 SUPER A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 SUPER O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 SUPER O8G White" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 Ti 11G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 Ti A11G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 2080 Ti O11G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 3050 8G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 3080 12G" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 3080 O12G" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 3080 O12G EVA EDITION" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4060 Ti 8G Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4070 O12G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4070 SUPER O12G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4070 Ti O12G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4070 Ti SUPER 16G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4070 Ti SUPER O16G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 16G GAMING White" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 O16G GAMING WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 SUPER 16G GAMING WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 SUPER O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4080 SUPER O16G GAMING WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4090 024G EVA-02" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4090 24G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4090 24G GAMING WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4090 O24G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RTX 4090 O24G GAMING WHITE" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 470 O4G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 480 Gaming OC" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 560 Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 590 Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 6700 XT O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 6750 XT O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS ROG STRIX RX 6800 O16G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Spatha Wired" },
            new() { VendorId = 3, Name = "ASUS ROG Spatha Wireless" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Claw" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Evolve" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Flare" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Flare CoD Black Ops 4 Edition" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Flare II" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Flare II Animate" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Flare PNK LTD" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1050 O2G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1050 Ti 4G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1050 Ti O4G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1070 Ti 8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1070 Ti A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 A8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 O8G 11Gbps" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 O8G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 Ti 11G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 Ti Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1080 Ti O11G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1650 SUPER A4G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1650 SUPER OC 4G" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1660 SUPER 6G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1660 SUPER O6G Gaming" },
            new() { VendorId = 3, Name = "ASUS ROG Strix GTX 1660 Ti OC 6G" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II Electro Punk" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II Gundam" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II Moonlight White" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II Wireless 2.4 Ghz" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact II Wireless USB" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Impact III" },
            new() { VendorId = 3, Name = "ASUS ROG Strix LC" },
            new() { VendorId = 3, Name = "ASUS ROG Strix SCAR 15" },
            new() { VendorId = 3, Name = "ASUS ROG Strix SCAR 17" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope II" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope II 96 Wireless USB" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope II RX" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope NX Wireless Deluxe 2.4GHz" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope NX Wireless Deluxe USB" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope RX" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope RX TKL Wireless Deluxe" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope TKL" },
            new() { VendorId = 3, Name = "ASUS ROG Strix Scope TKL PNK LTD" },
            new() { VendorId = 3, Name = "ASUS ROG Strix XG279Q" },
            new() { VendorId = 3, Name = "ASUS ROG Strix XG27AQ" },
            new() { VendorId = 3, Name = "ASUS ROG Strix XG27AQM" },
            new() { VendorId = 3, Name = "ASUS ROG Strix XG27W" },
            new() { VendorId = 3, Name = "ASUS ROG Strix XG32VC" },
            new() { VendorId = 3, Name = "ASUS ROG Throne" },
            new() { VendorId = 3, Name = "ASUS ROG Throne QI" },
            new() { VendorId = 3, Name = "ASUS ROG Throne QI GUNDAM" },
            new() { VendorId = 3, Name = "ASUS ROG Vega 64 Strix" },
            new() { VendorId = 3, Name = "ASUS RX 6800 TUF Gaming OC" },
            new() { VendorId = 3, Name = "ASUS Sagaris GK1100" },
            new() { VendorId = 3, Name = "ASUS TUF 3060 O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF 3060 O12G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming K1" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming K3" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming K5" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming K7" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming M3" },
            new() { VendorId = 3, Name = "ASUS TUF Gaming M5" },
            new() { VendorId = 3, Name = "ASUS TUF Laptop" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3060 Ti 8G Gaming OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3060 Ti O8G" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3060 Ti O8G OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3070 8G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3070 O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3070 O8G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3070 Ti O8G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3070 Ti O8G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 10G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 O10G OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 O10G V2 GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 Ti 12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3080 Ti O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3090 O24G" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3090 O24G OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3090 Ti 24G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 3090 Ti O24G OC GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4060 Ti 8G Gaming OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 12G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 O12G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 SUPER 12G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti 12G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti O12G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti O12G Gaming White" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti SUPER 16G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti SUPER O16G Gaming" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4070 Ti SUPER O16G Gaming White" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4080 16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4080 O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4080 O16G OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4080 SUPER 16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4080 SUPER O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4090 O24G" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4090 O24G OC" },
            new() { VendorId = 3, Name = "ASUS TUF RTX 4090 O24G OG OC" },
            new() { VendorId = 3, Name = "ASUS TUF RX 6700 XT O12G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 6800 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 6900 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 6900 XT T16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 6950 XT O16G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 7700 XT GAMING OC" },
            new() { VendorId = 3, Name = "ASUS TUF RX 7800 XT GAMING OC" },
            new() { VendorId = 3, Name = "ASUS TUF RX 7800 XT GAMING WHITE OC" },
            new() { VendorId = 3, Name = "ASUS TUF RX 7900 XT 020G GAMING" },
            new() { VendorId = 3, Name = "ASUS TUF RX 7900 XTX O24G GAMING" },
            new() { VendorId = 3, Name = "Asus ROG Chakram (Wired)" },
            new() { VendorId = 3, Name = "Asus ROG Chakram Core" },
            new() { VendorId = 3, Name = "Asus ROG Chakram X 2.4GHz" },
            new() { VendorId = 3, Name = "Asus ROG Chakram X USB" },
            new() { VendorId = 3, Name = "Asus ROG Spatha X 2.4GHz" },
            new() { VendorId = 3, Name = "Asus ROG Spatha X Dock" },
            new() { VendorId = 3, Name = "Asus ROG Spatha X USB" },

            // -------------------------
            // Corsair
            // -------------------------
            new() { VendorId = 4, Name = "Corsair 1000D Obsidian" },
            new() { VendorId = 4, Name = "Corsair Commander Core" },
            new() { VendorId = 4, Name = "Corsair Commander Pro" },
            new() { VendorId = 4, Name = "Corsair Dark Core RGB Pro SE (Wired)" },
            new() { VendorId = 4, Name = "Corsair Dark Core RGB SE (Wired)" },
            new() { VendorId = 4, Name = "Corsair Dominator Platinum" },
            new() { VendorId = 4, Name = "Corsair Glaive RGB" },
            new() { VendorId = 4, Name = "Corsair Glaive RGB PRO" },
            new() { VendorId = 4, Name = "Corsair H100i v2" },
            new() { VendorId = 4, Name = "Corsair Harpoon RGB" },
            new() { VendorId = 4, Name = "Corsair Harpoon RGB PRO" },
            new() { VendorId = 4, Name = "Corsair Harpoon Wireless (Wired)" },
            new() { VendorId = 4, Name = "Corsair Hydro H100i Platinum" },
            new() { VendorId = 4, Name = "Corsair Hydro H100i Platinum SE" },
            new() { VendorId = 4, Name = "Corsair Hydro H100i Pro XT" },
            new() { VendorId = 4, Name = "Corsair Hydro H100i Pro XT v2" },
            new() { VendorId = 4, Name = "Corsair Hydro H115i Platinum" },
            new() { VendorId = 4, Name = "Corsair Hydro H115i Pro XT" },
            new() { VendorId = 4, Name = "Corsair Hydro H150i Pro XT" },
            new() { VendorId = 4, Name = "Corsair Hydro H60i Pro XT" },
            new() { VendorId = 4, Name = "Corsair Hydro Series" },
            new() { VendorId = 4, Name = "Corsair Ironclaw RGB" },
            new() { VendorId = 4, Name = "Corsair Ironclaw Wireless (Wired)" },
            new() { VendorId = 4, Name = "Corsair K100 MX Red" },
            new() { VendorId = 4, Name = "Corsair K100 RGB Optical" },
            new() { VendorId = 4, Name = "Corsair K55 RGB" },
            new() { VendorId = 4, Name = "Corsair K55 RGB PRO" },
            new() { VendorId = 4, Name = "Corsair K55 RGB PRO XT" },
            new() { VendorId = 4, Name = "Corsair K57 RGB (Wired)" },
            new() { VendorId = 4, Name = "Corsair K60 RGB PRO" },
            new() { VendorId = 4, Name = "Corsair K60 RGB PRO Low Profile" },
            new() { VendorId = 4, Name = "Corsair K60 RGB PRO TKL" },
            new() { VendorId = 4, Name = "Corsair K65 LUX RGB" },
            new() { VendorId = 4, Name = "Corsair K65 Mini" },
            new() { VendorId = 4, Name = "Corsair K65 RGB" },
            new() { VendorId = 4, Name = "Corsair K65 RGB RAPIDFIRE" },
            new() { VendorId = 4, Name = "Corsair K68 RED" },
            new() { VendorId = 4, Name = "Corsair K68 RGB" },
            new() { VendorId = 4, Name = "Corsair K70 LUX" },
            new() { VendorId = 4, Name = "Corsair K70 LUX RGB" },
            new() { VendorId = 4, Name = "Corsair K70 RGB" },
            new() { VendorId = 4, Name = "Corsair K70 RGB MK.2" },
            new() { VendorId = 4, Name = "Corsair K70 RGB MK.2 Low Profile" },
            new() { VendorId = 4, Name = "Corsair K70 RGB MK.2 SE" },
            new() { VendorId = 4, Name = "Corsair K70 RGB PRO" },
            new() { VendorId = 4, Name = "Corsair K70 RGB RAPIDFIRE" },
            new() { VendorId = 4, Name = "Corsair K70 RGB TKL" },
            new() { VendorId = 4, Name = "Corsair K70 RGB TKL Champion Series" },
            new() { VendorId = 4, Name = "Corsair K95 RGB" },
            new() { VendorId = 4, Name = "Corsair K95 RGB PLATINUM" },
            new() { VendorId = 4, Name = "Corsair K95 RGB PLATINUM XT" },
            new() { VendorId = 4, Name = "Corsair Katar Pro" },
            new() { VendorId = 4, Name = "Corsair Katar Pro V2" },
            new() { VendorId = 4, Name = "Corsair Katar Pro XT" },
            new() { VendorId = 4, Name = "Corsair LS100 Lighting Kit" },
            new() { VendorId = 4, Name = "Corsair LT100" },
            new() { VendorId = 4, Name = "Corsair Lighting Node Core" },
            new() { VendorId = 4, Name = "Corsair Lighting Node Pro" },
            new() { VendorId = 4, Name = "Corsair M55 RGB PRO" },
            new() { VendorId = 4, Name = "Corsair M65" },
            new() { VendorId = 4, Name = "Corsair M65 PRO" },
            new() { VendorId = 4, Name = "Corsair M65 RGB Elite" },
            new() { VendorId = 4, Name = "Corsair M65 RGB Ultra Wired" },
            new() { VendorId = 4, Name = "Corsair M65 RGB Ultra Wireless (Wired)" },
            new() { VendorId = 4, Name = "Corsair MM700" },
            new() { VendorId = 4, Name = "Corsair MM800 RGB Polaris" },
            new() { VendorId = 4, Name = "Corsair Nightsword" },
            new() { VendorId = 4, Name = "Corsair SPEC OMEGA RGB" },
            new() { VendorId = 4, Name = "Corsair ST100 RGB" },
            new() { VendorId = 4, Name = "Corsair Sabre RGB" },
            new() { VendorId = 4, Name = "Corsair Scimitar Elite RGB" },
            new() { VendorId = 4, Name = "Corsair Scimitar PRO RGB" },
            new() { VendorId = 4, Name = "Corsair Scimitar RGB" },
            new() { VendorId = 4, Name = "Corsair Slipstream Wireless Receiver HW" },
            new() { VendorId = 4, Name = "Corsair Slipstream Wireless Receiver SW" },
            new() { VendorId = 4, Name = "Corsair Strafe" },
            new() { VendorId = 4, Name = "Corsair Strafe MK.2" },
            new() { VendorId = 4, Name = "Corsair Strafe Red" },
            new() { VendorId = 4, Name = "Corsair Vengeance" },
            new() { VendorId = 4, Name = "Corsair Vengeance Pro" },

            // -------------------------
            // Gigabyte
            // -------------------------
            new() { VendorId = 5, Name = "Gigabyte AORUS ATC800" },
            new() { VendorId = 5, Name = "Gigabyte AORUS C300 GLASS" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2060 SUPER 8G V1" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2070 SUPER 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2070 XTREME 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 SUPER 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 SUPER 8G Rev 1.0" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 SUPER Waterforce WB 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 Ti XTREME 11G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 2080 XTREME 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3060 ELITE 12G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3060 ELITE 12G LHR" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3060 ELITE 12G Rev a1" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3060 Ti ELITE 8G LHR" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3070 Ti MASTER 8G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3080 Ti XTREME WATERFORCE 12G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3080 XTREME WATERFORCE 10G Rev 2.0" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3080 XTREME WATERFORCE WB 10G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3080 XTREME WATERFORCE WB 12G LHR" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3090 XTREME WATERFORCE 24G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 3090 XTREME WATERFORCE WB 24G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 4080 MASTER 16G" },
            new() { VendorId = 5, Name = "Gigabyte AORUS RTX 4090 MASTER 24G" },
            new() { VendorId = 5, Name = "Gigabyte Aorus 15BKF Backlight" },
            new() { VendorId = 5, Name = "Gigabyte Aorus 15BKF Keyboard" },
            new() { VendorId = 5, Name = "Gigabyte Aorus 17X Backlight" },
            new() { VendorId = 5, Name = "Gigabyte Aorus 17X Keyboard" },
            new() { VendorId = 5, Name = "Gigabyte Aorus M2" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1050 Ti G1 Gaming" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1050 Ti G1 Gaming (rev A1)" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1060 G1 Gaming 6G" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1060 G1 Gaming 6G OC" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1060 Xtreme Gaming V1" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1060 Xtreme Gaming v2" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1070 G1 Gaming 8G V1" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1070 Ti 8G Gaming" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1070 Xtreme Gaming" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 G1 Gaming" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 Ti 11G" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 Ti Gaming OC 11G" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 Ti Gaming OC BLACK 11G" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 Ti Xtreme Edition" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1080 Ti Xtreme Waterforce Edition" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1650 Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1660 Gaming OC 6G" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1660 SUPER Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte GTX 1660 Ti Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte RGB" },
            new() { VendorId = 5, Name = "Gigabyte RGB Fusion" },
            new() { VendorId = 5, Name = "Gigabyte RGB Fusion 2 DRAM" },
            new() { VendorId = 5, Name = "Gigabyte RGB Fusion 2 SMBus" },
            new() { VendorId = 5, Name = "Gigabyte RGB Fusion 2 USB" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 Gaming OC PRO" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 Gaming OC PRO V2" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 Gaming OC PRO White" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 SUPER Gaming" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 SUPER Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 SUPER Gaming OC 3X 8G V2" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2060 SUPER Gaming OC 3X White 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 Gaming OC 8GC" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 SUPER Gaming OC" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 SUPER Gaming OC 3X" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 SUPER Gaming OC 3X White" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2070 Windforce 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2080 Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2080 SUPER Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 2080 Ti GAMING OC 11G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3050 Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 EAGLE 12G LHR V2" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 EAGLE OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 EAGLE OC 12G V2" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Gaming OC 12G (rev. 2.0)" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti EAGLE OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti EAGLE OC 8G V2.0 LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti GAMING OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti GAMING OC LHR 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti GAMING OC PRO 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti Gaming OC PRO 8G LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Ti Vision OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Vision OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Vision OC 12G LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3060 Vision OC 12G v3.0" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Eagle OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Eagle OC 8G V2.0 LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Gaming OC 8G v3.0 LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 MASTER 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 MASTER 8G LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Ti EAGLE 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Ti Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Ti Vision OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Vision 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3070 Vision 8G V2.0 LHR" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 EAGLE OC 10G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Gaming OC 10G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Ti EAGLE 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Ti EAGLE OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Ti Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Ti Vision OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Vision OC 10G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3080 Vision OC 10G (REV 2.0)" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3090 Gaming OC 24G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 3090 VISION OC 24G " },
            new() { VendorId = 5, Name = "Gigabyte RTX 4060 Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4060 Ti Gaming OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4060 Ti Gaming OC 8G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 SUPER Aero OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 SUPER Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 Ti Gaming 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 Ti Gaming OC 12G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 Ti SUPER EAGLE OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4070 Ti Super Gaming OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4080 AERO OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4080 Eagle OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4080 Gaming OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4080 SUPER Gaming OC 16G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4090 AERO OC 24G" },
            new() { VendorId = 5, Name = "Gigabyte RTX 4090 GAMING OC 24G" },

            // -------------------------
            // MSI
            // -------------------------
            new() { VendorId = 6, Name = "MSI 3-Zone Laptop" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1070 Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1660 Gaming X 6G" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1660 SUPER Gaming 6G" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1660 SUPER Gaming X 6G" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1660 Ti Gaming 6G" },
            new() { VendorId = 6, Name = "MSI GeForce GTX 1660 Ti Gaming X 6G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2060 Gaming Z 6G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2060 SUPER ARMOR OC" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2060 SUPER Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 ARMOR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 ARMOR OC" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 Gaming" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 Gaming Z" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER ARMOR OC" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER Gaming" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER Gaming Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2070 SUPER Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Duke 8G OC" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Gaming Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 SUPER Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Sea Hawk EK X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Ti 11G Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Ti Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Ti Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 2080 Ti Sea Hawk EK X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3050 Gaming X 8G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 12G Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 12G Gaming X Trio LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 12G Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Gaming X 12G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Gaming X 12G (GA104)" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Gaming X 12G LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Ti 8GB Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Ti 8GB Gaming X LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Ti 8GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Ti 8GB Gaming X Trio LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3060 Ti 8GB SUPER 3X OC" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Gaming Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Suprim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Suprim LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Suprim X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Suprim X GODZILLA LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 8GB Suprim X LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 Ti 8GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3070 Ti Suprim X 8G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 10GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 10GB Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 10GB Gaming Z Trio LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 12GB Gaming Z Trio LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 Suprim X 10G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 Suprim X 10G LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 Suprim X 12G LHR" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 Ti Gaming X Trio 12G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3080 Ti Suprim X 12G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3090 24GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3090 Suprim 24G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3090 Suprim X 24G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3090 Ti Gaming X Trio 24G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 3090 Ti Suprim X 24G" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4060 8GB Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4060 Ti 16GB Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4060 Ti 16GB Gaming X Slim White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4060 Ti 8GB Gaming X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 12GB Gaming X Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 12GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 SUPER 12GB Gaming X Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 SUPER 12GB Gaming X Slim White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti 12GB Gaming X Slim White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti 12GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti 12GB Gaming X Trio White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti 12GB Suprim X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti SUPER 16GB Gaming Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti SUPER 16GB Gaming X Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4070 Ti SUPER 16GB Gaming X Trio White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 16GB Gaming X Slim White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 16GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 16GB Gaming X Trio White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 16GB Suprim X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 SUPER 16GB Gaming X Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 SUPER 16GB Gaming X Slim White" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4080 SUPER 16GB Suprim X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4090 24GB Gaming X Slim" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4090 24GB Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4090 24GB Suprim Liquid X" },
            new() { VendorId = 6, Name = "MSI GeForce RTX 4090 24GB Suprim X" },
            new() { VendorId = 6, Name = "MSI Mystic Light Common" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_1562" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_1563" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_1564" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_1720" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B12" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B16" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B17" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B18" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B50" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B85" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7B93" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C34" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C35" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C36" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C37" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C56" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C59" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C60" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C67" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C71" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C73" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C75" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C76" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C77" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C79" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C80" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C81" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C82" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C83" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C84" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C86" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C87" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C90" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C91" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C92" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C94" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C95" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7C98" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D03" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D04" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D06" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D07" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D08" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D09" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D13" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D15" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D17" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D18" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D19" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D20" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D25" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D27" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D28" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D29" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D30" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D31" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D32" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D36" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D38" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D40" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D41" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D42" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D43" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D46" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D50" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D51" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D52" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D53" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D54" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D59" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D67" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D69" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D70" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D73" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D74" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D75" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D76" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D77" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D78" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D86" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D89" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D90" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D91" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D97" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7D99" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7E01" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7E03" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7E06" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7E07" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_7E10" },
            new() { VendorId = 6, Name = "MSI Mystic Light MS_B926" },
            new() { VendorId = 6, Name = "MSI Optix controller" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6600 XT Gaming X" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6700 XT Gaming X" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6750 XT Gaming X Trio 12G" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6800 Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6800 Gaming Z Trio v1" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6800 XT Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6800 XT Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6900 XT Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6900 XT Gaming Z Trio" },
            new() { VendorId = 6, Name = "MSI Radeon RX 6950 XT Gaming X Trio" },
            new() { VendorId = 6, Name = "MSI Vigor GK30 controller" },
            new() { VendorId = 6, Name = "MSI-RGB" },

            // -------------------------
            // EVGA
            // -------------------------
            new() { VendorId = 7, Name = "EVGA GP102 GPU" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 SUPER FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 SUPER FTW3 Ultra+" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 SUPER XC Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 SUPER XC Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 SUPER XC Ultra+" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 XC Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 XC Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2070 XC OC" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 SUPER FTW3 Hybrid OC" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 SUPER FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 SUPER FTW3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 SUPER XC Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 SUPER XC Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Ti Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Ti FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Ti XC HYBRID GAMING" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Ti XC HYDRO COPPER" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 Ti XC Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 XC Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 XC Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 XC Hybrid Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 2080 XC Ultra Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3060 Ti FTW3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3060 Ti FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3060 Ti FTW3 Ultra LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Black Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 FTW3 Ultra LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Ti FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Ti FTW3 Ultra v2" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Ti XC3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Ti XC3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 Ti XC3 Ultra v2" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 XC3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 XC3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 XC3 Ultra Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3070 XC3 Ultra LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra 12GB" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra Hybrid Gaming LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra Hybrid LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra Hydro Copper 12G" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 FTW3 Ultra v2 LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti FTW3 Ultra Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti FTW3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti XC3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti XC3 Gaming Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti XC3 Gaming Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 Ti XC3 Ultra Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Black LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Gaming LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra 12G" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra Hybrid LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3080 XC3 Ultra LHR" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 FTW3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 FTW3 Ultra Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 FTW3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 FTW3 Ultra v2" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 FTW3 Ultra v3" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 K|NGP|N Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 K|NGP|N Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 Ti FTW3 Black Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 Ti FTW3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 Ti FTW3 Ultra Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 XC3 Black" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 XC3 Gaming" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 XC3 Ultra" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 XC3 Ultra Hybrid" },
            new() { VendorId = 7, Name = "EVGA GeForce RTX 3090 XC3 Ultra Hydro Copper" },
            new() { VendorId = 7, Name = "EVGA Motherboard SMBus Controllers" },
            new() { VendorId = 7, Name = "EVGA Pascal GPU" },
            new() { VendorId = 7, Name = "EVGA X20 Gaming Mouse" },
            new() { VendorId = 7, Name = "EVGA X20 USB Receiver" },
            new() { VendorId = 7, Name = "EVGA Z15 Keyboard" },
            new() { VendorId = 7, Name = "EVGA Z20 Keyboard" },

            // -------------------------
            // Logitech
            // -------------------------
            new() { VendorId = 8, Name = "Logitech G Powerplay Mousepad" },
            new() { VendorId = 8, Name = "Logitech G Pro (HERO) Gaming Mouse" },
            new() { VendorId = 8, Name = "Logitech G Pro Gaming Mouse" },
            new() { VendorId = 8, Name = "Logitech G Pro RGB Mechanical Gaming Keyboard" },
            new() { VendorId = 8, Name = "Logitech G Pro Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G203 Lightsync" },
            new() { VendorId = 8, Name = "Logitech G203 Prodigy" },
            new() { VendorId = 8, Name = "Logitech G213" },
            new() { VendorId = 8, Name = "Logitech G303 Daedalus Apex" },
            new() { VendorId = 8, Name = "Logitech G403 Hero" },
            new() { VendorId = 8, Name = "Logitech G403 Prodigy Gaming Mouse" },
            new() { VendorId = 8, Name = "Logitech G403 Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G502 Hero Gaming Mouse" },
            new() { VendorId = 8, Name = "Logitech G502 Proteus Spectrum Gaming Mouse" },
            new() { VendorId = 8, Name = "Logitech G502 Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G512" },
            new() { VendorId = 8, Name = "Logitech G512 RGB" },
            new() { VendorId = 8, Name = "Logitech G560 Lightsync Speaker" },
            new() { VendorId = 8, Name = "Logitech G610 Orion" },
            new() { VendorId = 8, Name = "Logitech G633 Gaming Headset" },
            new() { VendorId = 8, Name = "Logitech G635 Gaming Headset" },
            new() { VendorId = 8, Name = "Logitech G703 Hero Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G703 Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G733 Gaming Headset" },
            new() { VendorId = 8, Name = "Logitech G810 Orion Spectrum" },
            new() { VendorId = 8, Name = "Logitech G813 RGB Mechanical Gaming Keyboard" },
            new() { VendorId = 8, Name = "Logitech G815 RGB Mechanical Gaming Keyboard" },
            new() { VendorId = 8, Name = "Logitech G900 Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G903 Hero Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G903 Wireless Gaming Mouse (wired)" },
            new() { VendorId = 8, Name = "Logitech G910 Orion Spark" },
            new() { VendorId = 8, Name = "Logitech G910 Orion Spectrum" },
            new() { VendorId = 8, Name = "Logitech G915 Wireless RGB Mechanical Gaming Keyboard" },
            new() { VendorId = 8, Name = "Logitech G915 Wireless RGB Mechanical Gaming Keyboard (Wired)" },
            new() { VendorId = 8, Name = "Logitech G915TKL Wireless RGB Mechanical Gaming Keyboard" },
            new() { VendorId = 8, Name = "Logitech G915TKL Wireless RGB Mechanical Gaming Keyboard (Wired)" },
            new() { VendorId = 8, Name = "Logitech G933 Lightsync Headset" },
            new() { VendorId = 8, Name = "Logitech G935 Gaming Headset" },
            new() { VendorId = 8, Name = "Logitech Lightspeed Receiver" },
            new() { VendorId = 8, Name = "Logitech X56 Rhino Hotas Joystick" },
            new() { VendorId = 8, Name = "Logitech X56 Rhino Hotas Throttle" },

            // -------------------------
            // Lian Li
            // -------------------------
            new() { VendorId = 9, Name = "Lian Li GA II Trinity" },
            new() { VendorId = 9, Name = "Lian Li GA II Trinity Performance" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub - AL" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub - AL V2" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub - SL Infinity" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub - SL V2" },
            new() { VendorId = 9, Name = "Lian Li Uni Hub - SL V2 v0.5" },
            new() { VendorId = 9, Name = "Strimer L Connect" },

            // -------------------------
            // SteelSeries
            // -------------------------
            new() { VendorId = 10, Name = "SteelSeries Aerox 3 Wired" },
            new() { VendorId = 10, Name = "SteelSeries Aerox 5 Wired" },
            new() { VendorId = 10, Name = "SteelSeries Aerox 9 Wired" },
            new() { VendorId = 10, Name = "SteelSeries Apex (OG)/Apex Fnatic" },
            new() { VendorId = 10, Name = "SteelSeries Apex 3" },
            new() { VendorId = 10, Name = "SteelSeries Apex 3 TKL" },
            new() { VendorId = 10, Name = "SteelSeries Apex 350" },
            new() { VendorId = 10, Name = "SteelSeries Apex 5" },
            new() { VendorId = 10, Name = "SteelSeries Apex 7" },
            new() { VendorId = 10, Name = "SteelSeries Apex 7 TKL" },
            new() { VendorId = 10, Name = "SteelSeries Apex M750" },
            new() { VendorId = 10, Name = "SteelSeries Apex Pro" },
            new() { VendorId = 10, Name = "SteelSeries Apex Pro TKL" },
            new() { VendorId = 10, Name = "SteelSeries Arctis 5" },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth 3XL" },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth 4XL" },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth Medium" },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth XL" },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth XL CS:GO Neo Noir Ed." },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth XL CS:GO Neon Rider Ed." },
            new() { VendorId = 10, Name = "SteelSeries QCK Prism Cloth XL Destiny Ed." },
            new() { VendorId = 10, Name = "SteelSeries Rival 100" },
            new() { VendorId = 10, Name = "SteelSeries Rival 100 DotA 2 Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 105" },
            new() { VendorId = 10, Name = "SteelSeries Rival 106" },
            new() { VendorId = 10, Name = "SteelSeries Rival 110" },
            new() { VendorId = 10, Name = "SteelSeries Rival 3" },
            new() { VendorId = 10, Name = "SteelSeries Rival 3 (Old Firmware)" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 Black Ops Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 CS:GO Fade Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 CS:GO Fade Edition (stm32)" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 CS:GO Hyperbeast Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 Dota 2 Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 300 HP Omen Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 310" },
            new() { VendorId = 10, Name = "SteelSeries Rival 310 CS:GO Howl Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 310 PUBG Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 600" },
            new() { VendorId = 10, Name = "SteelSeries Rival 600 Dota 2 Edition" },
            new() { VendorId = 10, Name = "SteelSeries Rival 650" },
            new() { VendorId = 10, Name = "SteelSeries Rival 650 Wireless" },
            new() { VendorId = 10, Name = "SteelSeries Rival 700" },
            new() { VendorId = 10, Name = "SteelSeries Rival 710" },
            new() { VendorId = 10, Name = "SteelSeries Sensei 310" },
            new() { VendorId = 10, Name = "SteelSeries Sensei TEN" },
            new() { VendorId = 10, Name = "SteelSeries Sensei TEN CS:GO Neon Rider Edition" },
            new() { VendorId = 10, Name = "SteelSeries Siberia 350" },

            // -------------------------
            // Thermaltake
            // -------------------------
            new() { VendorId = 11, Name = "Thermaltake Poseidon Z RGB" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FA5)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FA6)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FA7)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FA8)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FA9)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAA)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAB)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAC)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAD)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAE)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FAF)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB0)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB1)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB2)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB3)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB4)" },
            new() { VendorId = 11, Name = "Thermaltake Riing (PID 0x1FB5)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2260)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2261)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2262)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2263)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2264)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2265)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2266)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2267)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2268)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2269)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226A)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226B)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226C)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226D)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226E)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x226F)" },
            new() { VendorId = 11, Name = "Thermaltake Riing Quad (PID 0x2270)" },

            // -------------------------
            // PNY
            // -------------------------
            new() { VendorId = 12, Name = "PNY RTX 2060 XLR8 OC EDITION" },
            new() { VendorId = 12, Name = "PNY RTX 3060 XLR8 Revel EPIC-X" },
            new() { VendorId = 12, Name = "PNY RTX 3070 XLR8 Revel EPIC-X" },
            new() { VendorId = 12, Name = "PNY RTX 3070 XLR8 Revel EPIC-X LHR" },
            new() { VendorId = 12, Name = "PNY RTX 3080 XLR8 Revel EPIC-X" },
            new() { VendorId = 12, Name = "PNY RTX 3090 XLR8 Revel EPIC-X" },
            new() { VendorId = 12, Name = "PNY RTX 4070 Ti Super XLR8 VERTO OC" },
            new() { VendorId = 12, Name = "PNY RTX 4070 Ti XLR8 VERTO Epic-X" },
            new() { VendorId = 12, Name = "PNY RTX 4070 Ti XLR8 VERTO OC" },
            new() { VendorId = 12, Name = "PNY RTX 4070 Ti XLR8 VERTO REV1" },
            new() { VendorId = 12, Name = "PNY RTX 4070 Ti XLR8 VERTO REV2" },
            new() { VendorId = 12, Name = "PNY RTX 4080 SUPER XLR8 VERTO" },
            new() { VendorId = 12, Name = "PNY RTX 4080 XLR8 UPRISING" },
            new() { VendorId = 12, Name = "PNY RTX 4080 XLR8 VERTO" },
            new() { VendorId = 12, Name = "PNY RTX 4080 XLR8 Verto Epic-X" },
            new() { VendorId = 12, Name = "PNY RTX 4090 XLR8 VERTO" },
            new() { VendorId = 12, Name = "PNY RTX 4090 XLR8 Verto Epic-X" },
            new() { VendorId = 12, Name = "PNY RTX 4090 XLR8 Verto Epic-X OC" },

            // -------------------------
            // Cooler Master
            // -------------------------
            new() { VendorId = 13, Name = "Cooler Master ARGB" },
            new() { VendorId = 13, Name = "Cooler Master ARGB Gen 2 A1" },
            new() { VendorId = 13, Name = "Cooler Master ARGB Gen 2 A1 V2" },
            new() { VendorId = 13, Name = "Cooler Master CK530" },
            new() { VendorId = 13, Name = "Cooler Master CK530 V2" },
            new() { VendorId = 13, Name = "Cooler Master CK550 V1 / CK552" },
            new() { VendorId = 13, Name = "Cooler Master CK550 V2" },
            new() { VendorId = 13, Name = "Cooler Master GM27-FQS ARGB Monitor" },
            new() { VendorId = 13, Name = "Cooler Master MK730" },
            new() { VendorId = 13, Name = "Cooler Master MK750" },
            new() { VendorId = 13, Name = "Cooler Master MK850" },
            new() { VendorId = 13, Name = "Cooler Master MM530" },
            new() { VendorId = 13, Name = "Cooler Master MM711" },
            new() { VendorId = 13, Name = "Cooler Master MM720" },
            new() { VendorId = 13, Name = "Cooler Master MM730" },
            new() { VendorId = 13, Name = "Cooler Master MP750 Large" },
            new() { VendorId = 13, Name = "Cooler Master MP750 Medium" },
            new() { VendorId = 13, Name = "Cooler Master MP750 XL" },
            new() { VendorId = 13, Name = "Cooler Master MasterKeys Pro L" },
            new() { VendorId = 13, Name = "Cooler Master MasterKeys Pro L White" },
            new() { VendorId = 13, Name = "Cooler Master MasterKeys Pro S" },
            new() { VendorId = 13, Name = "Cooler Master RGB" },
            new() { VendorId = 13, Name = "Cooler Master Radeon 6000 GPU" },
            new() { VendorId = 13, Name = "Cooler Master Radeon 6900 GPU" },
            new() { VendorId = 13, Name = "Cooler Master SK622 Black" },
            new() { VendorId = 13, Name = "Cooler Master SK622 White" },
            new() { VendorId = 13, Name = "Cooler Master SK630" },
            new() { VendorId = 13, Name = "Cooler Master SK650" },
            new() { VendorId = 13, Name = "Cooler Master SK652" },
            new() { VendorId = 13, Name = "Cooler Master SK653" },
            new() { VendorId = 13, Name = "Cooler Master Small ARGB" },

            // -------------------------
            // NZXT
            // -------------------------
            new() { VendorId = 14, Name = "NZXT Hue 2" },
            new() { VendorId = 14, Name = "NZXT Hue 2 Ambient" },
            new() { VendorId = 14, Name = "NZXT Hue 2 Motherboard" },
            new() { VendorId = 14, Name = "NZXT Hue+" },
            new() { VendorId = 14, Name = "NZXT Kraken M2" },
            new() { VendorId = 14, Name = "NZXT Kraken X2" },
            new() { VendorId = 14, Name = "NZXT Kraken X3 Series" },
            new() { VendorId = 14, Name = "NZXT Kraken X3 Series RGB" },
            new() { VendorId = 14, Name = "NZXT Lift" },
            new() { VendorId = 14, Name = "NZXT RGB & Fan Controller" },
            new() { VendorId = 14, Name = "NZXT RGB Controller" },
            new() { VendorId = 14, Name = "NZXT Smart Device V1" },
            new() { VendorId = 14, Name = "NZXT Smart Device V2" },

            // -------------------------
            // Lenovo
            // -------------------------
            new() { VendorId = 15, Name = "Lenovo" },
            new() { VendorId = 15, Name = "Lenovo 5 2020" },
            new() { VendorId = 15, Name = "Lenovo 5 2021" },
            new() { VendorId = 15, Name = "Lenovo 5 2021 Ideapad" },
            new() { VendorId = 15, Name = "Lenovo 5 2022" },
            new() { VendorId = 15, Name = "Lenovo 5 2022 Ideapad" },
            new() { VendorId = 15, Name = "Lenovo 5 2023" },
            new() { VendorId = 15, Name = "Lenovo 5 2023 Ideapad" },
            new() { VendorId = 15, Name = "Lenovo Ideapad 3-15ach6" },
            new() { VendorId = 15, Name = "Lenovo Legion 7 Gen 5" },
            new() { VendorId = 15, Name = "Lenovo Legion 7 Gen 6" },
            new() { VendorId = 15, Name = "Lenovo Legion 7 Gen 7" },
            new() { VendorId = 15, Name = "Lenovo Legion 7 Gen 8" },
            new() { VendorId = 15, Name = "Lenovo Legion 7S Gen 5" },
            new() { VendorId = 15, Name = "Lenovo Legion 7S Gen 6" },
            new() { VendorId = 15, Name = "Lenovo Legion M300" },
            new() { VendorId = 15, Name = "Lenovo Legion Y740" },

            // -------------------------
            // Sapphire
            // -------------------------
            new() { VendorId = 16, Name = "Sapphire RX 470/480 Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 5500 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 570/580/590 Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 5700 (XT) Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 5700 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 580 Nitro+ (2048SP)" },
            new() { VendorId = 16, Name = "Sapphire RX 6600 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6650 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6700 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6750 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6800 Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6800 XT Nitro+ SE" },
            new() { VendorId = 16, Name = "Sapphire RX 6800 XT/6900 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 6900 XT Nitro+ SE" },
            new() { VendorId = 16, Name = "Sapphire RX 6900 XT Toxic" },
            new() { VendorId = 16, Name = "Sapphire RX 6900 XT Toxic Limited Edition" },
            new() { VendorId = 16, Name = "Sapphire RX 6950 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 7800 XT Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 7900 GRE Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX 7900 XTX Nitro+" },
            new() { VendorId = 16, Name = "Sapphire RX Vega 56/64 Nitro+" },

            // -------------------------
            // ZOTAC
            // -------------------------
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 2070 SUPER Twin Fan" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 2080 SUPER Twin Fan" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3070 AMP Holo LHR" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3070 Ti" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3070 Ti Trinity OC/AMP Holo" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3080 AMP Holo LHR" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3080 Ti AMP Holo" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3080 Trinity LHR" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3080 Trinity OC" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3080 Trinity OC LHR 12GB" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3090 AMP Extreme Holo" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 3090 Trinity" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 4070 Ti Trinity OC" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 4080 16GB AMP Extreme AIRO" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 4090 AMP Extreme AIRO" },
            new() { VendorId = 17, Name = "ZOTAC GAMING GeForce RTX 4090 Trinity OC" },

            // -------------------------
            // Palit
            // -------------------------
            new() { VendorId = 18, Name = "Palit GTX 1060" },
            new() { VendorId = 18, Name = "Palit GTX 1070" },
            new() { VendorId = 18, Name = "Palit GTX 1070 Ti" },
            new() { VendorId = 18, Name = "Palit GTX 1080" },
            new() { VendorId = 18, Name = "Palit GTX 1080 Ti" },
            new() { VendorId = 18, Name = "Palit GeForce RTX 3060 Ti Dual" },
            new() { VendorId = 18, Name = "Palit RTX 3060" },
            new() { VendorId = 18, Name = "Palit RTX 3060 LHR" },
            new() { VendorId = 18, Name = "Palit RTX 3060 Ti" },
            new() { VendorId = 18, Name = "Palit RTX 3060 Ti LHR" },
            new() { VendorId = 18, Name = "Palit RTX 3070" },
            new() { VendorId = 18, Name = "Palit RTX 3070 LHR" },
            new() { VendorId = 18, Name = "Palit RTX 3070 Ti" },
            new() { VendorId = 18, Name = "Palit RTX 3070 Ti GamingPro" },
            new() { VendorId = 18, Name = "Palit RTX 3080" },
            new() { VendorId = 18, Name = "Palit RTX 3080 Gamerock" },
            new() { VendorId = 18, Name = "Palit RTX 3080 Gamerock LHR" },
            new() { VendorId = 18, Name = "Palit RTX 3080 GamingPro 12G" },
            new() { VendorId = 18, Name = "Palit RTX 3080 LHR" },
            new() { VendorId = 18, Name = "Palit RTX 3080 Ti" },
            new() { VendorId = 18, Name = "Palit RTX 3080 Ti Gamerock" },
            new() { VendorId = 18, Name = "Palit RTX 3090" },
            new() { VendorId = 18, Name = "Palit RTX 3090 Gamerock" },
            new() { VendorId = 18, Name = "Palit RTX 4070 Ti Gamerock" },
            new() { VendorId = 18, Name = "Palit RTX 4080 GamingPro" },
            new() { VendorId = 18, Name = "Palit RTX 4090 Gamerock" },

            // -------------------------
            // Roccat
            // -------------------------
            new() { VendorId = 19, Name = "Roccat Burst Core" },
            new() { VendorId = 19, Name = "Roccat Burst Pro" },
            new() { VendorId = 19, Name = "Roccat Burst Pro Air" },
            new() { VendorId = 19, Name = "Roccat Elo 7.1" },
            new() { VendorId = 19, Name = "Roccat Horde Aimo" },
            new() { VendorId = 19, Name = "Roccat Kone Aimo" },
            new() { VendorId = 19, Name = "Roccat Kone Aimo 16K" },
            new() { VendorId = 19, Name = "Roccat Kone Pro" },
            new() { VendorId = 19, Name = "Roccat Kone Pro Air" },
            new() { VendorId = 19, Name = "Roccat Kone Pro Air (Wired)" },
            new() { VendorId = 19, Name = "Roccat Kone XP" },
            new() { VendorId = 19, Name = "Roccat Kova" },
            new() { VendorId = 19, Name = "Roccat Magma" },
            new() { VendorId = 19, Name = "Roccat Magma Mini" },
            new() { VendorId = 19, Name = "Roccat Pyro" },
            new() { VendorId = 19, Name = "Roccat Sense Aimo Mid" },
            new() { VendorId = 19, Name = "Roccat Sense Aimo XXL" },
            new() { VendorId = 19, Name = "Roccat Vulcan 100 Aimo" },
            new() { VendorId = 19, Name = "Roccat Vulcan 120-Series Aimo" },
            new() { VendorId = 19, Name = "Roccat Vulcan Pro" },
            new() { VendorId = 19, Name = "Roccat Vulcan TKL" },

            // -------------------------
            // Wooting
            // -------------------------
            new() { VendorId = 20, Name = "Wooting One (Classic)" },
            new() { VendorId = 20, Name = "Wooting One (Legacy)" },
            new() { VendorId = 20, Name = "Wooting One (None)" },
            new() { VendorId = 20, Name = "Wooting One (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two (Legacy)" },
            new() { VendorId = 20, Name = "Wooting Two (None)" },
            new() { VendorId = 20, Name = "Wooting Two (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (ARM) (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (ARM) (None)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (ARM) (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (None)" },
            new() { VendorId = 20, Name = "Wooting Two 60HE (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two HE (ARM) (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two HE (ARM) (None)" },
            new() { VendorId = 20, Name = "Wooting Two HE (ARM) (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two HE (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two HE (None)" },
            new() { VendorId = 20, Name = "Wooting Two HE (Xbox)" },
            new() { VendorId = 20, Name = "Wooting Two LE (Classic)" },
            new() { VendorId = 20, Name = "Wooting Two LE (None)" },
            new() { VendorId = 20, Name = "Wooting Two LE (Xbox)" },

            // -------------------------
            // Cherry
            // -------------------------
            new() { VendorId = 21, Name = "Cherry Keyboard CCF MX 1.0 TKL BL" },
            new() { VendorId = 21, Name = "Cherry Keyboard CCF MX 1.0 TKL NBL" },
            new() { VendorId = 21, Name = "Cherry Keyboard CCF MX 8.0 TKL BL" },
            new() { VendorId = 21, Name = "Cherry Keyboard G80-3000 TKL NBL" },
            new() { VendorId = 21, Name = "Cherry Keyboard G80-3000 TKL NBL KOREAN" },
            new() { VendorId = 21, Name = "Cherry Keyboard G80-3000 TKL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard G80-3000N FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard G80-3000N TKL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MV BOARD 3.0 FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX 1.0 FL BL" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX 1.0 FL NBL" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX 1.0 FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 1.0 TKL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 10.0 FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 10.0N FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 2.0S FL NBL" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 2.0S FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 2.0S FL RGB DE" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 3.0S FL NBL" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 3.0S FL RGB" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 3.0S FL RGB KOREAN" },
            new() { VendorId = 21, Name = "Cherry Keyboard MX BOARD 8.0 TKL RGB" },

            // -------------------------
            // Glorious
            // -------------------------
            new() { VendorId = 22, Name = "Glorious Model D / D-" },
            new() { VendorId = 22, Name = "Glorious Model D / D- Wireless" },
            new() { VendorId = 22, Name = "Glorious Model O / O-" },
            new() { VendorId = 22, Name = "Glorious Model O / O- Wireless" },

            // -------------------------
            // HyperX
            // -------------------------
            new() { VendorId = 23, Name = "HyperX Alloy Elite 2" },
            new() { VendorId = 23, Name = "HyperX Alloy Elite 2 (HP)" },
            new() { VendorId = 23, Name = "HyperX Alloy Elite RGB" },
            new() { VendorId = 23, Name = "HyperX Alloy FPS RGB" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins (HP)" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins 60" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins 60 (HP)" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins 65 (HP)" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins Core" },
            new() { VendorId = 23, Name = "HyperX Alloy Origins Core (HP)" },
            new() { VendorId = 23, Name = "HyperX DRAM" },
            new() { VendorId = 23, Name = "HyperX DuoCast" },
            new() { VendorId = 23, Name = "HyperX Fury Ultra" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Core" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Core (HP)" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Dart (Wired)" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Dart (Wireless)" },
            new() { VendorId = 23, Name = "HyperX Pulsefire FPS Pro" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Haste" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Mat" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Mat RGB Mouse Pad XL" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Raid" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Surge" },
            new() { VendorId = 23, Name = "HyperX Pulsefire Surge (HP)" },
            new() { VendorId = 23, Name = "HyperX Quadcast S" },

            // -------------------------
            // Colorful
            // -------------------------
            new() { VendorId = 24, Name = "iGame GeForce RTX 2070 SUPER Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3060 Advanced OC 12G L-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3060 Ti Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3060 Ti Ultra W OC LHR-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3060 Ultra W OC 12G L-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3070 Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3070 Ti Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3070 Ti Ultra W OC LHR" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3070 Ultra W OC LHR" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3070 Ultra W OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3080 Advanced OC 10G-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3080 Ti Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 3080 Ultra W OC 10G LHR-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 4070 Ti Advanced OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 4070 Vulcan OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 4080 Ultra W OC-V" },
            new() { VendorId = 24, Name = "iGame GeForce RTX 4090 Advanced OC-V" },

            // -------------------------
            // Redragon
            // -------------------------
            new() { VendorId = 25, Name = "Redragon M602 Griffin" },
            new() { VendorId = 25, Name = "Redragon M711 Cobra" },
            new() { VendorId = 25, Name = "Redragon M715 Dagger" },
            new() { VendorId = 25, Name = "Redragon M716 Inquisitor" },
            new() { VendorId = 25, Name = "Redragon M801 Sniper" },
            new() { VendorId = 25, Name = "Redragon M808 Storm" },
            new() { VendorId = 25, Name = "Redragon M908 Impact" },

            // -------------------------
            // Gainward
            // -------------------------
            new() { VendorId = 26, Name = "Gainward GTX 1080 Phoenix" },
            new() { VendorId = 26, Name = "Gainward GTX 1080 Ti Phoenix" },
            new() { VendorId = 26, Name = "Gainward GTX 1660 Super Ghost" },
            new() { VendorId = 26, Name = "Gainward RTX 2070 Super Phantom" },
            new() { VendorId = 26, Name = "Gainward RTX 2080 Phoenix GS" },
            new() { VendorId = 26, Name = "Gainward RTX 3070 Phantom" },
            new() { VendorId = 26, Name = "Gainward RTX 3070 Phoenix" },
            new() { VendorId = 26, Name = "Gainward RTX 3070 Ti Phoenix" },
            new() { VendorId = 26, Name = "Gainward RTX 3080 Phoenix" },
            new() { VendorId = 26, Name = "Gainward RTX 3080 Ti Phoenix" },
            new() { VendorId = 26, Name = "Gainward RTX 3090 Phoenix" },

            // -------------------------
            // Alienware
            // -------------------------
            new() { VendorId = 27, Name = "Alienware AW410K" },
            new() { VendorId = 27, Name = "Alienware AW510K" },

            // -------------------------
            // ASRock
            // -------------------------
            new() { VendorId = 28, Name = "ASRock Deskmini Addressable LED Strip" },
            new() { VendorId = 28, Name = "ASRock Motherboard SMBus Controllers" },
            new() { VendorId = 28, Name = "ASRock Polychrome USB" },

            // -------------------------
            // AOC
            // -------------------------
            new() { VendorId = 29, Name = "AOC AGON AMM700" },
            new() { VendorId = 29, Name = "AOC GK500" },
            new() { VendorId = 29, Name = "AOC GM500" },

            // -------------------------
            // Arctic
            // -------------------------
            new() { VendorId = 30, Name = "Arctic RGB controller" },

            // -------------------------
            // HYTE
            // -------------------------
            new() { VendorId = 31, Name = "HYTE Mousemat" },

            // -------------------------
            // Zalman
            // -------------------------
            new() { VendorId = 32, Name = "Zalman Z Sync" },

            // -------------------------
            // Cougar
            // -------------------------
            new() { VendorId = 33, Name = "Cougar 700K EVO Gaming Keyboard" },
            new() { VendorId = 33, Name = "Cougar Revenger ST" },

            // -------------------------
            // Creative
            // -------------------------
            new() { VendorId = 34, Name = "Creative SoundBlasterX G6" },

            // -------------------------
            // Crucial
            // -------------------------
            new() { VendorId = 35, Name = "Crucial" },

            // -------------------------
            // Elgato
            // -------------------------
            new() { VendorId = 36, Name = "Elgato Light Strip" },
            new() { VendorId = 36, Name = "ElgatoKeyLight" },

            // -------------------------
            // EVision
            // -------------------------
            new() { VendorId = 37, Name = "CSB/ICL01 Keyboard" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:5004" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:5104" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:5204" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:652F" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:7698" },
            new() { VendorId = 37, Name = "EVision Keyboard 0C45:8520" },
            new() { VendorId = 37, Name = "EVision Keyboard 320F:5000" },
            new() { VendorId = 37, Name = "EVision Keyboard 320F:502A" },
            new() { VendorId = 37, Name = "EVision Keyboard 320F:5064" },
            new() { VendorId = 37, Name = "EVision Keyboard 320F:5078" },

            // -------------------------
            // EKWB
            // -------------------------
            new() { VendorId = 38, Name = "EK Loop Connect" },

            // -------------------------
            // GaiZhongGai
            // -------------------------
            new() { VendorId = 39, Name = "GaiZhongGai 17 PRO" },
            new() { VendorId = 39, Name = "GaiZhongGai 17+4+Touch PRO" },
            new() { VendorId = 39, Name = "GaiZhongGai 20 PRO" },
            new() { VendorId = 39, Name = "GaiZhongGai 42 PRO" },
            new() { VendorId = 39, Name = "GaiZhongGai 68+4 PRO" },
            new() { VendorId = 39, Name = "GaiZhongGai Dial" },
            new() { VendorId = 39, Name = "GaiZhongGai LightBoard" },
            new() { VendorId = 39, Name = "GaiZhongGai RGB HUB Blue" },
            new() { VendorId = 39, Name = "GaiZhongGai RGB HUB Green" },

            // -------------------------
            // DRGB
            // -------------------------
            new() { VendorId = 40, Name = "DRGB CORE V3" },
            new() { VendorId = 40, Name = "DRGB CORE V4F" },
            new() { VendorId = 40, Name = "DRGB LED" },
            new() { VendorId = 40, Name = "DRGB LED Controller" },
            new() { VendorId = 40, Name = "DRGB LED V4" },
            new() { VendorId = 40, Name = "DRGB SIG AB" },
            new() { VendorId = 40, Name = "DRGB SIG CD" },
            new() { VendorId = 40, Name = "DRGB SIG V4F" },
            new() { VendorId = 40, Name = "DRGB Strimer Controller" },
            new() { VendorId = 40, Name = "DRGB ULTRA" },
            new() { VendorId = 40, Name = "DRGB ULTRA V4F" },
            new() { VendorId = 40, Name = "DRGB Ultra V3" },

            // -------------------------
            // Red Square
            // -------------------------
            new() { VendorId = 41, Name = "Red Square Keyrox TKL" },
            new() { VendorId = 41, Name = "Red Square Keyrox TKL Classic" },
            new() { VendorId = 41, Name = "Red Square Keyrox TKL V2" },

            // -------------------------
            // ZET
            // -------------------------
            new() { VendorId = 42, Name = "ZET Blade Optical" },
            new() { VendorId = 42, Name = "ZET Fury Pro" },
            new() { VendorId = 42, Name = "ZET GAMING Edge Air Elit" },
            new() { VendorId = 42, Name = "ZET GAMING Edge Air Elit (Wireless)" },
            new() { VendorId = 42, Name = "ZET GAMING Edge Air Pro" },
            new() { VendorId = 42, Name = "ZET GAMING Edge Air Pro (Wireless)" },

            // -------------------------
            // Nollie
            // -------------------------
            new() { VendorId = 43, Name = "Nollie 16CH" },
            new() { VendorId = 43, Name = "Nollie 1CH" },
            new() { VendorId = 43, Name = "Nollie 28 12" },
            new() { VendorId = 43, Name = "Nollie 28 L1" },
            new() { VendorId = 43, Name = "Nollie 28 L2" },
            new() { VendorId = 43, Name = "Nollie 32CH" },
            new() { VendorId = 43, Name = "Nollie 8CH" },

            // -------------------------
            // Ducky
            // -------------------------
            new() { VendorId = 44, Name = "Ducky One 2 RGB TKL" },
            new() { VendorId = 44, Name = "Ducky Shine 7/Ducky One 2 RGB" },

            // -------------------------
            // Das Keyboard
            // -------------------------
            new() { VendorId = 45, Name = "Das Keyboard Q4 RGB" },
            new() { VendorId = 45, Name = "Das Keyboard Q5 RGB" },
            new() { VendorId = 45, Name = "Das Keyboard Q5S RGB" },

            // -------------------------
            // Epomaker
            // -------------------------
            new() { VendorId = 46, Name = "Epomaker TH80 Pro (USB Cable)" },
            new() { VendorId = 46, Name = "Epomaker TH80 Pro (USB Dongle)" },

            // -------------------------
            // GALAX
            // -------------------------
            new() { VendorId = 47, Name = "GALAX RTX 2070 SUPER EX Gamer Black" },

            // -------------------------
            // ViewSonic
            // -------------------------
            new() { VendorId = 48, Name = "ViewSonic Monitor XG270QG" },

            // -------------------------
            // Philips
            // -------------------------
            new() { VendorId = 49, Name = "Philips Hue" },
            new() { VendorId = 49, Name = "Philips Wiz" },

            // -------------------------
            // Seagate
            // -------------------------
            new() { VendorId = 50, Name = "Seagate Firecuda HDD" },

            // -------------------------
            // NVIDIA
            // -------------------------
            new() { VendorId = 51, Name = "NVidia NvAPI Illumination" },
            new() { VendorId = 51, Name = "NVIDIA RTX 2060 SUPER" },
            new() { VendorId = 51, Name = "NVIDIA RTX 2080 SUPER" },

            // -------------------------
            // Sony
            // -------------------------
            new() { VendorId = 52, Name = "Sony DualSense" },
            new() { VendorId = 52, Name = "Sony DualShock 4" },

            // -------------------------
            // Patriot
            // -------------------------
            new() { VendorId = 53, Name = "Patriot Viper" },
            new() { VendorId = 53, Name = "Patriot Viper Steel" },

            // -------------------------
            // SRGBMods
            // -------------------------
            new() { VendorId = 54, Name = "SRGBMods LED Controller v1" },
            new() { VendorId = 54, Name = "SRGBmods Pico LED Controller" },

            // -------------------------
            // Valkyrie
            // -------------------------
            new() { VendorId = 55, Name = "Valkyrie VK99" },
            new() { VendorId = 55, Name = "Valkyrie VK99 Pro" },

            // -------------------------
            // Trust
            // -------------------------
            new() { VendorId = 56, Name = "Trust GXT 114" },
            new() { VendorId = 56, Name = "Trust GXT 180" },

            // -------------------------
            // LIFX
            // -------------------------
            new() { VendorId = 57, Name = "LIFX" },

            // -------------------------
            // LG
            // -------------------------
            new() { VendorId = 58, Name = "LG 27GN950-B Monitor" },

            // -------------------------
            // Kingston
            // -------------------------
            new() { VendorId = 59, Name = "Kingston Fury DDR4/5 DRAM" },

            // -------------------------
            // Intel
            // -------------------------
            new() { VendorId = 60, Name = "Intel Arc A770 Limited Edition" },

            // -------------------------
            // KFA2
            // -------------------------
            new() { VendorId = 61, Name = "KFA2 RTX 2070 EX" },
            new() { VendorId = 61, Name = "KFA2 RTX 2080 EX OC" },
            new() { VendorId = 61, Name = "KFA2 RTX 2080 SUPER EX OC" },
            new() { VendorId = 61, Name = "KFA2 RTX 2080 Ti EX OC" },

            // -------------------------
            // PC Specialist
            // -------------------------
            new() { VendorId = 62, Name = "Ionico Keyboard" },
            new() { VendorId = 62, Name = "Ionico Light Bar" },

            // -------------------------
            // Holtek
            // -------------------------
            new() { VendorId = 63, Name = "Holtek Mousemat" },
            new() { VendorId = 63, Name = "Holtek USB Gaming Mouse" },

            // -------------------------
            // Genesis
            // -------------------------
            new() { VendorId = 64, Name = "Genesis Thor 300" },
            new() { VendorId = 64, Name = "Genesis Xenon 200" },

            // -------------------------
            // XPG
            // -------------------------
            new() { VendorId = 65, Name = "XPG Spectrix S40G" },

            // -------------------------
            // TP-Link
            // -------------------------
            new() { VendorId = 66, Name = "KasaSmart" },

            // -------------------------
            // Lego
            // -------------------------
            new() { VendorId = 67, Name = "Lego Dimensions Toypad Base" },

            // -------------------------
            // A4Tech
            // -------------------------
            new() { VendorId = 68, Name = "A4Tech Bloody B820R" },
            new() { VendorId = 68, Name = "Bloody MP 50RS" },
            new() { VendorId = 68, Name = "Bloody W60 Pro" },
            new() { VendorId = 68, Name = "Bloody W90 Max" },

            // -------------------------
            // AMD
            // -------------------------
            new() { VendorId = 69, Name = "AMD Wraith Prism" },

            // -------------------------
            // Acer
            // -------------------------
            new() { VendorId = 70, Name = "Acer Predator Gaming Mouse (Rival 300)" },

            // -------------------------
            // Anko
            // -------------------------
            new() { VendorId = 71, Name = "Anko KM43243952 USB Gaming Mouse" },
            new() { VendorId = 71, Name = "Anko KM43277483 USB Gaming Mouse" },

            // -------------------------
            // Advance
            // -------------------------
            new() { VendorId = 72, Name = "Advanced GTA 250 USB Gaming Mouse" },

            // -------------------------
            // HP
            // -------------------------
            new() { VendorId = 73, Name = "HP Omen 30L" },

            // -------------------------
            // Dell
            // -------------------------
            new() { VendorId = 74, Name = "Dell G Series LED Controller" },
            new() { VendorId = 74, Name = "Nvidia ESA - Dell XPS 730x" },

            // -------------------------
            // Endorfy
            // -------------------------
            new() { VendorId = 75, Name = "Endorfy Omnis" },

            // -------------------------
            // Hexcore
            // -------------------------
            new() { VendorId = 76, Name = "Anne Pro 2" },

            // -------------------------
            // LightSalt
            // -------------------------
            new() { VendorId = 77, Name = "LightSalt Peripherals" },

            // -------------------------
            // Dygma
            // -------------------------
            new() { VendorId = 78, Name = "Dygma Raise" },

            // -------------------------
            // Yeelight
            // -------------------------
            new() { VendorId = 79, Name = "Yeelight" },

            // -------------------------
            // Winbond
            // -------------------------
            new() { VendorId = 80, Name = "Winbond Gaming Keyboard" },

            // -------------------------
            // ThingM
            // -------------------------
            new() { VendorId = 81, Name = "ThingM blink(1) mk2" },

            // -------------------------
            // Tecknet
            // -------------------------
            new() { VendorId = 82, Name = "Tecknet M008" },

            // -------------------------
            // Skyloong
            // -------------------------
            new() { VendorId = 83, Name = "Skyloong GK104 Pro" },

            // -------------------------
            // OKS
            // -------------------------
            new() { VendorId = 84, Name = "OKS Optical Axis RGB" },

            // -------------------------
            // Lexip
            // -------------------------
            new() { VendorId = 85, Name = "Np93 ALPHA - Gaming Mouse" },

            // -------------------------
            // Nanoleaf
            // -------------------------
            new() { VendorId = 86, Name = "Nanoleaf" },

            // -------------------------
            // Mountain
            // -------------------------
            new() { VendorId = 87, Name = "Mountain Everest" },

            // -------------------------
            // Keychron
            // -------------------------
            new() { VendorId = 88, Name = "Keychron Gaming Keyboard 1" },

            // -------------------------
            // JSAUX
            // -------------------------
            new() { VendorId = 89, Name = "JSAUX RGB Docking Station" },

            // -------------------------
            // JGINYUE
            // -------------------------
            new() { VendorId = 90, Name = "JGINYUE Internal USB Controller" },

            // -------------------------
            // PNC Partner
            // -------------------------
            new() { VendorId = 91, Name = "Everest GT-100 RGB" },

            // -------------------------
            // Espurna
            // -------------------------
            new() { VendorId = 92, Name = "Espurna" },

            // -------------------------
            // Dark Project
            // -------------------------
            new() { VendorId = 93, Name = "Dark Project KD3B V2" },

            // -------------------------
            // DMX
            // -------------------------
            new() { VendorId = 94, Name = "DMX" },

            // -------------------------
            // CRYORIG
            // -------------------------
            new() { VendorId = 95, Name = "CRYORIG H7 Quad Lumi" },

            // -------------------------
            // Blinkinlabs
            // -------------------------
            new() { VendorId = 96, Name = "BlinkyTape" },

            // -------------------------
            // Attack Shark
            // -------------------------
            new() { VendorId = 97, Name = "Attack Shark K86 (USB Cable)" },

            // -------------------------
            // FanBus
            // -------------------------
            new() { VendorId = 98, Name = "FanBus" },

            // -------------------------
            // E1.31
            // -------------------------
            new() { VendorId = 99, Name = "E1.31" },

            // -------------------------
            // N5312A
            // -------------------------
            new() { VendorId = 100, Name = "N5312A USB Optical Mouse" },

            // -------------------------
            // LED Strip
            // -------------------------
            new() { VendorId = 101, Name = "LED Strip" },

            // -------------------------
            // ENE DRAM
            // -------------------------
            new() { VendorId = 102, Name = "ENE SMBus DRAM" }
        ]);
    }
}
