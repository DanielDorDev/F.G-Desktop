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
        private IFlightModel model;

        public MainWindowViewModel()
        {
            this.model = FlightGearModel.Instance();

            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        #region Commands
        #region ConnectCommand
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                // Connection handler, get on click command and use it.
                return _connectCommand ?? (_connectCommand =
                new CommandHandler(() => OnClick()));
            }
        }

        private void OnClick()
        {
            // Send connect command to model.
            model.Connect(Settings.Default.FlightServerIP, Settings.Default.FlightInfoPort, Settings.Default.FlightCommandPort);
        }
        #endregion

        #region SettingCommand
        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                // Create settings command and use it.
                return _settingsCommand ?? (_settingsCommand =
                new CommandHandler(() => SettingsClick()));
            }
        }
        private void SettingsClick()
        {
            // Open settings window for the user.
            SettingsWindowViewModel VM_settings = new SettingsWindowViewModel(new ApplicationSettingsModel());
            Views.Windows.SettingsWindow objSettings = new Views.Windows.SettingsWindow();
            objSettings.ShowDialog();
        }

        #endregion
        #endregion
    }
}
