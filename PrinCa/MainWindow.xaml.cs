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
        List<Filament> li_filament = new List<Filament>();

        public MainWindow()
        {
            InitializeComponent();
            StartUp();
            reloadFilament();
        }

        private void StartUp()
        {
            //DB + Paths
            if (!Directory.Exists(Globals.dbDirPath))
                Directory.CreateDirectory(Globals.dbDirPath);

            if (!File.Exists(Globals.dbFullPath))
            {
                DatabaseHandler dbhandle = new DatabaseHandler();
                dbhandle.CreateDBWithEntries();
            }
            else
            {
                DatabaseHandler dbhandle = new DatabaseHandler();
            }

            //load DB values
            try
            {             
                SQLiteConnection db;
                db = new SQLiteConnection(Globals.dbFullPath);
                List<Settings> li_setting = new List<Settings>();
                string sql = "Select currencySymbol from Settings where id=1;";
                li_setting = db.Query<Settings>(sql).ToList();

                foreach (Settings x in li_setting)
                {
                    Globals.currencySymbol = x.currencySymbol;
                    tbx_SettingsCurr.Text = x.currencySymbol;
                }
                db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void reloadFilament()
        {
            li_filament.Clear();
            datagrid_filament.ItemsSource = null;

            SQLiteConnection db;
            db = new SQLiteConnection(Globals.dbFullPath);
            li_filament = db.Table<Filament>().ToList();
            db.Close();

            datagrid_filament.ItemsSource = li_filament;
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
        {
            AddFilament addf = new AddFilament();
            addf.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addf.Owner = this;
            addf.ShowDialog();
            reloadFilament();
        }

        private void btn_settingsChangeSave_Click(object sender, RoutedEventArgs e)
        {
            if(btn_settingsChangeSave.Content.ToString() == "Change")
            {
                tbx_SettingsCurr.IsEnabled = true;
                btn_settingsChangeSave.Content = "Save";
            }
            else
            {
                tbx_SettingsCurr.IsEnabled = false;
                btn_settingsChangeSave.Content = "Change";
                //save

                try
                {
                    SQLiteConnection db;
                    db = new SQLiteConnection(Globals.dbFullPath);
                    db.Execute($"Update Settings SET currencySymbol = '{tbx_SettingsCurr.Text}' WHERE id = 1");
                    db.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    Globals.currencySymbol = tbx_SettingsCurr.Text;
                }
            }
        }
    }
}
