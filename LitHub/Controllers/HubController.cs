using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LitHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly HubDetails[] _hubs = new HubDetails[] {
                new HubDetails(){
                    Author = "admin",
                    DateModified  = DateTimeOffset.Now,
                    Name = "Book1",
                    Id = 1,
                    Description = "Test hub 1",
                    Files = new Book[]{
                        new Book()
                        {
                            Id = 1,
                            Name = "Chapter1",
                            Description = "Chapter1 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 2,
                            Name = "Chapter2",
                            Description = "Chapter2 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 3,
                            Name = "Chapter3",
                            Description = "Chapter3 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        }
                    }
                },
                new HubDetails(){
                    Author = "admin",
                    DateModified  = DateTimeOffset.Now,
                    Name = "Book2",
                    Id = 2,
                    Description = "Test hub 2",
                    Files = new Book[]{
                        new Book()
                        {
                            Id = 1,
                            Name = "Chapter1",
                            Description = "Chapter1 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 2,
                            Name = "Chapter2",
                            Description = "Chapter2 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 3,
                            Name = "Chapter3",
                            Description = "Chapter3 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        }
                    }
                },
                new HubDetails(){
                    Author = "admin",
                    DateModified  = DateTimeOffset.Now,
                    Name = "Book3",
                    Id = 3,
                    Description = "Test hub 3",
                    Files = new Book[]{
                        new Book()
                        {
                            Id = 1,
                            Name = "Chapter1",
                            Description = "Chapter1 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 2,
                            Name = "Chapter2",
                            Description = "Chapter2 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        },
                        new Book()
                        {
                            Id = 3,
                            Name = "Chapter3",
                            Description = "Chapter3 description",
                            Author = "admin",
                            DateModified = DateTimeOffset.Now
                        }
                    }
                }
            };

        public HubController()
        {

        }

        // GET: api/Hub
        [HttpGet]
        public IEnumerable<Hub> Get()
        {
            return _hubs;
        }

        // GET: api/Hub/5
        [HttpGet("{id}", Name = "Get")]
        public HubDetails Get(int id)
        {
            return _hubs[id-1];
        }

        // POST: api/Hub
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Hub/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Hub {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Author { get; set; }
    }

    public class HubDetails: Hub
    {
        public string Description { get; set; }
        public Book[] Files { get; set; }
    }

    public class Book {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Author { get; set; }
    }
}
