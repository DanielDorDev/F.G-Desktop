using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator.ViewModels
{
    class FlightGearViewModel : BaseNotify
    {
        /*
Properties.Settings.Default.FlightCommandPort;
Properties.Settings.Default.FlightServerIP;
Properties.Settings.Default.FlightInfoPort;
*/
        private IFlightModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public FlightGearViewModel(IFlightModel model)
        {

            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {

            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));


        }
        #region Commands
        #region ConnectCommand
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand =
                new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            //  VM_FlightGear = new FlightGearViewModel(new FlightSimulator.Model.FlightGearModel(new ConnectToServer(), new CreateServer()));
            //VM_FlightGear.Connect();

        }
        #endregion

        #region SettingCommand
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand ?? (_settingsCommand =
                new CommandHandler(() => SettingsClick()));
            }
        }
        private void SettingsClick()
        {

            SettingsWindowViewModel VM_settings = new SettingsWindowViewModel(new ApplicationSettingsModel());
            Views.Windows.SettingsWindow objSettings = new Views.Windows.SettingsWindow();
            objSettings.ShowDialog();
        }

        #endregion
        #endregion



    }
}
