﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IFlightModel : System.ComponentModel.INotifyPropertyChanged
    {
        void Connect(string ip, int port, int portCom);
        void Disconnect();
        void DataUpdate();
        bool IsConnected();
        void Send(string msg);
        double Lat { get; set; } // Will change to SQL database.
        double Lon { get; set; }
        
    }
}
