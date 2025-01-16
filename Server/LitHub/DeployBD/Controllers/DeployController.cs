using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployBD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeployBD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeployController : Controller
    {
        private ILogger<DeployController> _logger;
        private readonly IDeployService _deployService;

        public DeployController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger < DeployController >> ();
            _deployService = serviceProvider.GetRequiredService<IDeployService>();
        }

        [HttpGet("[action]")]
        public void Deploy()
        {
            _logger.LogInformation("Begin deploy");
            try
            {
                _deployService.DeployBD();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception throws: {ex.Message} \r\n {ex.StackTrace}");
            }
        }
    }
}