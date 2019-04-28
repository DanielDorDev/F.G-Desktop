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
        public override string Ip { get => ep.Address.ToString(); }

        public override int Port { get => ep.Port; }

        public ConnectToServer(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

    
        public override void Connect()
        {
            new Task(() =>
            {
                client = new TcpClient();
                try
                {
                    client.Connect(ep);
                    this.NotifyClientConnectedEvent();

                while (client != null)
                    {
                        Thread.Sleep(100);// read every 10HZ seconds.
                    }
                }
                catch (Exception e)
                {
                    Disconnect();
                    NotifyClientDisconnectedEvent();
                }
            }).Start();

        }



        public override void ReConnect(string ip, int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect();
        }

        public override void Disconnect()
        {
            NotifyClientDisconnectedEvent();
            if (this.client != null)
            {
                client.Client.Close();
                client.Close();
                this.client = null;

            }

        }

        public override void Write(string command)
        {
            try
            {
                byte[] myWriteBuffer = Encoding.ASCII.GetBytes(command);
                client.GetStream().Write(myWriteBuffer, 0, myWriteBuffer.Length);
            }
            catch (Exception e)
            {
                NotifyClientDisconnectedEvent();
            }
        }
    }
}