using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace FlightSimulator.Views
{
    public partial class FlightBoard : UserControl
    {
        // Point collection data.
        ObservableDataSource<Point> planeLocations = null;
        FlightBoardViewModel vm;

        public FlightBoard()
        {
            // Create Board VM, add observer to vm of flight board. 
            vm = new FlightBoardViewModel();
            InitializeComponent();
            this.vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            planeLocations = new ObservableDataSource<Point>();
            // Set identity mapping of point in collection to point on plot
            planeLocations.SetXYMapping(p => p);
            plotter.AddLineGraph(planeLocations, 2, "Route");
        }

        private void Vm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // In event check if property changes fit, is so add plane location.
            if (e.PropertyName.Equals("VM_Lat") || e.PropertyName.Equals("VM_Lon"))
            {
                // Add point to collection.
                planeLocations.AppendAsync(Dispatcher, new Point(vm.Lat, vm.Lon));
            }
        }
    }
}
