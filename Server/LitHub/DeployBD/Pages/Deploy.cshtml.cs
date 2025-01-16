using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployBD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace DeployBD.Pages
{
    public class DeployModel : PageModel
    {
        private IDeployService deployService;
        public DeployModel(IServiceProvider serviceProvider)
        {
            deployService = serviceProvider.GetRequiredService<IDeployService>();
        }

        public void OnGet()
        {
            
        }

        public void OnPostDeploy()
        {
            deployService.DeployBD();
        }
    }
}