using SQLite;

namespace PrinCa
{
    [Table("material")]
    class Material
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int Id { get; set; }
        public string Name_EN { get; set; }

    }
}
