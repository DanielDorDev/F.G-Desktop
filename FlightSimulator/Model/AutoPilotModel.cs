using FlightSimulator.Model.Interface;
using FlightSimulator.Model.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FlightSimulator.Model
{
    class AutoPilotModel
    {
        private IFlightModel server;

        public AutoPilotModel() => server = FlightGearModel.Instance;

        private bool _change_Background;
        public bool Change_Background
        {
            set
            {
                this._change_Background = value;
            }
            get { return this._change_Background; }
        }

        public void SendCommands(string commands_txt)
        {
            if (string.IsNullOrEmpty(commands_txt))
                return;
                  
            string[] commands = commands_txt.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            new Thread(delegate ()
            {
                for (int i = 0; i < commands.Length; i++)
                {
                    this.server.Send(commands[i]);
                    Thread.Sleep(200);
                }
            }).Start();
        }
    }
}
