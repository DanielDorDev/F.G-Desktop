using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ITelnetClient // Interface connect as client
    {
        void Connect(string ip, int port);
        void Write(string command);
        void Disconnect();
    }
}
