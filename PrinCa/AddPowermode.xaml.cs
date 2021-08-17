using System.Collections.Generic;
using System.Windows;
using SQLite;


namespace PrinCa
{
    /// <summary>
    /// Interaktionslogik für AddPowermode.xaml
    /// </summary>
    public partial class AddPowermode : Window
    {
        List<Material> li_material = new List<Material>();
        List<string> li_materialForFiltering = new List<string>();
        public AddPowermode()
        {
            InitializeComponent();
            LoadMaterialList();
        }

        private void LoadMaterialList()
        {
            cmbx_Material.ItemsSource = null;
            li_material.Clear();

            SQLiteConnection db;
            db = new SQLiteConnection(Globals.dbFullPath);
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

        public string MaterialName
        {
            get { return cmbx_Material.Text; }
            set { cmbx_Material.Text = value; }
        }

        public double WattagePower
        {
            get { return (double)numUpDn_Wattage.Value; }
            set { numUpDn_Wattage.Value = value; }
        }

        private void btn_SavePowermode_Click(object sender, RoutedEventArgs e)
        {
            if (fieldCheck())
                this.DialogResult = true;
            else
            {
                MessageBox.Show("Fill all relevant fields!", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
        private bool fieldCheck()
        {
            bool x = false;

            if (cmbx_Material.Text.Length > 0)
                x = true;

            return x;
        }
    }
}
