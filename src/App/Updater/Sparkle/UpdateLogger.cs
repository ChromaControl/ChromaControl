// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace ChromaControl.App.Updater.Sparkle;

/// <summary>
/// The update logger.
/// </summary>
public class UpdateLogger : NetSparkleUpdater.Interfaces.ILogger
{
    private readonly ILogger<UpdateLogger> _logger;

    /// <summary>
    /// Creates an <see cref="UpdateLogger"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use for logging.</param>
    public UpdateLogger(ILogger<UpdateLogger> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public void PrintMessage(string message, params object[] arguments)
    {
#pragma warning disable CA1848 // Use the LoggerMessage delegates
#pragma warning disable CA2254 // Template should be a static expression
        if (message.Contains("Error", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError(1, message, arguments);
        }
        else
        {
            _logger.LogTrace(0, message, arguments);
        }
#pragma warning restore CA2254 // Template should be a static expression
#pragma warning restore CA1848 // Use the LoggerMessage delegates
    }
}
