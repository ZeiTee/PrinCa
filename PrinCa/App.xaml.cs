using SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PrinCa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string dbDirectory = Path.GetDirectoryName(Globals.DbPath);
            if (!Directory.Exists(dbDirectory))
                Directory.CreateDirectory(dbDirectory);
            var dbExists = File.Exists(Globals.DbPath);
            using var db = new SQLiteConnection(Globals.DbPath);
            if (!dbExists)
            {
                db.CreateTable<Settings>();
                db.CreateTable<Locations>();
                db.CreateTable<Printer>();
                db.CreateTable<Powermodes>();
                db.CreateTable<Filament>();
                db.CreateTable<Material>();
                db.CreateTable<m_Printer_x_Filament>();

                Globals.Settings = new Settings() { Id = 1, currencySymbol = "$" };
                db.Insert(Globals.Settings);

                var materials = new string[] { "PLA", "PETG", "ASA", "ABS", "PC", "CPE", "PVA/BVOH", "PVB", "HIPS", "PP", "Flex", "nGen", "Nylon", "Woodfill" };
                db.InsertAll(materials.Select(m => new Material { Name_EN = m }));

                db.Execute(@"
                    CREATE VIEW v_FilaInfo AS
                         SELECT fil.id AS id_Filament,
                                fil.company || ' ' || fil.MaterialName || ' ' || fil.Colorname AS FilamentName, 
                                fil.colorhex,
                                mpf.id_Printer
                           FROM filament fil
                      LEFT JOIN m_Printer_x_Filament mpf 
                             ON mpf.id_Filament = fil.id
                ");
            }
            else
            {
                Globals.Settings = db.FindWithQuery<Settings>(
                    "SELECT * FROM settings WHERE id=1;"
                );
            }
        }
    }
}
