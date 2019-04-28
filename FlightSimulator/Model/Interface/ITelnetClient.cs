using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface ITelnetClient // Interface connect as client
    {
        string Ip { get;} // Return ip adress.
        int Port { get;} // Return port number.
        void Connect(); // Connect to client.
        void ReConnect(string ip, int port); // Reconnect to ip and port given.
        void Write(string command); // Write to client msg.
        void Disconnect(); // Disconnect from client.
    }
}
