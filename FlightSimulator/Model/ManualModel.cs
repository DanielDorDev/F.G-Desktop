using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class ManualModel
    {
        private ITelnetClient server;
        public event PropertyChangedEventHandler PropertyChanged;

        public ManualModel()
        {
            server = ConnectToServer.Instance;
        }

        public double Rudder
        {
            get => this.Rudder;
            set
            {
                this.Rudder = value;
                NotifyPropertyChanged("flight_rudder");
            }
        }

        public double Throttle
        {
            get => this.Throttle;
            set
            {
                this.Throttle = value;
                NotifyPropertyChanged("engine_throttle");
            }
        }

        public double Aileron
        {
            get => this.Aileron;
            set
            {
                this.Aileron = value;
                NotifyPropertyChanged("flight_aileron");
            }
        }

        public double Elevator
        {
            get => this.Elevator;
            set
            {
                this.Elevator = value;
                NotifyPropertyChanged("flight_elevator");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void ChangeValue(string new_value)
        {
            this.server.Write(new_value);
        }
    }
}
