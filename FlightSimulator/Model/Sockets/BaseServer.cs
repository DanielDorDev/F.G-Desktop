using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model.Sockets
{
    abstract class BaseServer : ITelnetServer
    {
        public abstract int Port { get; }   // Return port number.

        public delegate void ServerEvent();     // Server event delegate.
        public event ServerEvent NotifyConnected;   // Event server connected.
        public event ServerEvent NotifyDisconnected;    // Event server disconnected.
        public event ServerEvent NotifyDataRecv;    // Event server got data.

        public abstract void Connect(); // Connect to server and listen to client.
        public abstract void ReConnect(int port);   // Reopen server by ip.
        public abstract string Read();  // Read data that server got.
        public abstract void Disconnect();  // Disconnect from the server.

        // Notify that server opened.
        public void NotifyServerConnectedEvent()
        {
            ServerEvent handler = NotifyConnected;
            NotifyConnected?.Invoke();
        }

        // Notify that server close.
        public void NotifyServerDisconnectedEvent()
        {
            ServerEvent handler = NotifyDisconnected;
            NotifyDisconnected?.Invoke();
        }

        // Notify that data recv.
        public void NotifyServerDataRecvEvent()
        {
            ServerEvent handler = NotifyDataRecv;
            NotifyDataRecv?.Invoke();
        }
    }
}
