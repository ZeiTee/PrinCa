using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinCa
{
    public static class Globals
    {
        public static string dbFullPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Arimatas Software\PrinCa\DB\BaseDatabase.sqlite";
        public static string dbDirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Arimatas Software\PrinCa\DB";
        public static string currencySymbol = "?";

    }
}
