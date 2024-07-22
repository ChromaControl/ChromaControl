// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.App.Core;
using ChromaControl.App.Shell;

var builder = BlazorDesktopHostBuilder.CreateDefault(args);

builder.ConfigureCore()
    .ConfigureShell()
    .ConfigureHome();

await builder.Build().RunAsync();
