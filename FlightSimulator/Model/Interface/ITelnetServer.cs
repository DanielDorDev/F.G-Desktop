using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ITelnetServer
    {
        void Connect(int port);
        string Read();
        void Disconnect();
    }
}
