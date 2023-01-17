// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Common.Extensions;
using ChromaControl.Core.API.Extensions;
using ChromaControl.Core.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddChromaControl(args);

builder.UseChromaControlSocket();

var app = builder.Build();

app.MapGrpcService<DriversService>();

app.Run();
