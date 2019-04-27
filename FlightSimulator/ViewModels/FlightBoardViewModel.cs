using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        IFlightModel model;
        public FlightBoardViewModel()
        {
            this.model = FlightGearModel.Instance();

            this.model.PropertyChanged +=          
                delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public double Lon
        {
            get;
        }

        public double Lat
        { 
            get;
        }
    }
}
