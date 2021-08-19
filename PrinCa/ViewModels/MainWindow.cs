using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PrinCa.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class MainWindow : ViewModel
    {
        private Window _window;

        private ObservableCollection<Filament> _filaments;
        public ObservableCollection<Filament> Filaments { get { return _filaments; } set { _filaments = value; OnPropertyChanged(nameof(Filaments)); } }

        private string _currencySymbol;
        public string CurrencySymbol { get { return _currencySymbol; } set { _currencySymbol = value; OnPropertyChanged(nameof(CurrencySymbol)); } }

        private bool _canChangeCurrencySymbol;
        public bool CanChangeCurrencySymbol { get { return _canChangeCurrencySymbol; } set { _canChangeCurrencySymbol = value; OnPropertyChanged(nameof(CanChangeCurrencySymbol)); } }


        public MainWindow(Window window)
            => _window = window;

        public void Initialize()
        {
            using var db = new SQLiteConnection(Globals.DbPath);

            CurrencySymbol = Globals.CurrencySymbol;

            var filaments = db.Table<Filament>().ToList();
            Filaments = new ObservableCollection<Filament>(filaments);
        }

        public void AddFilament()
        {
            AddFilament addf = new AddFilament();
            addf.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addf.Owner = _window;
            if (addf.ShowDialog() == true)
                Filaments.Add(addf.Filament);
        }

        public void ChangeCurrencySymbol()
        {
            if (CanChangeCurrencySymbol)
            {
                using var db = new SQLiteConnection(Globals.DbPath);
                db.Execute($@"
                    UPDATE Settings 
                       SET currencySymbol = '{CurrencySymbol}'
                     WHERE id = 1
                ");
                Globals.CurrencySymbol = CurrencySymbol;
            }
            else
            {
                CurrencySymbol = Globals.CurrencySymbol;
            }

            CanChangeCurrencySymbol = !CanChangeCurrencySymbol;
        }
    }
}
