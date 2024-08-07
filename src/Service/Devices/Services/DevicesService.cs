// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Devices;
using ChromaControl.Service.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Devices.Services;

/// <summary>
/// The devices service.
/// </summary>
public class DevicesService : DevicesGrpc.DevicesGrpcBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Creates a <see cref="DevicesService"/> instance.
    /// </summary>
    /// <param name="context">The <see cref="AppDbContext"/>.</param>
    public DevicesService(AppDbContext context)
    {
        _context = context;
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
}
