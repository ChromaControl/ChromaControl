﻿// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.EntityFrameworkCore;

namespace ChromaControl.Service.Data.Services;

/// <summary>
/// Service that performs database migrations.
/// </summary>
public partial class MigrationService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MigrationService> _logger;

    [LoggerMessage(0, LogLevel.Information, "Starting configuration database migration...", EventName = "DatabaseMigrationStarted")]
    private static partial void LogStartingMigration(ILogger logger);

    [LoggerMessage(1, LogLevel.Error, "Configuration database migration failed, retrying in 15 seconds...", EventName = "DatabaseMigrationFailed")]
    private static partial void LogMigrationFailed(ILogger logger, Exception ex);

    [LoggerMessage(2, LogLevel.Error, "Configuration database migration aborted, failure threshold exceeded.", EventName = "DatabaseMigrationAborted")]
    private static partial void LogMigrationAborted(ILogger logger, Exception ex);

    [LoggerMessage(3, LogLevel.Information, "Configuration database migration completed successfully.", EventName = "DatabaseMigrationFinished")]
    private static partial void LogMigrationComplete(ILogger logger);

    /// <summary>
    /// Creates a <see cref="MigrationService"/> instance.
    /// </summary>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
    /// <param name="logger">The <see cref="ILogger"/>.</param>
    public MigrationService(IServiceProvider serviceProvider, ILogger<MigrationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var hostLifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

        LogStartingMigration(_logger);

        var failureCounter = 0;

        while (true && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                await context.Database.EnsureCreatedAsync(cancellationToken);
                await context.Database.ExecuteSqlRawAsync("PRAGMA journal_mode = delete;", cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                GC.Collect();
            }
            catch (Exception ex)
            {
                failureCounter++;

                if (failureCounter < 5)
                {
                    LogMigrationFailed(_logger, ex);

                    await Task.Delay(15000, cancellationToken);
                }
                else
                {
                    LogMigrationAborted(_logger, ex);

                    hostLifetime.StopApplication();

                    break;
                }

                continue;
            }

            break;
        }

        LogMigrationComplete(_logger);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
