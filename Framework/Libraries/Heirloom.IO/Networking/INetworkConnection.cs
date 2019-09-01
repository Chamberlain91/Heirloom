using System;
using System.Net;

namespace Heirloom.IO.Networking
{
    public interface INetworkConnection : IDisposable
    {
        IPEndPoint Remote { get; }

        IPEndPoint Local { get; }

        bool IsConnected { get; }

        int AvailableMessages { get; }

        event Action Disconnected;

        /// <summary>
        /// Read an incomming message.
        /// </summary>
        Message ReadMessage();

        /// <summary>
        /// Send an outgoing message.
        /// </summary>
        void SendMessage(int type, byte[] data);

        /// <summary>
        /// Signal the remote that this connection wishes to disconnect.
        /// This method assumes success.
        /// </summary>
        void Disconnect();
    }
}
