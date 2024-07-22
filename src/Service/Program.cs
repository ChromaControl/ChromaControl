﻿// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Core;
using ChromaControl.Service.Home;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore();

var app = builder.Build();

app.UseHome();

app.Run();
