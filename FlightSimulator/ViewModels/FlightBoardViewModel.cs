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
        IFlightModel model; // Contain instance of model FG.

        public FlightBoardViewModel()
        {
            this.model = FlightGearModel.Instance();

            // Register as listener to notify property changes.
            this.model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        // Flight board parameters.
        public double Lon => model.Lon;

        // Flight board parameters.
        public double Lat => model.Lat;
    }
}
