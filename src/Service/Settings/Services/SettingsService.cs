// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Settings;
using Grpc.Core;

namespace ChromaControl.Service.Settings.Services;

/// <summary>
/// The settings service.
/// </summary>
public class SettingsService : SettingsGrpc.SettingsGrpcBase
{
    /// <summary>
    /// Gets a string setting.
    /// </summary>
    /// <param name="request">The <see cref="GetSettingRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="StringSettingResponse"/>.</returns>
    public override Task<StringSettingResponse> GetString(GetSettingRequest request, ServerCallContext context)
    {
        return Task.FromResult<StringSettingResponse>(new()
        {
            Value = "Light"
        });
    }

    /// <summary>
    /// Sets a string setting.
    /// </summary>
    /// <param name="request">The <see cref="SetStringRequest"/>.</param>
    /// <param name="context">The <see cref="ServerCallContext"/>.</param>
    /// <returns>A <see cref="EmptySettingResponse"/>.</returns>
    public override Task<EmptySettingResponse> SetString(SetStringRequest request, ServerCallContext context)
    {
        return Task.FromResult<EmptySettingResponse>(new());
    }
}
