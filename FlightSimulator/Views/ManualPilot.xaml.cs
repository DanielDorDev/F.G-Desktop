using FlightSimulator.ViewModels;
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
    /// Interaction logic for ManualPilot.xaml
    /// </summary>
    public partial class ManualPilot : UserControl
    {
        private ManualViewModel vm;
        public ManualPilot()
        {
            InitializeComponent();
            this.vm = new ManualViewModel();
            this.DataContext = vm;
            // Registering the Joystick_Move method to the Moved event in the Joystick.
            this.Joystick.Moved += this.vm.Joystick_Move;
        }
    }
}
