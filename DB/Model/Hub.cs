using LitHub.Db.Attributes;
using System;

namespace LitHub.DB.Model
{
    [TableName("hub")]
    public class Hub : Entity
    {
        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("description")]
        public string Description { get; set; }
       
        [ColumnName("path")]
        public string Path { get; set; }       
    }
}
