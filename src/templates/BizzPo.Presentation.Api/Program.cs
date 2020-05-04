using BizzPo.Presentation.Common.Configs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BizzPo.Presentation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).BuidAndRun();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}