using System;

namespace LitHub.Db.Attributes
{
    /// <summary>
    /// Атрибут Имя таблицы (используется в контексте БД в методе создания моделей)
    /// </summary>
    public class TableNameAttribute : Attribute
    {
        /// <summary>
        /// Наименование таблицы
        /// </summary>
        public string Name { get; }

        public TableNameAttribute(string name)
        {
            Name = name;
        }
    }
}
