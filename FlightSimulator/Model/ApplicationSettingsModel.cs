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
                if (m_Instance == null)
                {
                    m_Instance = new ApplicationSettingsModel();
                }
                return m_Instance;
            }
        }
        #endregion

        // Get ip from Confg file.
        public string FlightServerIP
        {
            get { return Properties.Settings.Default.FlightServerIP; }
            set { Properties.Settings.Default.FlightServerIP = value; }
        }

        // Get port for command opeartion from Confg file.
        public int FlightCommandPort
        {
            get { return Properties.Settings.Default.FlightCommandPort; }
            set { Properties.Settings.Default.FlightCommandPort = value; }
        }

        // Get port for info server from Confg file.
        public int FlightInfoPort
        {
            get { return Properties.Settings.Default.FlightInfoPort; }
            set { Properties.Settings.Default.FlightInfoPort = value; }
        }

        // Save settings in confg file.
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        // Reload data.
        public void ReloadSettings()
        {
            Properties.Settings.Default.Reload();
        }
    }
}
