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
        private volatile bool stop = true;
        public bool Stop { get => stop; set => stop = value; }

        public override int Port { get => ep.Port; }

        private string _Data = string.Empty;
        public string Data
        {
            get => _Data;
            set
            {
                _Data = value;
                NotifyServerDataRecvEvent();
            }
        }

    public event PropertyChangedEventHandler PropertyChanged;

        public GetClient(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        public override void Connect()
        {
            new Task(() =>
            {
                try
                {
                    Stop = false;
                    NotifyServerConnectedEvent();
                    listener.Start(1);
                    while (!Stop)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        new Task(() => HandleClient(client)).Start();
                        Thread.Sleep(1000);// read every 10HZ seconds.
                    }
                }
                catch (Exception e)
                {
                    Disconnect();
                }
                finally
                {
                    NotifyServerDisconnectedEvent();
                }
            }).Start();
        }
    
        private void HandleClient(TcpClient client)
        {
            TcpClient clientHandle = client;
            try
            {
                using (NetworkStream stream = clientHandle.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!Stop)
                    {
                        Data = reader.ReadLine();
                    }
                    Thread.Sleep(100);// read every 10HZ seconds.
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                clientHandle.Client.Close();
                clientHandle.Close();
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
            Stop = true;
            NotifyServerDisconnectedEvent();

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
