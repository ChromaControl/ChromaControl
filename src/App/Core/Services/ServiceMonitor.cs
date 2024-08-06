// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using System.Diagnostics;
using System.IO;

namespace ChromaControl.App.Core.Services;

/// <summary>
/// A <see cref="BackgroundService"/> that monitors the lighting service.
/// </summary>
public partial class ServiceMonitor : IHostedService
{
    private Process? _serviceProcess;
    private readonly ILogger _logger;
    private readonly string _servicePath;

    [LoggerMessage(0, LogLevel.Warning, "Unable to find the lighting service executable.")]
    private static partial void LogServiceExecutableMissing(ILogger logger);

    /// <summary>
    /// Creates a <see cref="ServiceMonitor"/> instance.
    /// </summary>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/>.</param>
    public ServiceMonitor(IConfiguration configuration, ILogger<ServiceMonitor> logger)
    {
        _servicePath = Path.Combine(configuration.GetChromaControlPath("app"), "ChromaControl.Service.exe");
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        StartProcess();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_serviceProcess != null)
        {
            _serviceProcess.Exited -= ServiceProcessExited;
            _serviceProcess.Dispose();
        }

        return Task.CompletedTask;
    }

    private void StartProcess()
    {
        if (_serviceProcess != null)
        {
            _serviceProcess.Exited -= ServiceProcessExited;
            _serviceProcess.Dispose();
        }

        var processes = Process.GetProcessesByName("ChromaControl.Service");

        if (processes.Length == 0)
        {
            if (File.Exists(_servicePath))
            {
                _serviceProcess = Process.Start(_servicePath);
            }
            else
            {
                _serviceProcess = null;
                LogServiceExecutableMissing(_logger);
            }
        }
        else
        {
            _serviceProcess = processes[0];

            try
            {
                _serviceProcess.EnableRaisingEvents = true;
            }
            catch (Exception) { }
        }

        if (_serviceProcess != null)
        {
            _serviceProcess.Exited += ServiceProcessExited;
        }
    }

    private void ServiceProcessExited(object? sender, EventArgs e)
    {
        StartProcess();
    }
}
