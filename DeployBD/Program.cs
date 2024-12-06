using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace DeployBD
{
    public class Program
    {
        public static void Main(string[] args)
        {           
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("log\\log.log", LogEventLevel.Debug)
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();                
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");                
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) {


            return WebHost.CreateDefaultBuilder(args)       
                .ConfigureAppConfiguration(config=>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .UseStartup<Startup>()
                .ConfigureLogging((s) => {
                    s.AddSerilog(Log.Logger);
                });
        }
    }
}
