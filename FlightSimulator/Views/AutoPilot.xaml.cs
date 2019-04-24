using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for AutoPilot.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        public AutoPilot()
        {
            InitializeComponent();
        }

        // now using RoutedEvent, but when a class of autoPilotViewModel is created should bind it to a property incharge on clicking the OK button.
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Button OK_button = e.Source as Button;
            txt.Background = Brushes.White;
        }

        // now using RoutedEvent, but when a class of autoPilotViewModel is created should bind it to a property incharge on clicking the Cancel button.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Button Cancel_button = e.Source as Button;
            txt.Clear();
        }
    }
}
