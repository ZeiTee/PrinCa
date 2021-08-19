using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinCa
{
    public static class Globals
    {
        public static string DbPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Arimatas Software\PrinCa\DB\BaseDatabase.sqlite";
        public static Settings Settings { get; set; }
        public static string CurrencySymbol { get => Settings.currencySymbol; set => Settings.currencySymbol = value; }
    }
}
