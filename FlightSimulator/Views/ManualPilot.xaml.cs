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
            this.Joystick.Moved += this.vm.Joystick_Move;
        }

        /*private void RudderSld_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.vm.VM_Rudder = this.rudderSld.Value;
        }

        private void ThrottleSld_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.vm.VM_Throttle = this.throttleSld.Value;
        }*/
    }
}
