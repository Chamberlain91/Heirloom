using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Heirloom.IO.Networking
{
    public class NetworkConnection : INetworkConnection
    {
        public static IPEndPoint None = new IPEndPoint(IPAddress.None, 0);

        private readonly TcpClient _client;

        private readonly BinaryReader _reader;
        private readonly BinaryWriter _writer;

        private readonly Queue<Message> _incomingMessages;
        private readonly Thread _incomingThread;

        private bool _isDisposed = false;

        #region Constructors

        public NetworkConnection(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        public NetworkConnection(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        public NetworkConnection(IPEndPoint remote)
            : this(CreateConnectedClient(remote))
        { }

        internal NetworkConnection(TcpClient client)
        {
            _client = client;

            if (_client == null) { Disconnect(); }
            else
            {
                // 
                Remote = _client.Client.RemoteEndPoint as IPEndPoint;
                Local = _client.Client.LocalEndPoint as IPEndPoint;

                // Construct reader and writer objects
                var networkStream = _client.GetStream();
                _writer = new BinaryWriter(networkStream, Encoding.UTF8, true);
                _reader = new BinaryReader(networkStream, Encoding.UTF8, true);

                // 
                _incomingMessages = new Queue<Message>();

                // 
                if (IsConnected)
                {
                    // Begin reading messages on thread
                    _incomingThread = new Thread(IncomingThreadStart);
                    _incomingThread.Start();
                }
                else
                {
                    Disconnect();
                }
            }
        }

        #endregion

        public event Action Disconnected;

        public IPEndPoint Remote { get; }

        public IPEndPoint Local { get; }

        public bool IsConnected
        {
            get
            {
                // No client, no connection
                if (_client == null) { return false; }

                // Was disposed, no connection
                if (_isDisposed) { return false; }

                // 
                var socket = _client.Client;

                // 
                if (!socket.Connected)
                {
                    Disconnect();
                    return false;
                }

                // 
                if (socket.Poll(0, SelectMode.SelectRead))
                {
                    // 
                    var buff = new byte[1];
                    if (socket.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        Disconnect();
                        return false;
                    }
                }

                return true;
            }
        }

        public int AvailableMessages
        {
            get
            {
                lock (_incomingMessages)
                {
                    return _incomingMessages.Count;
                }
            }
        }

        public void SendMessage(int type, byte[] data)
        {
            try
            {
                _writer.Write(type);        // int32  - type
                _writer.Write(data.Length); // int32  - length
                _writer.Write(data);        // byte[] - data   
            }
            catch (Exception)
            {
                Disconnect();
            }
        }

        public Message ReadMessage()
        {
            lock (_incomingMessages)
            {
                if (AvailableMessages > 0)
                {
                    return _incomingMessages.Dequeue();
                }
                else
                {
                    // todo: or exception?
                    return null;
                }
            }
        }

        public void Disconnect()
        {
            if (!_isDisposed)
            {
                // 
                _isDisposed = true;

                if (_client != null)
                {
                    // Close TCP client
                    _client.Close();

                    // Was the thread started?
                    if (_incomingThread != null)
                    {
                        // Wait a maximum of 500 ms before forceful termination
                        if (!_incomingThread.Join(500))
                        {
                            // Aggressively terminate thread
                            _incomingThread.Abort();

                            // Show warning
                            Console.WriteLine("Forceful termination of network connection's incoming message thread.");
                        }
                    }

                    // Dispose reader and writer
                    _reader.Dispose();
                    _writer.Dispose();

                    // Dispose of client entirely
                    _client.Dispose();
                }

                // 
                Disconnected?.Invoke();
            }
        }

        private void IncomingThreadStart()
        {
            // While connected, keep trying to read messages
            while (IsConnected)
            {
                // Wait until more messages are available or disconnect
                SpinWait.SpinUntil(() => _isDisposed || _client.Available > 0, 5);

                // If disposed after wait, break
                if (_isDisposed) { break; }

                // If data is available, read message
                if (_client.Available > 0)
                {
                    // Read a message
                    var type = _reader.ReadInt32();
                    var size = _reader.ReadInt32();
                    var data = _reader.ReadBytes(size);

                    // Append to messages queue
                    lock (_incomingMessages)
                    {
                        var message = new Message(type, data);
                        _incomingMessages.Enqueue(message);
                    }
                }
            }

            if (_client.Connected)
            {
                Console.WriteLine("CONNECTION THREAD EXIT");
                Disconnect();
            }
        }

        private static TcpClient CreateConnectedClient(IPEndPoint remote)
        {
            try
            {
                var tcpClient = new TcpClient();
                tcpClient.Connect(remote);

                // 
                Thread.Sleep(5);

                return tcpClient;
            }
            catch (SocketException)
            {
                return null;
            }
        }

        void IDisposable.Dispose()
        {
            Disconnect();
        }
    }
}
