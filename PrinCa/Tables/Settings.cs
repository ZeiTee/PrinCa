﻿using SQLite;

namespace PrinCa
{
    [Table("settings")]
    public class Settings
    {
        [PrimaryKey, AutoIncrement, Unique]
        [Column("id")]
        public int Id { get; set; }

        public string currencySymbol { get; set; }

    }
}
