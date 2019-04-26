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
    class ConnectToServer : Interface.ITelnetClient
    {

        IPEndPoint ep;
        TcpClient client;

        public string Ip { get => ep.Address.ToString(); }

        public int Port { get => ep.Port; }
        #region Singleton
        private static Interface.ITelnetClient m_Instance = null;
        public static Interface.ITelnetClient Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ConnectToServer(Settings.Default.FlightServerIP, Settings.Default.FlightCommandPort);
                }
                return m_Instance;
            }
        }
        #endregion

        public ConnectToServer(string ip, int port)
        {
            client = new TcpClient();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public bool IsConnected()
        {
            return client.Connected;
        }

        public void Connect()
        {
            try
            {
                if (!client.Connected)
                {
                    client = new TcpClient();
                    client.Connect(ep);
                }
            }
            catch (SocketException e)
            {

            }
        }

        public void ReConnect(string ip, int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect();
        }

        public void Disconnect()
        {
            if (client.Connected)
            {
                client.Close();
            }
        }

        public void Write(string command)
        {
            try
            {
                if (client.Connected)
                {
                    using (NetworkStream stream = client.GetStream())
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(command);
                        writer.Flush();
                    }
                }
            }
            catch (SocketException e)
            {

            }
        }
    }
}