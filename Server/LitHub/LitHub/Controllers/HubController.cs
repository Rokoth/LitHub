using LibGit2Sharp;
using LitHub.Contract;
using LitHub.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace LitHub.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HubController : ControllerBase
    {        
        private IGitService gitService;

        public HubController(IGitService gitService)
        {
            this.gitService = gitService;
        }

        // GET: api/Hub
        [HttpGet]
        public IEnumerable<Hub> Get()
        {
            return gitService.GetHubs();
        }

        // GET: api/Hub/5
        [HttpGet("{id}", Name = "Get")]
        public Hub Get(int id)
        {
            return gitService.GetHub(id);
        }

        // POST: api/Hub
        [HttpPost]
        public void Post([FromBody] Hub value)
        {
            gitService.GitCreate(value);            
        }

        // PUT: api/Hub/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Hub value)
        {
            gitService.GitUpdate(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            gitService.GitDelete(id);
        }
    }
}
