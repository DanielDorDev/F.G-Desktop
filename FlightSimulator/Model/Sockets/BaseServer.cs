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
        public abstract int Port { get; } // Return port number.

        public delegate void ServerEvent();  // Server event delegate.
        public event ServerEvent NotifyConnected; // Event server connected.
        public event ServerEvent NotifyDisconnected; // Event server disconnected.
        public event ServerEvent NotifyDataRecv; // Event server got data.

        public abstract void Connect(); // Connect to server and listen to client.
        public abstract void ReConnect(int port);  // Reopen server by ip.
        public abstract string Read(); // Read data that server got.
        public abstract void Disconnect();  // Disconnect from the server.

        public void NotifyServerConnectedEvent() // Notify that server opened.
        {
            ServerEvent handler = NotifyConnected;
            NotifyConnected?.Invoke();
        }
        public void NotifyServerDisconnectedEvent() // Notify that server close.
        {
            ServerEvent handler = NotifyDisconnected;
            NotifyDisconnected?.Invoke();
        }
        public void NotifyServerDataRecvEvent() // Notify that data recv.
        {
            ServerEvent handler = NotifyDataRecv;
            NotifyDataRecv?.Invoke();
        }
    }
}
