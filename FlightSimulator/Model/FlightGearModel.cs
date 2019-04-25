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

namespace FlightSimulator.Model
{
    class FlightGearModel : Interface.IFlightModel
    {
        #region Singleton
        private static Interface.IFlightModel m_Instance = null;
        public static Interface.IFlightModel Instance(ITelnetClient setClient, ITelnetServer setServer)
        {

                if (m_Instance == null)
                {
                    m_Instance = new FlightGearModel(setClient, setServer);
                }
                return m_Instance;
        }
        #endregion

        private ITelnetClient telnetClient;
        private ITelnetServer telnetServer;
        volatile Boolean stop;


        private double _Lon;
        public double Lon
        {
            get {
            return _Lon; 
            }
            set
            {
                _Lon = value;
                NotifyPropertyChanged("longitude_deg");
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
                _Lat = value;
                NotifyPropertyChanged("latitude_deg");
            }
        }


        public FlightGearModel(ITelnetClient setClient, ITelnetServer setServer)
        {
            this.telnetClient = setClient;
            this.telnetServer = setServer;
            stop = false;
        }

        public void Connect(string txtIP, int txtPort, int txtCommandPort)
        {
            telnetClient.Connect(txtIP, txtPort);

            new Thread(delegate () {
                telnetServer.Connect(txtCommandPort);

                while (!stop)
                {
                double[] data = Array.ConvertAll(telnetServer.Read().Split(','), Double.Parse);
                lock (this)
                    {
                        Lon = data[0];
                        Lat = data[1];
                    }
                    Thread.Sleep(250);// read every 4HZ seconds.
                }
            }).Start();
        }

        public void Disconnect()
        {
            if(stop == false)
            {
                stop = true;
                telnetClient.Disconnect();
                telnetServer.Disconnect();
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
*/








/*
                        FieldInfo[] myFieldInfo = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
                    Array fieldChange = Array.ConvertAll(telnetServer.Read().Split(','), Double.Parse);
                    int i = 0;
                    foreach (double change in fieldChange)
                    {
                        myFieldInfo.SetValue(change, i);
                        i++;
                    }
*/