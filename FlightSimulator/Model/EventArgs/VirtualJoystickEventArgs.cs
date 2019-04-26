using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.EventArgs
{
    public class VirtualJoystickEventArgs
    {
        public string Aileron_Path { get; } = "/controls/flight/aileron";
        public string Elevator_Path { get; } = "/controls/flight/elevator";

        public double Aileron { get; set; }
        public double Elevator { get; set; }
    }
}
