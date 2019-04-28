using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IFlightModel : System.ComponentModel.INotifyPropertyChanged
    {
        void Connect(string ip, int port, int portCom);         // Connect interface for flight gear.
        void Disconnect();        // Disconnect server for flight gear.
        void DataUpdate();        // Opeartion when data updated.
        void Send(string msg);        // Send data to flight gear.
        double Lat { get; set; } // Will change to SQL database, if more binding needed.
        double Lon { get; set; }
        
    }
}
