// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.App.Updater.Services;
using ChromaControl.App.Updater.Sparkle;
using ChromaControl.Common.Extensions;
using NetSparkleUpdater;
using NetSparkleUpdater.AssemblyAccessors;
using NetSparkleUpdater.Configurations;
using NetSparkleUpdater.Enums;
using NetSparkleUpdater.Interfaces;
using NetSparkleUpdater.SignatureVerifiers;
using System.IO;

namespace ChromaControl.App.Updater;

/// <summary>
/// Updater extension methods.
/// </summary>
public static class UpdaterExtensions
{
    /// <summary>
    /// Adds updater configuration to a <see cref="BlazorDesktopHostBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="BlazorDesktopHostBuilder"/> to add configuration to.</param>
    /// <returns>The <see cref="BlazorDesktopHostBuilder"/> to continue adding configuration to.</returns>
    public static BlazorDesktopHostBuilder ConfigureUpdater(this BlazorDesktopHostBuilder builder)
    {
        builder.ConfigureServices();

        return builder;
    }

    private static BlazorDesktopHostBuilder ConfigureServices(this BlazorDesktopHostBuilder builder)
    {
        builder.Services.AddSingleton<ISignatureVerifier, Ed25519Checker>(_ =>
        {
            return new Ed25519Checker(SecurityMode.Unsafe);
        });

        builder.Services.AddSingleton<IAssemblyAccessor, AsmResolverAccessor>(_ =>
        {
            return new AsmResolverAccessor(null);
        });

        builder.Services.AddSingleton<Configuration, JSONConfiguration>(services =>
        {
            var accessor = services.GetRequiredService<IAssemblyAccessor>();
            var config = services.GetRequiredService<IConfiguration>();
            var jsonPath = Path.Combine(config.GetChromaControlPath("environment"), "Updater.json");

            return new JSONConfiguration(accessor, jsonPath);
        });

        builder.Services.AddSingleton<NetSparkleUpdater.Interfaces.ILogger, UpdateLogger>();
        builder.Services.AddSingleton<IAppCastDataDownloader, UpdateInfoDownloader>();
        builder.Services.AddSingleton<IAppCastHandler, UpdateInfoHandler>();
        builder.Services.AddSingleton<IUpdateDownloader, UpdateDownloader>();

        builder.Services.AddSingleton<SparkleUpdater>(services =>
        {
            var signatureVerifier = services.GetRequiredService<ISignatureVerifier>();
            var logger = services.GetRequiredService<NetSparkleUpdater.Interfaces.ILogger>();
            var updateInfoDownloader = services.GetRequiredService<IAppCastDataDownloader>();
            var updateDownloader = services.GetRequiredService<IUpdateDownloader>();
            var updateInfoHandler = services.GetRequiredService<IAppCastHandler>();
            var configuration = services.GetRequiredService<Configuration>();

            return new("https://api.github.com/repos/AndrewBabbitt97/Updates/releases", signatureVerifier)
            {
                LogWriter = logger,
                AppCastDataDownloader = updateInfoDownloader,
                AppCastHandler = updateInfoHandler,
                UpdateDownloader = updateDownloader,
                Configuration = configuration,
                CustomInstallerArguments = "/silent",
                CheckServerFileName = false,
                RelaunchAfterUpdate = true
            };
        });

        builder.Services.AddHostedService<UpdateWorker>();
        builder.Services.AddSingleton<UpdateService>();

        return builder;
    }
}
