// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MediatR;

namespace ChromaControl.App.Core.Mediator;

/// <summary>
/// Defines a handler for a query.
/// </summary>
/// <typeparam name="TQuery">The type of the query being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse, string>> where TQuery : IQuery<TResponse>;
