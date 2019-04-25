using FlightSimulator.ViewModels.Windows;
using FlightSimulator.Model;
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

namespace FlightSimulator.Views.Windows
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsWindowViewModel VM_settings;
        public SettingsWindow()
        {
            InitializeComponent();
            VM_settings = new SettingsWindowViewModel(new ApplicationSettingsModel());
            this.DataContext = VM_settings;
        }

        private void Btn_OK(object sender, RoutedEventArgs e)
        {
            VM_settings.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();

        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
            VM_settings.ReloadSettings();
        }
    }
}
