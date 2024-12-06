using LitHub.Db.Attributes;
using System;

namespace LitHub.DB.Model
{
    public abstract class Entity : IEntity
    {
        [PrimaryKey]
        [ColumnName("id")]
        public Guid Id { get; set; }
        [ColumnName("version_date")]
        public DateTimeOffset VersionDate { get; set; }
        [ColumnName("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
