using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployBD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeployBD.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDeployService _deployService;

        public IndexModel(IServiceProvider serviceProvider, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _deployService = serviceProvider.GetRequiredService<IDeployService>();
        }

        public void OnGet()
        {
            _logger.LogInformation("Get page");
        }
    }
}
