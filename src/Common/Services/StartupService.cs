// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChromaControl.Common.Services;

/// <summary>
/// The Chroma Control startup service.
/// </summary>
internal sealed partial class StartupService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IConfiguration _configuration;

    [LoggerMessage(0, LogLevel.Information, "Chroma Control environment: {environment}")]
    private static partial void LogEnvironment(ILogger logger, string environment);

    [LoggerMessage(1, LogLevel.Information, "Chroma Control version: {version}")]
    private static partial void LogVersion(ILogger logger, string? version);

    [LoggerMessage(2, LogLevel.Information, "Chroma Control app path: {appPath}")]
    private static partial void LogAppPath(ILogger logger, string appPath);

    [LoggerMessage(3, LogLevel.Information, "Chroma Control data path: {dataPath}")]
    private static partial void LogDataPath(ILogger logger, string dataPath);

    /// <summary>
    /// Creates a <see cref="StartupService"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/>.</param>
    /// <param name="hostEnvironment">The <see cref="IHostEnvironment"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    public StartupService(ILogger<StartupService> logger, IHostEnvironment hostEnvironment, IConfiguration configuration)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _configuration = configuration;
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        LogEnvironment(_logger, _hostEnvironment.EnvironmentName);
        LogVersion(_logger, _configuration.GetSection("ChromaControl")["VERSION"]);
        LogAppPath(_logger, _configuration.GetChromaControlPath("app").Trim('\\'));
        LogDataPath(_logger, _configuration.GetChromaControlPath("data"));
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
