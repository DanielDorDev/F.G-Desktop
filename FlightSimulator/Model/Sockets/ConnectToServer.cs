using FlightSimulator.Model.Interface;
using FlightSimulator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Sockets
{
    class ConnectToServer : BaseClient 
    {

        IPEndPoint ep;
        TcpClient client;
        public override string Ip { get => ep.Address.ToString(); } // Get ip from IP object.

        public override int Port { get => ep.Port; } // Get port from port object.

        public ConnectToServer(string ip, int port) // Construct client ip end point.
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public override void Connect() // Connect to client, create task and loop.
        {
            new Task(() =>
            {
                // Create tcp client.
                client = new TcpClient();
                try
                {
                    client.Connect(ep);     // Connect to client.
                    this.NotifyClientConnectedEvent();      // Notify connection.

                while (client != null)      // disconnect set client to null(free socket).
                    {
                        Thread.Sleep(100);// read every 10HZ seconds.
                    }
                }
                catch (Exception)
                {
                    Disconnect();   // Disconnect notify and disconnect operation.
                }
            }).Start();
        }

        public override void ReConnect(string ip, int port)
        {
            // Disconnect , create new IP end point, create connection again.
            Disconnect();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect();
        }

        public override void Disconnect()       // Disconnect from the client.
        {
            NotifyClientDisconnectedEvent();    // Notify disconnection.
            if (this.client != null)        // Close if client object exist.
            {
                client.Client.Close();      // Close socket and then client.
                client.Close();
                this.client = null;     // Insurance that client closed.

            }

        }
        public override void Write(string command)          // Write to client msg.
        {
            try
            {
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(command+ "\r\n");        // Send data to server, first encode it.
                client.GetStream().Write(myWriteBuffer, 0, myWriteBuffer.Length);
            }
            catch (Exception)
            {
                // If exepction happened, problem with client.
                NotifyClientDisconnectedEvent();
            }
        }
    }
}