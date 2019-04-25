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
        private ITelnetClient server;

        public AutoPilotModel() => server = ConnectToServer.Instance;

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
            {
                return;
                //throw new ArgumentException("message", nameof(commands));
            }
            //List<string> arr = new List<string>();
            //Interpret(ref arr, commands);

            string[] commands = commands_txt.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            new Thread(delegate ()
            {
                for (int i = 0; i < commands.Length; i++)
                {
                    this.server.Write(commands[i]);
                    Thread.Sleep(200);
                }
            }).Start();
        }
        
        /*private void Interpret (ref List<string> arr,string toInterpret)
        {
            string temp ="";
            int j = 0;
            for(int i = 0; i < toInterpret.Length; i++)
            {
                if (toInterpret[i] != '\n')
                {
                    arr[j] = temp;
                    temp = "";
                    j++;
                }
                temp += toInterpret[i];
            }
            if (!string.IsNullOrEmpty(temp)) { arr[j] = temp; }
        }*/
    }
}
