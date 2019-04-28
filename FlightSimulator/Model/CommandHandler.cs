using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand // Command handler object, for command by mvvm structure.
    {
        private Action _action;
        public CommandHandler(Action action) => _action = action;

        public bool CanExecute(object parameter) => true;    // Return if executable.

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter) => _action();       // Execute the action.
    }
}
