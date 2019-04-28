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
    class GetClient : BaseServer    // Server for accepting clients, baseServer interface.
    {
        IPEndPoint ep;

        // Server listener.
        TcpListener listener;

        // Volatile boolean for connection status.
        private volatile bool stop = true;

        public bool Stop { get => stop; set => stop = value; }

        // Get port by ip end point object.
        public override int Port => ep.Port;

        // Data buffer.
        private string _Data = string.Empty;
        public string Data
        {
            get => _Data;
            set
            {
                _Data = value;
                // If data changed, notify data recv.
                NotifyServerDataRecvEvent();
            }
        }

        // If property changed, event observable.
        public event PropertyChangedEventHandler PropertyChanged;

        // Create the server data, by port and listner object.
        public GetClient(int port)
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        // Connect  to server, open for clients.
        public override void Connect()
        {
            new Task(() =>
            {
                try
                {
                    Stop = false;
                    NotifyServerConnectedEvent();   // Set stop false, and connect notify, start listening for 1 client(flight gear).
                    listener.Start(1);
                    while (!Stop)
                    {
                        // Accept clien(or wait for one and check every 10HZ), handle client in another thread.
                        TcpClient client = listener.AcceptTcpClient();
                        new Task(() => HandleClient(client)).Start();
                        Thread.Sleep(1000);// read every 10HZ seconds.
                    }
                }
                catch (Exception)
                {
                    // Someting happened, disconnect.
                    Disconnect();
                }
            }).Start();
        }

        // Handle client, given tcp client object.
        private void HandleClient(TcpClient client)
        {
            TcpClient clientHandle = client;    // Save it.
            try
            {
                // Using the strem from the client, create reader and read lines while server connected.
                using (NetworkStream stream = clientHandle.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!Stop)
                    {
                        Data = reader.ReadLine();
                    }
                    Thread.Sleep(90); // Sleep little less then the client (beacause of the overload time).
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                // After process finshed , close client.
                clientHandle.Client.Close();
                clientHandle.Close();
            }
        }

        // Reconnect to server, disconnect first, and then connect again.
        public override void ReConnect(int port)
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Any, port);
            Connect();
        }

        // Disconnect from server, stop server opeartion, notify it.
        public override void Disconnect()
        {
            Stop = true;
            NotifyServerDisconnectedEvent();

            try // Close listener and clients.
            {
                if (listener != null)
                {
                    listener.Server.Close();
                    listener.Stop();
                }
            }
            catch (Exception) { };
        }

        // Read data, a socket for the outside interface.
        public override string Read() => Data;


        public void NotifyPropertyChanged(string propName)      // Notify property changed.
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
