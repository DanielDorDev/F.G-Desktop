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
        private ITelnetClient server;

        public ManualModel() => server = ConnectToServer.Instance;

        public double Rudder
        {
            get => this.Rudder;
            set
            {
                this.Rudder = value;
                ChangeValue("/controls/flight/rudder", value);
            }
        }

        public double Throttle
        {
            get => this.Throttle;
            set
            {
                this.Throttle = value;
                ChangeValue("/controls/engines/current-engine/throttle", value);
            }
        }

        public void ChangeValue(string path, double new_value) => server.Write("set " + path + " " + new_value.ToString() + "\r\n");
    }
}
