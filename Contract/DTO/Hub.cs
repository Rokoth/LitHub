using Microsoft.AspNetCore.Http;
using System;

namespace LitHub.Contract
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTimeOffset VersionDate { get; set; }
    }

    public class Hub : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }

        public virtual Book[] Books { get; set; }
    }

    public class Book : Entity
    {
        public Guid HubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public IFormFile FormFile { get; set; }


        public virtual Hub Hub { get; set; }
    }
}
