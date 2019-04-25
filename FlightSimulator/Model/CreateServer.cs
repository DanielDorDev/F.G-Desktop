using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace FlightSimulator.Model.Sockets
{
    class CreateServer : Interface.ITelnetServer
    {

        IPEndPoint ep;
        TcpListener listener;
        TcpClient client;

        #region Singleton
        private static Interface.ITelnetServer m_Instance = null;
        public static Interface.ITelnetServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CreateServer();
                }
                return m_Instance;
            }
        }
        #endregion


        public void Connect(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
            listener.Start();
            client = listener.AcceptTcpClient();
        }

        public void Disconnect()
        {
            client.Close();
            listener.Stop();
        }

        public string Read()
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                return reader.ReadString();
            }
        }
    }
}
