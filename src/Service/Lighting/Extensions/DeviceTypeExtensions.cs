// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.SDK.OpenRGB.Enums;

namespace ChromaControl.Service.Lighting.Extensions;

/// <summary>
/// Extension methods for <see cref="OpenRGBDeviceType"/>.
/// </summary>
public static class DeviceTypeExtensions
{
    /// <summary>
    /// Returns a friendly string version of a <see cref="OpenRGBDeviceType"/>.
    /// </summary>
    /// <param name="deviceType">The <see cref="OpenRGBDeviceType"/>.</param>
    /// <returns>A friendly version of the <see cref="OpenRGBDeviceType"/>.</returns>
    public static string ToFriendlyName(this OpenRGBDeviceType deviceType)
    {
        return deviceType switch
        {
            OpenRGBDeviceType.Motherboard => deviceType.ToString(),
            OpenRGBDeviceType.Memory => deviceType.ToString(),
            OpenRGBDeviceType.GraphicsCard => "Graphics Card",
            OpenRGBDeviceType.Cooler => deviceType.ToString(),
            OpenRGBDeviceType.LedStrip => "Led Strip",
            OpenRGBDeviceType.Keyboard => deviceType.ToString(),
            OpenRGBDeviceType.Mouse => deviceType.ToString(),
            OpenRGBDeviceType.MouseMat => "Mouse Mat",
            OpenRGBDeviceType.Headset => deviceType.ToString(),
            OpenRGBDeviceType.HeadsetStand => "Headset Stand",
            OpenRGBDeviceType.Gamepad => deviceType.ToString(),
            OpenRGBDeviceType.Light => deviceType.ToString(),
            OpenRGBDeviceType.Speaker => deviceType.ToString(),
            OpenRGBDeviceType.Virtual => deviceType.ToString(),
            OpenRGBDeviceType.Storage => deviceType.ToString(),
            OpenRGBDeviceType.Case => deviceType.ToString(),
            OpenRGBDeviceType.Microphone => deviceType.ToString(),
            OpenRGBDeviceType.Accessory => deviceType.ToString(),
            OpenRGBDeviceType.Keypad => deviceType.ToString(),
            OpenRGBDeviceType.Unknown => deviceType.ToString(),
            _ => deviceType.ToString(),
        };
    }
}
