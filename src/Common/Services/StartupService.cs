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
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _hostEnvironment;

    [LoggerMessage(0, LogLevel.Information, "Chroma Control {property}: {value}", EventName = "Startup")]
    private static partial void LogStartup(ILogger logger, string property, string? value);

    /// <summary>
    /// Creates a <see cref="StartupService"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <param name="hostEnvironment">The <see cref="IHostEnvironment"/>.</param>
    public StartupService(ILogger<StartupService> logger, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        LogStartup(_logger, "version", _configuration.GetSection("ChromaControl")["VERSION"]);
        LogStartup(_logger, "environment", _hostEnvironment.EnvironmentName);
        LogStartup(_logger, "app path", _configuration.GetChromaControlPath("app").Trim('\\'));
        LogStartup(_logger, "data path", _configuration.GetChromaControlPath("data"));
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
