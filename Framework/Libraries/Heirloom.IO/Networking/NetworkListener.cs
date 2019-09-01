using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Heirloom.IO.Networking
{
    public class NetworkListener : INetworkListener
    {
        private readonly TcpListener _listener;

        private readonly List<NetworkConnection> _connections;

        private readonly Thread _thread;

        private bool _isDisposed = false;

        #region Constructors

        public NetworkListener(string address, int port)
            : this(IPAddress.Parse(address), port)
        { }

        public NetworkListener(IPAddress address, int port)
            : this(new IPEndPoint(address, port))
        { }

        public NetworkListener(IPEndPoint local)
        {
            // 
            _listener = new TcpListener(local);
            _listener.AllowNatTraversal(true);
            _listener.Start();

            // 
            _connections = new List<NetworkConnection>();

            // Begin accept thread
            _thread = new Thread(AcceptThread);
            _thread.Start();
        }

        #endregion

        #region Events

        public event Action<NetworkConnection> Disconnected;

        public event Action<NetworkConnection> Connected;

        #endregion

        #region Properties

        public IPEndPoint Local => _listener.LocalEndpoint as IPEndPoint;

        public IReadOnlyList<NetworkConnection> Connections => _connections;

        #endregion

        private void AcceptThread()
        {
            while (!_isDisposed)
            {
                try
                {
                    // Accept next available tcp client
                    var client = _listener.AcceptTcpClient();
                    var endPoint = client.Client.RemoteEndPoint;

                    // Construct a connection object
                    var connection = new NetworkConnection(client);

                    // Bind disconnect event
                    connection.Disconnected += () =>
                    {
                        lock (_connections)
                        {
                            // 
                            _connections.Remove(connection);

                            // Invoke disconnected callback
                            Disconnected?.Invoke(connection);
                        }
                    };

                    // 
                    lock (_connections)
                    {
                        // 
                        _connections.Add(connection);

                        // Invoke connection callback
                        Connected?.Invoke(connection);
                    }
                }
                catch (IOException ioex)
                {
                    Console.WriteLine(ioex);
                    Dispose();
                }
            }
        }

        public void SendMessage(int type, byte[] data)
        {
            lock (_connections)
            {
                // sends message to each connection
                // todo: why reverse order?
                for (var i = _connections.Count - 1; i >= 0; i--)
                {
                    var connection = _connections[i];
                    if (connection.IsConnected)
                    {
                        connection.SendMessage(type, data);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_isDisposed == false)
            {
                // Stop listener
                _listener.Stop();

                // 
                lock (_connections)
                {
                    // Disconnect each client connection
                    foreach (var connection in _connections)
                    {
                        connection.Disconnect();
                    }

                    // Clear connections
                    _connections.Clear();
                }

                // 
                _isDisposed = true;
            }
        }
    }
}
