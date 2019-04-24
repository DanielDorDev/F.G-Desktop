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



        private ITelnetClient telnetClient;
        private ITelnetServer telnetServer;
        volatile Boolean stop;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Lon
        {
            get {
            return Lon; 
            }
            set
            {
                Lon = value;
                NotifyPropertyChanged("longitude_deg");
            }
        }
        public double Lat
        {
            get
            {
                return Lon;
            }
            set
            {
                Lon = value;
                NotifyPropertyChanged("longitude_deg");
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
            telnetServer.Connect(txtCommandPort);

            new Thread(delegate () {

                while (!stop)
                {
                    lock (this)
                    {
                  //  telnetServer.Read().Split(','), Double.Parse);
        }
                    Thread.Sleep(250);// read every 4HZ seconds.
                }
            }).Start();
        }

        public void Disconnect()
        {
            stop = true;
            telnetClient.Disconnect();
            telnetServer.Disconnect();
        }

        public void Send(string msg)
        {
            telnetClient.Write(msg);
        }

        public void NotifyPropertyChanged(string propName)
        {
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


myOrderedDictionary.Add("airspeed_indicator_indicated_speed_ktg", 0);
        myOrderedDictionary.Add("altimeter_indicated_altitude_ft", 0);
        myOrderedDictionary.Add("altimeter_pressure_alt_ft", 0);
        myOrderedDictionary.Add("attitude_indicator_indicated_pitch_deg", 0);
        myOrderedDictionary.Add("attitude_indicator_indicated_roll_deg", 0);
        myOrderedDictionary.Add("attitude_indicator_internal_roll_deg", 0);
        myOrderedDictionary.Add("encoder_indicated_altitude_ft", 0);
        myOrderedDictionary.Add("gps_indicated_altitude_ft", 0);
        myOrderedDictionary.Add("gps_indicated_ground_speed_kt", 0);
        myOrderedDictionary.Add("gps_indicated_vertical_speed", 0);
        myOrderedDictionary.Add("gps_indicated_vertical_speed", 0);
        myOrderedDictionary.Add("indicated_heading_deg", 0);
        myOrderedDictionary.Add("magnetic_compass_indicated_heading_deg", 0);
        myOrderedDictionary.Add("slip_skid_ball_indicated_slip_skid", 0);
        myOrderedDictionary.Add("turn_indicator_indicated_turn_rate", 0);
        myOrderedDictionary.Add("vertical_speed_indicator_indicated_speed_fpm", 0);
        myOrderedDictionary.Add("flight_aileron", 0);
        myOrderedDictionary.Add("flight_elevator", 0);
        myOrderedDictionary.Add("flight_rudder", 0);
        myOrderedDictionary.Add("flight_flaps", 0);
        myOrderedDictionary.Add("engine_throttle", 0);
        myOrderedDictionary.Add("engine_rpm", 0);
        */
