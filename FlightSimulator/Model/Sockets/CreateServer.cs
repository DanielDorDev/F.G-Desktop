using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using FlightSimulator.Model.Interface;
using FlightSimulator.Properties;

namespace FlightSimulator.Model.Sockets
{
    class CreateServer : BaseServer
    {
        IPEndPoint ep;
        TcpListener listener;
        TcpClient client;
        volatile bool stop = true;
        public bool Stop { get => stop; set => stop = value; }

        public override int Port { get => ep.Port; }

        private string _Data = string.Empty;
        public string Data { get => _Data; set => _Data = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CreateServer(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        public override void Connect()
        {
            if (!IsConnected())
            {
            stop = false;
            NotifyServerConnectedEvent();
            listener.Start();
            new Thread(delegate () {
                client = listener.AcceptTcpClient();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!stop)
                    {

                        Data = reader.ReadLine();
                        NotifyServerDataRecvEvent();

                    }
                    Thread.Sleep(100);// read every 10HZ seconds.
                }
            }).Start();
            }
        }


        public override void ReConnect(int port)
        {
            NotifyServerDisconnectedEvent();
            Disconnect();
            ep = new IPEndPoint(IPAddress.Any, port);
            Connect();
            NotifyServerConnectedEvent();
        }

        public override void Disconnect()
        {
            stop = true;
            NotifyServerDisconnectedEvent();

            if (client != null && client.Connected)
            {
                client.Close();
            }
           
            if(listener != null)
            {
                listener.Stop();
            }
        }
        public override bool IsConnected()
        {
            try
            {
                if (stop)
                {
                    return false;
                }
                return this.listener.Pending();

            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }

        public override string Read()
        {
            return Data;
        }


        public void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
