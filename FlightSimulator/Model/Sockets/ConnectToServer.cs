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
        TcpClient client = new TcpClient();

        #region Singleton
        private static Interface.ITelnetClient m_Instance = null;
        public static Interface.ITelnetClient Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ConnectToServer();
                }
                return m_Instance;
            }
        }
        #endregion

        public void Connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client.Connect(ep);
        }

        public void Disconnect()
        {
            client.Close();
        }

        public void Write(string command)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(command);
                writer.Flush();
            }
        }
    }
}