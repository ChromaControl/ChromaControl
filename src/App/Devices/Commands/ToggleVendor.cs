// Licensed to the Chroma Control Contributors under one or more agreements.
// The Chroma Control Contributors licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ChromaControl.App.Core.Mediator;
using ChromaControl.Common.Protos.Devices;

namespace ChromaControl.App.Devices.Commands;

/// <summary>
/// Toggles a vendor.
/// </summary>
public class ToggleVendor
{
    /// <summary>
    /// Toggles a vendor.
    /// </summary>
    /// <param name="Name">The vendor name.</param>
    /// <param name="Enabled">If the vendor is enabled.</param>
    public record struct Command(string Name, bool Enabled) : ICommand;

    /// <summary>
    /// Handles toggling the vendor.
    /// </summary>
    public class Handler : ICommandHandler<Command>
    {
        private readonly DevicesGrpc.DevicesGrpcClient _devicesClient;

        /// <summary>
        /// Creates a <see cref="Handler"/> instance.
        /// </summary>
        /// <param name="devicesClient">The <see cref="DevicesGrpc.DevicesGrpcClient"/>.</param>
        public Handler(DevicesGrpc.DevicesGrpcClient devicesClient)
        {
            _devicesClient = devicesClient;
        }

        /// <inheritdoc/>
        public async Task<Result<int, string>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _devicesClient.ToggleVendorAsync(new()
            {
                Name = request.Name,
                Enabled = request.Enabled
            }, cancellationToken: cancellationToken);

            return Success(0);
        }
    }
}
