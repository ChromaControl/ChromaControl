// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

var builder = DistributedApplication.CreateBuilder(args);

var service = builder.AddProject<Projects.ChromaControl_Service>("service", "http");

builder.AddProject<Projects.ChromaControl_App>("app")
    .WithReference(service);

builder.Build().Run();
