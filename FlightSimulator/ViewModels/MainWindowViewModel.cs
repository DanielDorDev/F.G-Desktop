using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightSimulator.Model;
using FlightSimulator.Model.Sockets;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Properties;
using System.Windows;

namespace FlightSimulator.ViewModels
{
    class MainWindowViewModel : BaseNotify
    {
        /*
Properties.Settings.Default.FlightCommandPort;
Properties.Settings.Default.FlightServerIP;
Properties.Settings.Default.FlightInfoPort;
*/
        private IFlightModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            this.model = new FlightGearModel(new ConnectToServer(), new CreateServer());


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
            model.Connect(Settings.Default.FlightServerIP, Settings.Default.FlightInfoPort, Settings.Default.FlightCommandPort);
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


        #region ClosedCommand
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {

                return _closeCommand ?? (_closeCommand =
                new CommandHandler(() => CloseClick()));
            }
        }
        private void CloseClick()
        {
            model.Disconnect();
        }
        #endregion
        #endregion

    }
}
