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
    class GetClient : BaseServer        // Server for accepting clients, baseServer interface.
    {
        IPEndPoint ep;
        TcpListener listener;       // Server listener.
        private volatile bool stop = true;      // Volatile boolean for connection status.
        public bool Stop { get => stop; set => stop = value; }

        public override int Port { get => ep.Port; }    // Get port by ip end point object.

        private string _Data = string.Empty;        // Data buffer.
        public string Data
        {
            get => _Data;
            set
            {
                _Data = value;      // If data changed, notify data recv.
                NotifyServerDataRecvEvent();
            }
        }

    public event PropertyChangedEventHandler PropertyChanged;       // If property changed, event observable.

        public GetClient(int port)      // Create the server data, by port and listner object.
        {
            ep = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ep);
        }

        public override void Connect()      // Connect  to server, open for clients.
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
    
        private void HandleClient(TcpClient client)     // Handle client, given tcp client object.
        {
            TcpClient clientHandle = client;             // Save it.
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
                clientHandle.Client.Close();                 // After process finshed , close client.
                clientHandle.Close();
            }
        }

        public override void ReConnect(int port)    // Reconnect to server, disconnect first, and then connect again.
        {
            Disconnect();
            ep = new IPEndPoint(IPAddress.Any, port);
            Connect();
        }

        public override void Disconnect()   // Disconnect from server, stop server opeartion, notify it.
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

        public override string Read()   // Read data, a socket for the outside interface.
        {
            return Data;
        }


        public void NotifyPropertyChanged(string propName)      // Notify property changed.
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
