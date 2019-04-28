using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model.Sockets
{
    abstract class BaseClient : ITelnetClient
    {
        public abstract string Ip { get; }
        public abstract int Port { get; }

        public delegate void ClientEvent();
        public event ClientEvent NotifyConnected;
        public event ClientEvent NotifyDisconnected;

        public abstract void Connect();
        public abstract void ReConnect(string ip, int port);
        public abstract void Write(string command);
        public abstract void Disconnect();

        public void NotifyClientConnectedEvent()
        {
            ClientEvent handler = NotifyConnected;
            NotifyConnected?.Invoke();
        }
        public void NotifyClientDisconnectedEvent()
        {
            ClientEvent handler = NotifyDisconnected;
            NotifyDisconnected?.Invoke();
        }
    }
}
