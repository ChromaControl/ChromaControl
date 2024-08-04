// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MediatR;

namespace ChromaControl.App.Core.Mediator;

/// <summary>
/// Marker interface to represent a query.
/// </summary>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse, string>>;
