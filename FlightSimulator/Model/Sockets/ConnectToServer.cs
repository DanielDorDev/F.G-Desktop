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
            client = new TcpClient();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public override bool IsConnected()
        {
            return client.Connected;
        }

        public override void Connect()
        {
            try
            {
                if (!client.Connected)
                {
                    client.Connect(ep);
                    this.NotifyClientConnectedEvent();
                        new Thread(delegate () {
                        while (client.Connected)
                        {
                            Thread.Sleep(250);// read every 4HZ seconds.
                        }
                   this.NotifyClientDisconnectedEvent();

                        }).Start();
                }
            }
            catch (SocketException e)
            {

            }
        }

        public override void ReConnect(string ip, int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect();
        }

        public override void Disconnect()
        {
            this.NotifyClientDisconnectedEvent();
            if (client.Connected)
            {
                client.Close();
            }
        }

        public override void Write(string command)
        {
            try
            {
                if (client.Connected)
                {

                    {
                        byte[] myWriteBuffer = Encoding.ASCII.GetBytes(command);
                        client.GetStream().Write(myWriteBuffer, 0, myWriteBuffer.Length);
                        client.GetStream().Flush();
                    }
                }
            }
            catch (SocketException e)
            {
            }
        }
    }
}