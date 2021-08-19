using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using SQLite;

namespace PrinCa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainWindow(this);
        }

        private void btn_addLocation_Click(object sender, RoutedEventArgs e)
        {
            AddLocation addl = new AddLocation();
            addl.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addl.Owner = this;
            addl.ShowDialog();
        }

        private void btn_addPrinter_Click(object sender, RoutedEventArgs e)
        {
            AddPrinter addp = new AddPrinter();
            addp.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addp.Owner = this;
            addp.ShowDialog();
        }

        private void btn_addFilament_Click(object sender, RoutedEventArgs e)
            => (DataContext as ViewModels.MainWindow)?.AddFilament();

        private void btn_settingsChangeSave_Click(object sender, RoutedEventArgs e)
            => (DataContext as ViewModels.MainWindow)?.ChangeCurrencySymbol();

        private void Window_Loaded(object sender, RoutedEventArgs e)
            => (DataContext as ViewModels.MainWindow)?.Initialize();
    }
}
