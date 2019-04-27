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
        private IFlightModel server;

        public ManualModel() => server = FlightGearModel.Instance;

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

        public void ChangeValue(string path, double new_value) => server.Send("set " + path + " " + new_value.ToString());
    }
}
