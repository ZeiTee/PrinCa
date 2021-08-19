using SQLite;

namespace PrinCa
{
    [Table("v_FilaInfo")]
    public class v_FilaInfo
    {
        public int id_Filament { get; set; }
        public string FilamentName { get; set; }
        public string colorhEX { get; set; }
        public int id_Printer { get; set; }
    }
}
