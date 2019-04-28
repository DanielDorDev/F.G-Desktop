using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ITelnetServer
    {
        int Port { get;} // Return port number.
        void Connect(); // Connect to server and listen to client.
        void ReConnect(int port); // Reopen server by ip.
        string Read(); // Read data that server got.
        void Disconnect();  // Disconnect from the server.
    }
}
