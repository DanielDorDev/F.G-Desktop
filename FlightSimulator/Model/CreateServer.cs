using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace FlightSimulator.Model
{
    class CreateServer : Interface.ITelnetServer
    {

        IPEndPoint ep;
        TcpListener listener;
        TcpClient client;

        public void Connect(int port)
        {
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
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
            }        }
    }
}
