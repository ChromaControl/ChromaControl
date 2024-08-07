// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Settings;
using ChromaControl.Service.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Settings.Services;

/// <summary>
/// The settings service.
/// </summary>
public class SettingsService : SettingsGrpc.SettingsGrpcBase
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Creates a <see cref="SettingsService"/> instance.
    /// </summary>
    /// <param name="context">The <see cref="AppDbContext"/>.</param>
    public SettingsService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a string setting.
    /// </summary>
    /// <param name="request">The <see cref="GetSettingRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="StringSettingResponse"/>.</returns>
    public override async Task<StringSettingResponse> GetString(GetSettingRequest request, ServerCallContext context)
    {
        var value = await _context.Settings
            .Where(s => s.Module == request.Module && s.Name == request.Name)
            .Select(s => s.StringValue)
            .FirstOrDefaultAsync();

        if (value == null)
        {
            return new();
        }
        else
        {
            return new()
            {
                Value = value
            };
        }
    }

    /// <summary>
    /// Gets a bool setting.
    /// </summary>
    /// <param name="request">The <see cref="GetSettingRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="BoolSettingResponse"/>.</returns>
    public override async Task<BoolSettingResponse> GetBool(GetSettingRequest request, ServerCallContext context)
    {
        var value = await _context.Settings
            .Where(s => s.Module == request.Module && s.Name == request.Name)
            .Select(s => s.BoolValue)
            .FirstOrDefaultAsync();

        if (value == null)
        {
            return new();
        }
        else
        {
            return new()
            {
                Value = value ?? false
            };
        }
    }

    /// <summary>
    /// Gets a date time setting.
    /// </summary>
    /// <param name="request">The <see cref="GetSettingRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="DateTimeSettingResponse"/>.</returns>
    public override async Task<DateTimeSettingResponse> GetDateTime(GetSettingRequest request, ServerCallContext context)
    {
        var value = await _context.Settings
            .Where(s => s.Module == request.Module && s.Name == request.Name)
            .Select(s => s.DateTimeValue)
            .FirstOrDefaultAsync();

        if (value == null)
        {
            return new();
        }
        else
        {
            return new()
            {
                Value = Timestamp.FromDateTime(value ?? DateTime.Now)
            };
        }
    }

    /// <summary>
    /// Sets a string setting.
    /// </summary>
    /// <param name="request">The <see cref="SetStringRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> SetString(SetStringRequest request, ServerCallContext context)
    {
        var exists = await _context.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _context.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.StringValue, request.Value));
        }
        else
        {
            _context.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                StringValue = request.Value
            });

            await _context.SaveChangesAsync();
        }

        return new();
    }

    /// <summary>
    /// Sets a bool setting.
    /// </summary>
    /// <param name="request">The <see cref="SetBoolRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> SetBool(SetBoolRequest request, ServerCallContext context)
    {
        var exists = await _context.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _context.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.BoolValue, request.Value));
        }
        else
        {
            _context.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                BoolValue = request.Value
            });

            await _context.SaveChangesAsync();
        }

        return new();
    }

    /// <summary>
    /// Sets a date time setting.
    /// </summary>
    /// <param name="request">The <see cref="SetDateTimeRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptyMessage"/>.</returns>
    public override async Task<EmptyMessage> SetDateTime(SetDateTimeRequest request, ServerCallContext context)
    {
        var exists = await _context.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _context.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.DateTimeValue, request.Value.ToDateTime()));
        }
        else
        {
            _context.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                DateTimeValue = request.Value.ToDateTime()
            });

            await _context.SaveChangesAsync();
        }

        return new();
    }
}
