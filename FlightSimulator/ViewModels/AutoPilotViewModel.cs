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
        private string commands_txt;
        private ICommand _OK_Command;
        private ICommand _Cancel_Command;

        public AutoPilotViewModel() => this.model = new AutoPilotModel();

        public ICommand OK_Command
        {
            get
            {
                return _OK_Command ?? (_OK_Command = new CommandHandler(() => { model.SendCommands(commands_txt); }));
            }
        }

        public ICommand Cancel_Command
        {
            get
            {
                return _Cancel_Command ?? (_Cancel_Command = new CommandHandler(() => { commands_txt=""; }));
            }
        }
    }
}
