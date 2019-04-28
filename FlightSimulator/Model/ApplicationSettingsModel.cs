using FlightSimulator.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    public class ApplicationSettingsModel : ISettingsModel  // Settings app model.
    {
        #region Singleton
        private static ISettingsModel m_Instance = null;    // Create single instance of settings.
        public static ISettingsModel Instance
        {
            get
            {
                if(m_Instance == null)
                {
                    m_Instance = new ApplicationSettingsModel();
                }
                return m_Instance;
            }
        }
        #endregion
        public string FlightServerIP        // Get ip from Confg file.
        {
            get { return Properties.Settings.Default.FlightServerIP; }
            set { Properties.Settings.Default.FlightServerIP = value; }
        }
        public int FlightCommandPort // Get port for command opeartion from Confg file.
        {
            get { return Properties.Settings.Default.FlightCommandPort; }
            set { Properties.Settings.Default.FlightCommandPort = value; }
        }

        public int FlightInfoPort // Get port for info server from Confg file.
        {
            get { return Properties.Settings.Default.FlightInfoPort; }
            set { Properties.Settings.Default.FlightInfoPort = value; }
        }

        public void SaveSettings()  // Save settings in confg file.
        {
            Properties.Settings.Default.Save();
        }

        public void ReloadSettings() // Reload data.
        {
            Properties.Settings.Default.Reload();
        }
    }
}
