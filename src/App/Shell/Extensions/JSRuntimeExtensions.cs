// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.JSInterop;

namespace ChromaControl.App.Shell.Extensions;

/// <summary>
/// <see cref="IJSRuntime"/> extension methods.
/// </summary>
public static class JSRuntimeExtensions
{
    /// <summary>
    /// Sets a local storage item.
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
    /// <param name="key">The key to set.</param>
    /// <param name="value">The value.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    public static async Task LocalStorageSetItem(this IJSRuntime jsRuntime, string key, string value)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    /// <summary>
    /// Gets a local storage item.
    /// </summary>
    /// <param name="jsRuntime">The <see cref="IJSRuntime"/>.</param>
    /// <param name="key">The key to get.</param>
    /// <returns>The value.</returns>
    public static async Task<string?> LocalStorageGetItem(this IJSRuntime jsRuntime, string key)
    {
        return await jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
    }
}
