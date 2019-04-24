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
       // public void NotifyPropertyChanged(string propName) {...}
        public double VM_Lon
        {
            get
            {
                return VM_Lon;
            }
            set
            {
                Lon = value;
                NotifyPropertyChanged("longitude_deg");
            }
        }
        public double VM_Lat
        {
            get
            {
                return VM_Lat;
            }
            set
            {
                Lat = value;
                
            }
        }

    }
}
