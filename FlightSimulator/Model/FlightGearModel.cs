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
    class FlightGearModel : Interface.IFlightModel
    {
        #region Singleton
        private static Interface.IFlightModel m_Instance = null;
        public static Interface.IFlightModel Instance()
        {

            if (m_Instance == null)
            {
                m_Instance = new FlightGearModel(CreateServer.Instance, ConnectToServer.Instance);
            }
            return m_Instance;
        }
        #endregion

        private BaseClient telnetClient;
        private BaseServer telnetServer;

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
            this.telnetServer = setServer;
            this.telnetServer.NotifyDataRecv += DataUpdate;
            this.telnetServer.NotifyDisconnected +=
                delegate () { this.StopServer = false;};
            this.telnetServer.NotifyConnected +=
                delegate () { this.StopServer = true; };

            this.telnetServer.Connect();

            this.telnetClient = setClient;
            this.telnetClient.NotifyDisconnected +=
                delegate () { this.StopServer = false; };
            this.telnetClient.NotifyConnected +=
                delegate () { this.StopServer = true; };
            this.telnetClient.Connect();
        }

        public void DataUpdate()
        {
            try
            {
                double[] fieldChange = Array.ConvertAll(telnetServer.Read().Split(','), Double.Parse);
                //   file.Wrditse(fieldCshange);
                Lon = fieldChange[0];
                Lat = fieldChange[1];

            }
            catch (NullReferenceException e)
            {

            }

        }



        public void Connect(string txtIP, int txtPort, int txtCommandPort)
        {


            if (txtPort != telnetServer.Port)
            {
                telnetServer.ReConnect(txtPort);
            }
            else if (!telnetServer.IsConnected())
            {
                telnetServer.Connect();
            }

            if (txtCommandPort != telnetClient.Port || txtIP != telnetClient.Ip)
            {
                telnetClient.ReConnect(txtIP, txtCommandPort);
            }
            else if (!telnetClient.IsConnected())
            {
                telnetClient.Connect();

            }
        }

        public void Disconnect()
        {
            if (StopServer == false)
            {
                StopServer = true;
                telnetServer.Disconnect();
            }
            if (StopClient == false)
            {
                StopClient = true;
                telnetClient.Disconnect();
            }
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