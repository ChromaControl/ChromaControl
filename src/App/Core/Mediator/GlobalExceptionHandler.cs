// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Grpc.Core;
using MediatR;
using MediatR.Pipeline;

namespace ChromaControl.App.Core.Mediator;

/// <summary>
/// The global mediator exception handler.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
/// <typeparam name="TException">The exception type.</typeparam>
public partial class GlobalExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IBaseRequest
    where TResponse : struct
    where TException : Exception
{
    private readonly ILogger _logger;

    [LoggerMessage(0, LogLevel.Warning, "Mediator exception handled: {message}")]
    private static partial void LogExceptionHandledMessage(ILogger logger, Exception exception, string message);

    /// <summary>
    /// Creates a <see cref="GlobalExceptionHandler{TRequest, TResponse, TException}"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/>.</param>
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler<TRequest, TResponse, TException>> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles an incoming exception.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="state">The <see cref="RequestExceptionHandlerState{TResponse}"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        if (exception is RpcException)
        {
            state.SetHandled(CreateResponse(exception, "Unable to communicate with the lighting service."));
        }
        else
        {
            state.SetHandled(CreateResponse(exception, "Unknown error occured."));
        }

        return Task.CompletedTask;
    }

    private TResponse CreateResponse(Exception exception, string message)
    {
        var responseType = typeof(TResponse);

        if (responseType.IsGenericType)
        {
            var valueType = responseType.GetGenericArguments()[0];
            var errorType = responseType.GetGenericArguments()[1];
            var resultType = typeof(Result<,>).MakeGenericType(valueType, errorType);

            var failure = Failure(message);
            var result = Activator.CreateInstance(resultType, failure) as TResponse?;

            if (result.HasValue)
            {
                LogExceptionHandledMessage(_logger, exception, message);

                return result.Value;
            }
        }

        throw new NotImplementedException("Unable to create mediator result, giving up.", exception);
    }
}
