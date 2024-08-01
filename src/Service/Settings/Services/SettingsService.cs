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
    private readonly Database _database;

    /// <summary>
    /// Creates a <see cref="SettingsService"/> instance.
    /// </summary>
    /// <param name="database">The <see cref="Database"/>.</param>
    public SettingsService(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Gets a string setting.
    /// </summary>
    /// <param name="request">The <see cref="GetSettingRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="StringSettingResponse"/>.</returns>
    public override async Task<StringSettingResponse> GetString(GetSettingRequest request, ServerCallContext context)
    {
        var value = await _database.Settings
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
        var value = await _database.Settings
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
        var value = await _database.Settings
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
    /// <returns>A <see cref="EmptySettingResponse"/>.</returns>
    public override async Task<EmptySettingResponse> SetString(SetStringRequest request, ServerCallContext context)
    {
        var exists = await _database.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _database.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.StringValue, request.Value));
        }
        else
        {
            _database.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                StringValue = request.Value
            });

            await _database.SaveChangesAsync();
        }

        return new();
    }

    /// <summary>
    /// Sets a bool setting.
    /// </summary>
    /// <param name="request">The <see cref="SetBoolRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptySettingResponse"/>.</returns>
    public override async Task<EmptySettingResponse> SetBool(SetBoolRequest request, ServerCallContext context)
    {
        var exists = await _database.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _database.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.BoolValue, request.Value));
        }
        else
        {
            _database.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                BoolValue = request.Value
            });

            await _database.SaveChangesAsync();
        }

        return new();
    }

    /// <summary>
    /// Sets a date time setting.
    /// </summary>
    /// <param name="request">The <see cref="SetDateTimeRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptySettingResponse"/>.</returns>
    public override async Task<EmptySettingResponse> SetDateTime(SetDateTimeRequest request, ServerCallContext context)
    {
        var exists = await _database.Settings
            .AnyAsync(s => s.Module == request.Module && s.Name == request.Name);

        if (exists)
        {
            await _database.Settings
                .Where(s => s.Module == request.Module && s.Name == request.Name)
                .ExecuteUpdateAsync(p => p.SetProperty(s => s.DateTimeValue, request.Value.ToDateTime()));
        }
        else
        {
            _database.Settings.Add(new()
            {
                Module = request.Module,
                Name = request.Name,
                DateTimeValue = request.Value.ToDateTime()
            });

            await _database.SaveChangesAsync();
        }

        return new();
    }
}
