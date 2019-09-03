using System;
using System.Collections.Generic;
using System.Net;

namespace Heirloom.IO.Networking
{
    public interface INetworkListener : IDisposable
    {
        /// <summary>
        /// Local endpoint.
        /// </summary>
        IPEndPoint Local { get; }

        /// <summary>
        /// The list of currently known connections.
        /// </summary>
        IReadOnlyList<NetworkConnection> Connections { get; }

        /// <summary>
        /// Event invoked when a client connection has connected.
        /// </summary>
        event Action<NetworkConnection> Connected;

        /// <summary>
        /// Event invoked when a client connection has disconnected.
        /// </summary>
        event Action<NetworkConnection> Disconnected;

        /// <summary>
        /// Send a message to all known connections.
        /// </summary>
        void SendMessage(int type, byte[] data);
    }
}
