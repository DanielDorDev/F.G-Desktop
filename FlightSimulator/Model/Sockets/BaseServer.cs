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
        public abstract int Port { get; }

        public delegate void ServerEvent();
        public event ServerEvent NotifyConnected;
        public event ServerEvent NotifyDisconnected;
        public event ServerEvent NotifyDataRecv;

        public abstract void Connect();
        public abstract void ReConnect(int port);
        public abstract string Read();
        public abstract void Disconnect();

        public void NotifyServerConnectedEvent()
        {
            ServerEvent handler = NotifyConnected;
            NotifyConnected?.Invoke();
        }
        public void NotifyServerDisconnectedEvent()
        {
            ServerEvent handler = NotifyDisconnected;
            NotifyDisconnected?.Invoke();
        }
        public void NotifyServerDataRecvEvent()
        {
            ServerEvent handler = NotifyDataRecv;
            NotifyDataRecv?.Invoke();
        }
    }
}
