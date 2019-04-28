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

        // Constructor.
        public AutoPilotViewModel()
        {
            this.model = new AutoPilotModel();
            this.VM_Change_Background = true;
        }

        // Property which is binded to the Background color of the text in the AutoPilot view.
        public bool VM_Change_Background
        {
            set
            {
                this.model.Change_Background = value;
                NotifyPropertyChanged("VM_Change_Background");
            }
            get { return this.model.Change_Background; }
        }

        // Property which is binded to the textBox, which holds the text with the command to the server, in the AutoPilot view.
        public string VM_Commands_txt
        {
            set
            {
                // If there is no text then the background color should be white.
                if (!string.IsNullOrEmpty(value))
                    VM_Change_Background = false;
                // Else, the background color should be pink/.
                else VM_Change_Background = true;

                commands_txt = value;
                NotifyPropertyChanged("VM_Commands_txt");
            }
        }

        // A command which is activated to the OK button in the AutoPilot view.
        public ICommand OK_Command => _OK_Command ?? (_OK_Command = new CommandHandler(() =>
        {
            model.SendCommands(commands_txt);
            // The background color should should change to white.
            this.VM_Change_Background = true;
        }));

        // A command which is activated to the Cancel button in the AutoPilot view.
        public ICommand Cancel_Command => _Cancel_Command ?? (_Cancel_Command = new CommandHandler(() =>
        {
            VM_Commands_txt = "";
            // The background color should should change to white.
            this.VM_Change_Background = true;
        }));
    }
}
