using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Sockets;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        #region Singleton
        private static ManualModel m_Instance = null;
        public static ManualModel Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ManualModel();
                }
                return m_Instance;
            }
        }
        #endregion

        private IFlightModel server;

        // Constructor
        public ManualModel() => server = FlightGearModel.Instance();

        // The Rudder property which is incharge of updating the server on any change with the Rudder's value.
        private double _rudder;
        public double Rudder
        {
            get => this._rudder;
            set
            {
                this._rudder = value;
                ChangeValue("/controls/flight/rudder", value);
            }
        }

        // The Throttle property which is incharge of updating the server on any change with the Throttle's value.
        private double _throttle;
        public double Throttle
        {
            get => this._throttle;
            set
            {
                this._throttle = value;
                ChangeValue("/controls/engines/current-engine/throttle", value);
            }
        }

        // The method in which we write the new value to the server.
        public void ChangeValue(string path, double new_value) => server.Send("set " + path + " " + new_value.ToString());
    }
}
