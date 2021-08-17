using SQLite;

namespace PrinCa
{
    [Table("locations")]
    class Locations
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public decimal Electricity_Costs { get; set; }
    }
}
