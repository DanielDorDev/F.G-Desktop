using FlightSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private AutoPilotModel model;
        private ICommand _OK_Command;
        private ICommand _Cancel_Command;
        private string commands_txt;

        public AutoPilotViewModel()
        {
            this.model = new AutoPilotModel();
            this.VM_Change_Background = true;
        }

        public bool VM_Change_Background
        {
            set
            {
                this.model.Change_Background = value;
                NotifyPropertyChanged("VM_Change_Background");
            }
            get { return this.model.Change_Background; }
        }

        public string VM_Commands_txt
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    VM_Change_Background = false;
                }
                commands_txt = value;
                NotifyPropertyChanged("VM_Commands_txt");
            }
        }
        public ICommand OK_Command => _OK_Command ?? (_OK_Command = new CommandHandler(() =>
        {
            model.SendCommands(commands_txt);
            this.VM_Change_Background = true;
        }));

        public ICommand Cancel_Command => _Cancel_Command ?? (_Cancel_Command = new CommandHandler(() =>
        {
            VM_Commands_txt = "";
            this.VM_Change_Background = true;
        }));
    }
}
