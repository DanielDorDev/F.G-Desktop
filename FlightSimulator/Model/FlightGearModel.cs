using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;
using FlightSimulator.Model.Sockets;

namespace FlightSimulator.Model
{
    /// <summary>
    /// Flight gear main model.
    /// Interface based object, using interface for flight model.
    /// server and client interface usage.
    /// Data recv with no bind to what porp.
    /// A base structure for notify server and clients status.
    /// Notify connection, disconnection by type.
    /// Elements are not solid, but simple way would be include SQL database for update and getting data.
    /// Because only one type of flightgear instructed, i used a more dierct and bind info.
    /// </summary>
    class FlightGearModel : IFlightModel
    {
        #region Singleton 
        private static Interface.IFlightModel m_Instance = null;   // Only one instance for Flight gear model handle.
        public static Interface.IFlightModel Instance()
        {

            if (m_Instance == null)
            {   // Create by confg file in project.
                m_Instance = new FlightGearModel(new GetClient(Properties.Settings.Default.FlightInfoPort),
                    new ConnectToServer(Properties.Settings.Default.FlightServerIP, Properties.Settings.Default.FlightCommandPort));
            }

            return m_Instance;
        }
        #endregion

        private BaseClient telnetClient;    // Client interface object.
        private BaseServer telnetServer;    // Server interface object.
        const int generic_Count = 25;       // XML data for server.

        volatile Boolean _StopServer;      // Boolean for server status.
        public Boolean StopServer
        {
            get => _StopServer;
            set => _StopServer = value;
        }

        volatile Boolean _StopClient;   // Boolean for client status.
        public Boolean StopClient
        {
            get => _StopClient;
            set => _StopClient = value;
        }

        // There are only two paramters, not need for complex databases(for this ex).
        #region Data Server 

        private double _Lon;
        public double Lon
        {
            get
            {
                return _Lon;
            }
            set
            {
                if (_Lon != value)
                {
                    _Lon = value;
                    NotifyPropertyChanged("Lon");
                }
            }
        }
        private double _Lat;
        public double Lat
        {
            get
            {
                return _Lat;
            }
            set
            {
                if (_Lat != value)
                {
                    _Lat = value;
                }
            }
        }
        #endregion

        public FlightGearModel(BaseServer setServer, BaseClient setClient) // Construct.
        {
            try
            {
                // Set server, register observers and connect.
                this.telnetServer = setServer;
                this.telnetServer.NotifyDataRecv += DataUpdate;
                this.telnetServer.NotifyDisconnected += delegate () { this.StopServer = true; };
                this.telnetServer.NotifyConnected += delegate () { this.StopServer = false; };
                telnetServer.Connect();
            }

            catch (Exception)
            {
                // If someting happened.
                this.StopServer = true;
            }

            try
            {
                // Set client, register observers and connect.
                this.telnetClient = setClient;
                this.telnetClient.NotifyDisconnected += delegate () { this.StopClient = true; };
                this.telnetClient.NotifyConnected += delegate () { this.StopClient = false; };
                this.telnetClient.Connect();
            }

            catch (Exception)
            {
                // If someting happened.
                this.StopClient = true;
            }
        }

        // Opeartion when data updated and object got notify.
        public void DataUpdate()
        {
            try
            {
                double[] fieldChange = Array.ConvertAll(telnetServer.Read().Split(','), Double.Parse);
                if (fieldChange[0] != Lon | fieldChange[1] != Lat)
                {
                    Lon = fieldChange[0];
                    Lat = fieldChange[1];
                    NotifyPropertyChanged("Lat");
                }
            }
            catch (NullReferenceException) { };
        }

        // Connect to both server and client (as asked).
        public void Connect(string txtIP, int txtPort, int txtCommandPort)
        {
            try // Check if reconnect needed, if not check if server stoped, and new connection needed.
            {
                if (txtPort != telnetServer.Port)
                {
                    telnetServer.ReConnect(txtPort);
                }
                else if (this.StopServer)
                {
                    new Task(() => telnetServer.Connect()).Start();
                }
            }
            catch (Exception) { };

            try
            {   // Check if reconnect needed, if not check if client stoped, and new connection needed.
                if (txtCommandPort != telnetClient.Port || txtIP != telnetClient.Ip)
                {
                    telnetClient.ReConnect(txtIP, txtCommandPort);
                }
                else if (this.StopClient)
                {
                    telnetClient.Connect();
                }
            }
            catch (Exception) { };
        }

        // Disconnect both from server and client(set status of servers and operate).
        public void Disconnect()
        {
            StopServer = true;
            telnetServer.Disconnect();
            StopClient = true;
            telnetClient.Disconnect();
        }

        // Send data by client connection.
        public void Send(string msg) => telnetClient.Write(msg);

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
