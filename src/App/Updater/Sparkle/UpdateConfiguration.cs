// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Protos.Settings;
using Google.Protobuf.WellKnownTypes;
using NetSparkleUpdater.Configurations;
using NetSparkleUpdater.Interfaces;

namespace ChromaControl.App.Updater.Sparkle;

/// <summary>
/// The update configuration.
/// </summary>
public class UpdateConfiguration : Configuration
{
    private readonly SettingsGrpc.SettingsGrpcClient _settingsClient;

    /// <summary>
    /// Creates a <see cref="UpdateConfiguration"/> instance.
    /// </summary>
    /// <param name="assemblyAccessor">The <see cref="IAssemblyAccessor"/>.</param>
    /// <param name="settingsClient">The <see cref="SettingsGrpc.SettingsGrpcClient"/>.</param>
    public UpdateConfiguration(IAssemblyAccessor assemblyAccessor, SettingsGrpc.SettingsGrpcClient settingsClient) : base(assemblyAccessor)
    {
        _settingsClient = settingsClient;
    }

    /// <inheritdoc/>
    public override void TouchProfileTime()
    {
        base.TouchProfileTime();
        SaveValues();
    }

    /// <inheritdoc/>
    public override void TouchCheckTime()
    {
        base.TouchCheckTime();
        SaveValues();
    }

    /// <inheritdoc/>
    public override void SetVersionToSkip(string version)
    {
        base.SetVersionToSkip(version);
        SaveValues();
    }

    /// <inheritdoc/>
    public override void Reload()
    {
        CheckForUpdate = _settingsClient.GetBool(new()
        {
            Module = "Updater",
            Name = nameof(CheckForUpdate)
        }).Value;

        LastCheckTime = _settingsClient.GetDateTime(new()
        {
            Module = "Updater",
            Name = nameof(LastCheckTime)
        }).Value.ToDateTime().ToLocalTime();

        LastVersionSkipped = _settingsClient.GetString(new()
        {
            Module = "Updater",
            Name = nameof(LastVersionSkipped)
        }).Value;

        DidRunOnce = _settingsClient.GetBool(new()
        {
            Module = "Updater",
            Name = nameof(DidRunOnce)
        }).Value;

        IsFirstRun = !DidRunOnce;

        PreviousVersionOfSoftwareRan = _settingsClient.GetString(new()
        {
            Module = "Updater",
            Name = nameof(PreviousVersionOfSoftwareRan)
        }).Value;

        if (IsFirstRun)
        {
            SaveDidRunOnceAsTrue();
        }
        else
        {
            SaveValues();
        }

        LastConfigUpdate = _settingsClient.GetDateTime(new()
        {
            Module = "Updater",
            Name = nameof(LastConfigUpdate)
        }).Value.ToDateTime().ToLocalTime();
    }

    private void SaveDidRunOnceAsTrue()
    {
        var initial = DidRunOnce;
        DidRunOnce = true;
        SaveValues();
        DidRunOnce = initial;
    }

    private void SaveValues()
    {
        _settingsClient.SetBool(new()
        {
            Module = "Updater",
            Name = nameof(CheckForUpdate),
            Value = true
        });

        _settingsClient.SetDateTime(new()
        {
            Module = "Updater",
            Name = nameof(LastCheckTime),
            Value = Timestamp.FromDateTime(LastCheckTime.ToUniversalTime())
        });

        _settingsClient.SetString(new()
        {
            Module = "Updater",
            Name = nameof(LastVersionSkipped),
            Value = LastVersionSkipped
        });

        _settingsClient.SetBool(new()
        {
            Module = "Updater",
            Name = nameof(DidRunOnce),
            Value = DidRunOnce
        });

        _settingsClient.SetDateTime(new()
        {
            Module = "Updater",
            Name = nameof(LastConfigUpdate),
            Value = Timestamp.FromDateTime(LastConfigUpdate.ToUniversalTime())
        });

        _settingsClient.SetString(new()
        {
            Module = "Updater",
            Name = nameof(PreviousVersionOfSoftwareRan),
            Value = InstalledVersion
        });
    }
}
