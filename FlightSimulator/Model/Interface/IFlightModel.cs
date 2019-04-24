using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IFlightModel : System.ComponentModel.INotifyPropertyChanged
    {
        // connection to the robot
        void Connect(string ip, int port, int portCom);
        void Disconnect();

        // activate actuators
        void Send(string msg);
        
    }
}
