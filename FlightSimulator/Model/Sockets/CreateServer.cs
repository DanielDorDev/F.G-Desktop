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
    class CreateServer : Interface.ITelnetServer
    {
        IPEndPoint ep;
        TcpListener listener;
        TcpClient client;
        volatile bool stop = true;
        public bool Stop { get => stop; set => stop = value; }

        public int Port { get => ep.Port; }

        volatile private string _Data = string.Empty;
        public string Data { get => _Data; set => _Data = value; }

        #region Singleton
        private static Interface.ITelnetServer m_Instance = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public static Interface.ITelnetServer Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new CreateServer(Settings.Default.FlightInfoPort);
                }
                return m_Instance;
            }
        }
        #endregion

        public CreateServer(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        public void Connect()
        {
            if (!IsConnected())
            {
            stop = false;
            listener.Start();
            new Thread(delegate () {
                client = listener.AcceptTcpClient();
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!stop)
                    {
                        lock (Data)
                        {
                            Data = reader.ReadLine();
                            NotifyPropertyChanged("DataHasChanged");
                        }
                    }
                    Thread.Sleep(100);// read every 10HZ seconds.
                }
            }).Start();
            }
        }


        public void ReConnect(int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Any, port);
            Connect();

        }
        public void Disconnect()
        {
            stop = true;
            if (client != null && client.Connected)
            {
                client.Close();
            }
           
            if(listener != null)
            {
                listener.Stop();
            }
        }
        public bool IsConnected()
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

        public string Read()
        {
            lock (Data)
            {
                return Data;
            }
        }


        public void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
