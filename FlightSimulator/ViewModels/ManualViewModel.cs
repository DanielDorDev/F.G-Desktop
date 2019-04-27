using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    class ManualViewModel : BaseNotify
    {
        private ManualModel model;

        public ManualViewModel() => this.model = new ManualModel();

        public double VM_Throttle
        {
            set
            {
                this.model.Throttle = value;
                NotifyPropertyChanged("VM_Throttle");
            }

            get => this.model.Throttle;
        }

        public double VM_Rudder
        {
            set
            {
                this.model.Rudder = value;
                NotifyPropertyChanged("VM_Rudder");
            }

            get => this.model.Rudder;
        }

        public void Joystick_Move (Views.Joystick sender, Model.EventArgs.VirtualJoystickEventArgs eventArgs)
        {
            this.model.ChangeValue(eventArgs.Aileron_Path, eventArgs.Aileron);
            this.model.ChangeValue(eventArgs.Elevator_Path, eventArgs.Elevator);
        }
    }
}
