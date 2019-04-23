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
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summarry>
    public partial class MainWindow : Window
    {
        ViewModels.Windows.SettingsWindowViewModel VM_settings;
        public MainWindow()
        {
            VM_settings = new ViewModels.Windows.SettingsWindowViewModel(new Model.ApplicationSettingsModel());
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Views.Windows.SettingsWindow objSettings = new Views.Windows.SettingsWindow();
            objSettings.Show();
        }

    }
}
