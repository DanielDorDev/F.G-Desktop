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
    class FlightGearModel : IFlightModel
    {
        #region Singleton
        private static Interface.IFlightModel m_Instance = null;
        public static Interface.IFlightModel Instance()
        {

            if (m_Instance == null)
            {
                m_Instance = new FlightGearModel(new GetClient(Properties.Settings.Default.FlightInfoPort),
                    new ConnectToServer(Properties.Settings.Default.FlightServerIP, Properties.Settings.Default.FlightCommandPort));
            }
            
            return m_Instance;
        }
        #endregion

        private BaseClient telnetClient;
        private BaseServer telnetServer;
        const int generic_Count = 25;

        volatile Boolean _StopServer;
        public Boolean StopServer
        {
            get
            {
                return _StopServer;
            }
            set
            {

                _StopServer = value;
                ////////////////////////
                ///}
            }
        }

        volatile Boolean _StopClient;
        public Boolean StopClient
        {
            get
            {
                return _StopClient;
            }
            set
            {
                _StopClient = value;
                ////////////////
            }
        }
        private double _Lon;
        public double Lon
        {
            get {
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
                    NotifyPropertyChanged("Lat");
                }
            }
        }

        public FlightGearModel(BaseServer setServer, BaseClient setClient)
        {
            try
            {
                this.telnetServer = setServer;
                this.telnetServer.NotifyDataRecv += DataUpdate;
                this.telnetServer.NotifyDisconnected += delegate () { this.StopServer = true; };
                this.telnetServer.NotifyConnected += delegate () { this.StopServer = false; };
                telnetServer.Connect();
            }
            catch (Exception e)
            {
                this.StopServer = true;

            }
            try
            {
                this.telnetClient = setClient;
                this.telnetClient.NotifyDisconnected += delegate () { this.StopClient = true; };
                this.telnetClient.NotifyConnected += delegate () { this.StopClient = false; };
                this.telnetClient.Connect();
            }
            catch (Exception e)
            {
                this.StopClient = true;
            }
        }

        public void DataUpdate()
        {
            try
            {
                string data = telnetServer.Read();
                string[] result = data.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (string dataSplit in result)
                {
                    double[] fieldChange = Array.ConvertAll(dataSplit.Split(','), Double.Parse);
                    if (fieldChange.Length == generic_Count)
                    {
                        Lon = fieldChange[0];
                        Lat = fieldChange[1];
                    }

                }
            }
            catch (NullReferenceException e) { };
        }

        public void Connect(string txtIP, int txtPort, int txtCommandPort)
        {
            try
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
            catch (Exception e) { };

            try
            {
                if (txtCommandPort != telnetClient.Port || txtIP != telnetClient.Ip)
                {
                    telnetClient.ReConnect(txtIP, txtCommandPort);
                }
                else if (this.StopClient)
                {
                    telnetClient.Connect();
                }
            } catch (Exception e) { };
        }

        public void Disconnect()
        {
            StopServer = true;
            telnetServer.Disconnect();
            StopClient = true;
            telnetClient.Disconnect();
        }

        public void Send(string msg)
        {
            telnetClient.Write(msg);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool IsConnected()
        {
            return !this.StopClient;
        }
    }
}


/*
public double longitude_deg { get; set; }
public double latitude_deg { get; set; }
public double airspeed_indicator_indicated_speed_ktg { get; set; }
public double altimeter_indicated_altitude_ft { get; set; }
public double altimeter_pressure_alt_ft { get; set; }
public double attitude_indicator_indicated_pitch_deg { get; set; }
public double attitude_indicator_indicated_roll_deg { get; set; }
public double attitude_indicator_internal_pitch_deg { get; set; }
public double attitude_indicator_internal_roll_deg { get; set; }
public double encoder_indicated_altitude_ft { get; set; }
public double encoder_pressure_alt_ft { get; set; }
public double gps_indicated_altitude_ft { get; set; }
public double gps_indicated_ground_speed_kt { get; set; }
public double gps_indicated_vertical_speed { get; set; }
public double indicated_heading_deg { get; set; }
public double magnetic_compass_indicated_heading_deg { get; set; }
public double slip_skid_ball_indicated_slip_skid { get; set; }
public double turn_indicator_indicated_turn_rate { get; set; }
public double vertical_speed_indicator_indicated_speed_fpm { get; set; }
public double flight_aileron { get; set; }
public double flight_elevator { get; set; }
public double flight_rudder { get; set; }
public double flight_flaps { get; set; }
public double engine_throttle { get; set; }
public double engine_rpm { get; set; }
FieldInfo[] myFieldInfo = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
Array fieldChange = Array.ConvertAll(telnetServer.Read().Split(','), Double.Parse);
int i = 0;
foreach (double change in fieldChange)
{
myFieldInfo.SetValue(change, i);
i++;
}
*/