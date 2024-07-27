// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BlazorDesktop.Hosting;
using ChromaControl.App.Core;
using ChromaControl.App.Home;
using ChromaControl.App.Shell;
using ChromaControl.App.Updater;

var builder = BlazorDesktopHostBuilder.CreateDefault(args);

builder.ConfigureCore()
    .ConfigureUpdater()
    .ConfigureShell()
    .ConfigureHome();

await builder.Build().RunAsync();
