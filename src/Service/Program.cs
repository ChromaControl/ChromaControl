// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.Service.Core;
using ChromaControl.Service.Data;
using ChromaControl.Service.Devices;
using ChromaControl.Service.Lighting;
using ChromaControl.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCore()
    .ConfigureData()
    .ConfigureLighting();

var app = builder.Build();

app.UseSettings()
    .UseDevices()
    .UseLighting();

app.Run();
