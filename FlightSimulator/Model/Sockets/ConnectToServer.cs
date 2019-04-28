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

        // Get ip from IP object.
        public override string Ip { get => ep.Address.ToString(); }

        // Get port from port object.
        public override int Port { get => ep.Port; }

        // Construct client ip end point.
        public ConnectToServer(string ip, int port) => ep = new IPEndPoint(IPAddress.Parse(ip), port);

        // Connect to client, create task and loop.
        public override void Connect()
        {
            new Task(() =>
            {
                // Create tcp client.
                client = new TcpClient();
                try
                {
                    client.Connect(ep);     // Connect to client.
                    this.NotifyClientConnectedEvent();      // Notify connection.

                    // disconnect set client to null(free socket).
                    while (client != null)
                    {
                        // read every 10HZ seconds.
                        Thread.Sleep(100);
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

        // Disconnect from the client.
        public override void Disconnect()
        {
            // Notify disconnection.
            NotifyClientDisconnectedEvent();

            // Close if client object exist.
            if (this.client != null)
            {
                client.Client.Close();      // Close socket and then client.
                client.Close();
                this.client = null;     // Insurance that client closed.

            }

        }

        // Write to client msg.
        public override void Write(string command)
        {
            try
            {
                // Send data to server, first encode it.
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(command + "\r\n");
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