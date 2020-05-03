using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BizzPo.Presentation.Common.Configs
{
    public static class HostBuilderConfiguration
    {
        public static void BuidAndRun(this IHostBuilder hostBuilder)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                var host = hostBuilder.Build();
                //uncomment if you want to auto migrate database on development environment
                ///host.Services.AutoMigrateDevDb();
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Application start-up failed: {ex.Message}");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}