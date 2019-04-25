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

       // ManualViewModel(ManualModel model)
      //  {
       //     this.model = model;
       // }

        public double VM_Throttle
        {
            set;
            get;
        }

        public double VM_Rudder
        {
            set;
            get;
        }

        public void Joystick_Move (Views.Joystick sender, Model.EventArgs.VirtualJoystickEventArgs eventArgs)
        {

        }
    }
}
