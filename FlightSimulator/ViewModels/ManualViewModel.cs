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

        // Constructor
        public ManualViewModel() => this.model = new ManualModel();

        // Matched property to the Throttle property which in the ManualModel.
        public double VM_Throttle
        {
            set => this.model.Throttle = value;
        }

        // Matched property to the Rudder property which in the ManualModel.
        public double VM_Rudder
        {
            set => this.model.Rudder = value;
        }

        // Method which registered to the Moved event in the Joystick's code behind and gets notifies each time the Joystick moved.
        public void Joystick_Move (Views.Joystick sender, Model.EventArgs.VirtualJoystickEventArgs eventArgs)
        {
            // Updating the model about the change in the Aileron and Elevator values.
            this.model.ChangeValue(eventArgs.Aileron_Path, eventArgs.Aileron);
            this.model.ChangeValue(eventArgs.Elevator_Path, eventArgs.Elevator);
        }
    }
}
