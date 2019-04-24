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

        ManualViewModel(ManualModel model)
        {
            this.model = model;
        }

        public double VM_Throttle
        {
            set => this.model.ChangeValue($"set /controls/engines/current-engine/throttle {value.ToString()}");
            get => this.model.Throttle;
        }

        public double VM_Rudder
        {
            set => this.model.ChangeValue($"set /controls/flight/rudder {value.ToString()}");
            get => this.model.Throttle;
        }

        public void Joystick_Move (Views.Joystick sender, Model.EventArgs.VirtualJoystickEventArgs eventArgs)
        {
            this.model.ChangeValue($"set /controls/flight/aileron {eventArgs.Aileron.ToString()}");
            this.model.ChangeValue($"set /controls/flight/elevator {eventArgs.Elevator.ToString()}");
        }
    }
}
