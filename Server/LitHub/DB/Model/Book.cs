﻿using System;

namespace LitHub.DB.Model
{
    public class Book : Entity
    {
        public Guid HubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }

        public virtual Hub Hub { get; set; }
    }
}
