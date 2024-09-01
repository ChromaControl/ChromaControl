// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Lighting;
using ChromaControl.SDK.OpenRGB;
using ChromaControl.Service.Data;
using ChromaControl.Service.Data.Extensions;
using ChromaControl.Service.Lighting.Extensions;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Lighting.Services;

/// <summary>
/// The lighting service
/// </summary>
public class LightingService : LightingGrpc.LightingGrpcBase
{
    private readonly AppDbContext _context;
    private readonly IOpenRGBService _openRGBService;
    private readonly EventDispatcher _eventDispatcher;

    /// <summary>
    /// Creates a <see cref="LightingService"/> instance.
    /// </summary>
    /// <param name="context">The <see cref="AppDbContext"/>.</param>
    /// <param name="openRGBService">The <see cref="IOpenRGBService"/>.</param>
    /// <param name="eventDispatcher">The <see cref="EventDispatcher"/>.</param>
    public LightingService(AppDbContext context, IOpenRGBService openRGBService, EventDispatcher eventDispatcher)
    {
        _context = context;
        _openRGBService = openRGBService;
        _eventDispatcher = eventDispatcher;
    }

    /// <summary>
    /// Restarts the service.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> RestartService(EmptyMessage request, ServerCallContext context)
    {
        var config = await _context.GenerateConfig();

        _openRGBService.UpdateConfiguration(config);
        await _openRGBService.Restart();

        return request;
    }

    /// <summary>
    /// Streams events.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="responseStream">The <see cref="IServerStreamWriter{T}"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task StreamEvents(EmptyMessage request, IServerStreamWriter<Event> responseStream, ServerCallContext context)
    {
        async void EventAction(EventType type)
        {
            await responseStream.WriteAsync(new() { Type = type });
        }

        _eventDispatcher.EventTriggered += EventAction;

        while (!context.CancellationToken.IsCancellationRequested)
        {
            await Task.Delay(5000, context.CancellationToken);
        }

        _eventDispatcher.EventTriggered -= EventAction;
    }

    /// <summary>
    /// Gets the device vendors.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="VendorListResponse"/>.</returns>
    public override async Task<VendorListResponse> GetVendors(EmptyMessage request, ServerCallContext context)
    {
        var vendors = await _context.DeviceVendors
            .Where(v => !v.Hidden)
            .OrderBy(v => v.Name)
            .Select(v => new Vendor()
            {
                Name = v.Name,
                Enabled = v.Enabled,
                Dangerous = v.Dangerous
            })
            .ToListAsync();

        var response = new VendorListResponse();

        response.Vendors.AddRange(vendors);

        return response;
    }

    /// <summary>
    /// Toggles a device vendor.
    /// </summary>
    /// <param name="request">The <see cref="ToggleVendorRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> ToggleVendor(ToggleVendorRequest request, ServerCallContext context)
    {
        await _context.DeviceVendors
            .Where(v => v.Name == request.Name)
            .ExecuteUpdateAsync(p => p.SetProperty(s => s.Enabled, request.Enabled));

        return new();
    }

    /// <summary>
    /// Gets the device groups.
    /// </summary>
    /// <param name="request">The <see cref="EmptyMessage"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="DeviceGroupsResponse"/>.</returns>
    public override async Task<DeviceGroupsResponse> GetDeviceGroups(EmptyMessage request, ServerCallContext context)
    {
        var devices = await _openRGBService.GetDeviceListAsync(context.CancellationToken);

        var groups = devices
            .Select(d => new DeviceGroup()
            {
                Name = d.Name,
                Vendor = d.Vendor,
                Type = d.Type.ToFriendlyName()
            })
            .Distinct()
            .OrderBy(g => g.Name);

        var response = new DeviceGroupsResponse();

        response.Groups.AddRange(groups);

        return response;
    }

    /// <summary>
    /// Gets the devices in a group.
    /// </summary>
    /// <param name="request">The <see cref="GetGroupDevicesRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="DevicesResponse"/>.</returns>
    public override async Task<DevicesResponse> GetGroupDevices(GetGroupDevicesRequest request, ServerCallContext context)
    {
        var devices = await _openRGBService.GetDeviceListAsync(context.CancellationToken);

        var groupDevices = devices
            .Where(d => d.Name == request.GroupName)
            .Select((d, i) => new Device()
            {
                Index = d.Index,
                Name = $"{d.Name} {i + 1}"
            })
            .OrderBy(g => g.Name);

        var response = new DevicesResponse();

        response.Devices.AddRange(groupDevices);

        return response;
    }

    /// <summary>
    /// Gets info for a specific device.
    /// </summary>
    /// <param name="request">The <see cref="GetDeviceInfoRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="DeviceInfoResponse"/>.</returns>
    public override async Task<DeviceInfoResponse> GetDeviceInfo(GetDeviceInfoRequest request, ServerCallContext context)
    {
        var devices = await _openRGBService.GetDeviceListAsync(context.CancellationToken);

        var device = devices.First(d => d.Index == request.DeviceIndex);

        var response = new DeviceInfoResponse()
        {
            Index = device.Index,
            Name = device.Name,
            Type = device.Type.ToFriendlyName(),
            Vendor = string.IsNullOrWhiteSpace(device.Vendor) ? "N/A" : device.Vendor,
            Description = string.IsNullOrWhiteSpace(device.Description) ? "N/A" : device.Description,
            Version = string.IsNullOrWhiteSpace(device.Version) ? "N/A" : device.Version,
            Serial = string.IsNullOrWhiteSpace(device.Serial) ? "N/A" : device.Serial,
            Location = string.IsNullOrWhiteSpace(device.Location) ? "N/A" : device.Location
        };

        return response;
    }

    /// <summary>
    /// Gets zones for a specific device.
    /// </summary>
    /// <param name="request">The <see cref="GetDeviceZonesRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="DeviceZonesResponse"/>.</returns>
    public override async Task<DeviceZonesResponse> GetDeviceZones(GetDeviceZonesRequest request, ServerCallContext context)
    {
        var devices = await _openRGBService.GetDeviceListAsync(context.CancellationToken);

        var device = devices.First(d => d.Index == request.DeviceIndex);

        var deviceZones = device.Zones
            .Select(z => new DeviceZone()
            {
                Index = z.Index,
                Name = z.Name,
                MinimumLeds = z.MinimumLeds,
                MaximumLeds = z.MaximumLeds,
                LedCount = z.LedCount
            })
            .OrderBy(g => g.Name);

        var response = new DeviceZonesResponse();

        response.Zones.AddRange(deviceZones);

        return response;
    }

    /// <summary>
    /// Resizes a zone for a specific device.
    /// </summary>
    /// <param name="request">The <see cref="ResizeDeviceZoneRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> ResizeDeviceZone(ResizeDeviceZoneRequest request, ServerCallContext context)
    {
        _eventDispatcher.ZoneResized = true;
        await _openRGBService.ResizeZoneAsync(request.DeviceIndex, request.ZoneIndex, request.NewSize, context.CancellationToken);

        return new EmptyMessage();
    }
}
