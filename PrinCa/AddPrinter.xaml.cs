using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SQLite;

namespace PrinCa
{
    /// <summary>
    /// Interaktionslogik für AddPrinter.xaml
    /// </summary>
    public partial class AddPrinter : Window
    {
        List<Powermodes> li_power = new List<Powermodes>();
        //List<Filament> li_allFilaments = new List<Filament>();
        List<v_FilaInfo> li_matchingFilaments = new List<v_FilaInfo>();
        
        public AddPrinter()
        {
            InitializeComponent();
            reloadPowermodes();
        }

        private void reloadPowermodes()
        {
            dg_powermodes.ItemsSource = null;
            dg_powermodes.Items.Clear();
            dg_powermodes.ItemsSource = li_power;
        }

        private void btn_SavePrinter_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCheck())
            {
                try
                {
                    int printerID = 1;
                    //save to DB
                    SQLiteConnection data = new SQLiteConnection(Globals.dbFullPath);
                    var printer = new Printer() { Name_EN = tbx_PrinterName.Text };
                    data.Insert(printer);

                    string sql = $"Select * From Printer Where Name_EN = '{tbx_PrinterName.Text}'";
                    List<Printer> li_printer = new List<Printer>();
                    li_printer = data.Query<Printer>(sql).ToList();

                    foreach (Printer x in li_printer)
                    {
                            printerID = x.Id;                         
                    }

                    foreach (Powermodes p in li_power)
                    {
                        var powermode = new Powermodes { MaterialName = p.MaterialName, Wattage = p.Wattage, Printer_Id = printerID };
                        data.Insert(powermode);
                    }

                    data.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill all relevant fields!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool fieldCheck()
        {
            bool x = false;

            if (tbx_PrinterName.Text.Length > 0)
                if (li_power.Count > 0)
                    x = true;
             
            return x;
        }

        private void btn_addPowermode_Click(object sender, RoutedEventArgs e)
        {
            AddPowermode addpo = new AddPowermode();
            addpo.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addpo.Owner = this;

            if (addpo.ShowDialog() == true)
            {
                Powermodes pm = new Powermodes { MaterialName = addpo.MaterialName, Wattage = addpo.WattagePower };
                li_power.Add(pm);
                reloadPowermodes();
            }         

        }
    }
}
