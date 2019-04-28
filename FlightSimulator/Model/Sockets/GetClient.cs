using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;
using FlightSimulator.Properties;

namespace FlightSimulator.Model.Sockets
{
    class GetClient : BaseServer
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

        public GetClient(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        public override void Connect()
        {
            if (client == null || !client.Client.Connected)
            {
                stop = false;
                new Task(() =>
                {
                    try
                    {
                        listener.Start();
                        NotifyServerConnectedEvent();

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
                    }
                    catch (Exception e)
                    {
                        NotifyServerDisconnectedEvent();
                    }
                    finally
                    {
                        Disconnect();
                    }
                }).Start();
            }
        }

        public override void ReConnect(int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Any, port);
            Connect();
        }

        public override void Disconnect()
        {
            stop = true;
            NotifyServerDisconnectedEvent();
            try
            {
                if (client != null)
                {
                    client.GetStream().Close();
                    client.Close();
                    client = null;
                }
            }
            catch (Exception e) { };

            try
            {
                    if (listener != null)
                {
                    listener.Server.Close();
                    listener.Stop();
                }
            }
            catch (Exception e) { };
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
