using System;
using System.Windows;
using SQLite;

namespace PrinCa
{
    /// <summary>
    /// Interaktionslogik für AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Window
    {
        public AddLocation()
        {
            InitializeComponent();
            lbl_currencyKWH.Content = $"Electricity Costs ({Globals.currencySymbol}/kWh):";
        }

        private void btn_SavePrinter_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCheck())
            {
                try
                {
                    //save to DB
                    SQLiteConnection data = new SQLiteConnection(Globals.dbFullPath);
                    var location = new Locations() { Name_EN = tbx_LocationName.Text, Electricity_Costs = (decimal)numUpDn_Ecosts.Value };
                    data.Insert(location);
                    data.Close();

                    this.Close();
                }
                catch(Exception ex)
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

            if (tbx_LocationName.Text.Length > 0)
                x = true;
            
            return x;
        }
    }
}
