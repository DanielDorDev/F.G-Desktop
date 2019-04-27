using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ITelnetServer
    {
        int Port { get;}
        bool IsConnected();
        void Connect();
        void ReConnect(int port);
        string Read();
        void Disconnect();
    }
}
