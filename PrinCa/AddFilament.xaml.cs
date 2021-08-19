using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SQLite;

namespace PrinCa
{
    /// <summary>
    /// Interaktionslogik für AddFilament.xaml
    /// </summary>
    public partial class AddFilament : Window
    {
        public Filament Filament { get; private set; } = null;

        List<Material> li_material = new List<Material>();
        List<string> li_materialForFiltering = new List<string>();
        string hexColor = "";

        public AddFilament()
        {
            InitializeComponent();
            LoadMaterialList();
        }

        private void LoadMaterialList()
        {
            cmbx_Material.ItemsSource = null;
            li_material.Clear();

            SQLiteConnection db;
            db = new SQLiteConnection(Globals.DbPath);
            li_material = db.Table<Material>().ToList();
            db.Close();

            foreach (Material x in li_material)
            {
                li_materialForFiltering.Add(x.Name_EN);
            }

            cmbx_Material.ItemsSource = li_materialForFiltering;

            //set default
            ComboxDefault cbd = new ComboxDefault();
            cbd.SetDefaultValue("PLA", cmbx_Material);
        }

        private void myColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            myColorPicker.IsDropDownOpen = false;
            hexColor = myColorPicker.SelectedColor.ToString();
            //hexColor = myColorPicker.SelectedColor.ToString().Replace("#", "");
        }

        private bool fieldCheck()
        {
            bool x = false;

            if (tbx_company.Text.Length > 0)
                if (tbx_colorName.Text.Length > 0)
                    if(cmbx_Material.Text.Length > 0)
                        x = true;

            return x;
        }

        private void btn_SaveFilament_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCheck())
            {
                try
                {
                    //save to DB
                    SQLiteConnection data = new SQLiteConnection(Globals.DbPath);
                    Filament = new Filament() { ColorHEX = hexColor, ColorName = tbx_colorName.Text, Company = tbx_company.Text, Cost = (decimal)numUpDn_cost.Value, Track_Weight_Loss = chbx_trackFilamentWeight.IsEnabled, Weight_Total = (decimal)numUpDn_WeightTotal.Value, Weight_Total_Used = (decimal)numUpDn_WeightUsed.Value, MaterialName = cmbx_Material.Text};
                    data.Insert(Filament);
                    this.DialogResult = true;
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



    }
}
