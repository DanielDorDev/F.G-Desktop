using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{
    public class SettingsWindowViewModel : BaseNotify
    {
        private ISettingsModel model;   // Settings model field.

        // Construct vm.
        public SettingsWindowViewModel(ISettingsModel model)
        {
            this.model = model;
        }

        // Return ip from model, if changed send notify.
        public string FlightServerIP
        {
            get { return model.FlightServerIP; }
            set
            {
                model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        // Return port from model, if changed send notify.
        public int FlightCommandPort
        {
            get { return model.FlightCommandPort; }
            set
            {
                model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        // Return port for server from model, if changed send notify
        public int FlightInfoPort
        {
            get { return model.FlightInfoPort; }
            set
            {
                model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }

        // Send command for save settings to model.
        public void SaveSettings() => model.SaveSettings();

        // Send command for reload settings to model
        public void ReloadSettings() => model.ReloadSettings();

        #region Commands
        #region ClickCommand       
        private ICommand _clickCommand;  // Click command got from view.
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick() // In ok click save changes.
        {
            model.SaveSettings();
        }
        #endregion

        #region CancelCommand  
        private ICommand _cancelCommand;    // In cancel reload data.
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        private void OnCancel()
        {
            // Send reload opeartion to model.
            model.ReloadSettings();
        }
        #endregion
        #endregion
    }
}

