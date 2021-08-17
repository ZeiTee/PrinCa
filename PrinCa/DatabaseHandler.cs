using SQLite;
using System.Collections.Generic;

namespace PrinCa
{
    public class DatabaseHandler
    {
        private SQLiteConnection _db;

        public DatabaseHandler()
        {
            _db = new SQLiteConnection(Globals.dbFullPath);
            _db.CreateTable<Settings>();
            _db.CreateTable<Locations>();
            _db.CreateTable<Printer>();
            _db.CreateTable<Powermodes>();
            _db.CreateTable<Filament>();
            _db.CreateTable<Material>();
            _db.CreateTable<m_Printer_x_Filament>();
            _db.Close();
        }
        //The tables will be automatically mapped to the SQLite database and we will be able to access them using SQL queries.
        //If the tables do not exist in the SQLite database, they will be generated.If they do exist, 
        //any existing data in the table will be "Migrated" and the tables will be updated (if new fields have been added).

        public void CreateDBWithEntries()
        {
            AddSettings();
            AddMaterials();
            createFilamentView();
        }

        public void AddSettings()
        {
            _db = new SQLiteConnection(Globals.dbFullPath);
            var settings = new Settings() { Id = 1, currencySymbol = "$"};
            _db.Insert(settings);
            _db.Close();
        }

        public void AddMaterials()
        {
            List<Material> li_mats = new List<Material>();
            li_mats.Add(new Material() { Name_EN = "PLA" });
            li_mats.Add(new Material() { Name_EN = "PETG" });
            li_mats.Add(new Material() { Name_EN = "ASA" });
            li_mats.Add(new Material() { Name_EN = "ABS" });
            li_mats.Add(new Material() { Name_EN = "PC" });
            li_mats.Add(new Material() { Name_EN = "CPE" });
            li_mats.Add(new Material() { Name_EN = "PVA/BVOH" });
            li_mats.Add(new Material() { Name_EN = "PVB" });
            li_mats.Add(new Material() { Name_EN = "HIPS" });
            li_mats.Add(new Material() { Name_EN = "PP" });
            li_mats.Add(new Material() { Name_EN = "Flex" });
            li_mats.Add(new Material() { Name_EN = "nGen" });
            li_mats.Add(new Material() { Name_EN = "Nylon" });
            li_mats.Add(new Material() { Name_EN = "Woodfill" });

            _db = new SQLiteConnection(Globals.dbFullPath);

            foreach (Material mat in li_mats)
            {
                _db.Insert(mat);
            }
            _db.Close();
        }


        public void createFilamentView() {

            string execSQL =    "CREATE VIEW v_FilaInfo AS " +
                                "SELECT fil.id AS id_Filament, " +
                                       "fil.company || ' ' || fil.MaterialName || ' ' || fil.Colorname AS FilamentName, " +
                                       "fil.colorhex, " +
                                       "mpf.id_Printer " +
                                "FROM filament fil " +
                                "LEFT JOIN m_Printer_x_Filament mpf ON mpf.id_Filament = fil.id; ";

            _db = new SQLiteConnection(Globals.dbFullPath);
            _db.BeginTransaction();
            _db.Execute(execSQL);
            _db.Commit();
            _db.Close();


        //    var queries = new List<string>()
        //{
        //    " DELETE FROM Product",
        //    " INSERT INTO Product VALUES (1,\"Name1\",1,1)",
        //    " INSERT INTO Product VALUES (2,\"Name2\",2,3)"
        //};

        //    db.BeginTransaction();
        //    queries.ForEach(query => db.Execute(query));
        //    db.Commit();
        } 
    }
}
