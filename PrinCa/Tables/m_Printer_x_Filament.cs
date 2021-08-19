using SQLite;

namespace PrinCa
{
    [Table("m_Printer_x_Filament")]
    public class m_Printer_x_Filament
    {
        public int id_Printer { get; set; }
        public int id_Filament { get; set; }
    }
}
