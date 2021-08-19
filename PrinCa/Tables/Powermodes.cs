using SQLite;

namespace PrinCa
{
    [Table("powermodes")]
    public class Powermodes
    {
        public int Printer_Id { get; set; }
        public string MaterialName { get; set; }
        public double Wattage { get; set; }
}
}
