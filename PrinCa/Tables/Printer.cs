using SQLite;

namespace PrinCa
{
    [Table("printer")]
    class Printer
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int Id { get; set; }
        public string Name_EN { get; set; }

    }
}
