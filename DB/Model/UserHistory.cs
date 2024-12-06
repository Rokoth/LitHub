﻿using LitHub.Db.Attributes;
using System;

namespace LitHub.DB.Model
{
    [TableName("h_user")]
    public class UserHistory : EntityHistory
    {
        [ColumnName("name")]
        public string Name { get; set; }
        [ColumnName("description")]
        public string Description { get; set; }
        [ColumnName("email")]
        public string Email { get; set; }
        [ColumnName("login")]
        public string Login { get; set; }
        [ColumnName("password")]
        public byte[] Password { get; set; }
        [ColumnName("formula_id")]
        public Guid FormulaId { get; set; }
        [ColumnName("last_add_date")]
        public DateTimeOffset LastAddedDate { get; set; }
    }
}
