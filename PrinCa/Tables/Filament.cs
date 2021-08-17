using SQLite;

namespace PrinCa
{
    [Table("filament")]
    class Filament
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int Id { get; set; }
        public string Company { get; set; }
        public string ColorName { get; set; }
        public string ColorHEX { get; set; }
        public string MaterialName { get; set; }
        public decimal Cost { get; set; }
        public bool Track_Weight_Loss { get; set; }
        public decimal Weight_Total { get; set; }
        public decimal Weight_Total_Used { get; set; }
    }
}
