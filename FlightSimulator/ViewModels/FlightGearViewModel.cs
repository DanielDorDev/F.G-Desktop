using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.ViewModels
{
    class FlightGearViewModel : BaseNotify
    {

        private IFlightModel model;

        public FlightGearViewModel(IFlightModel model)
        {
            this.model = model;
            //model.PropertyChanged+=...
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) {...}

        public double VM_value(string key)
        {
            return model[key];
        }



    }
}
