using System;
using BizzPoService.Infrastructure.Messaging.SqlPubSub.Database;
using BizzPoService.Infrastructure.Repositories.EfSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BizzPoService.Presentation.Common.Configs
{
    public static class SqlMigrateConfiguration
    {
        public static void AutoMigrateDevDb(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var environment = services.GetService<IHostEnvironment>();

                if (!environment.IsDevelopment()) return;

                Log.Information("Migrating database...");
                var context = services.GetRequiredService<ContactsDbContext>();
                context.Database.Migrate();
            }
        }

        public static void AutoMigrateDevIntegrationEventsDb(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var environment = services.GetService<IHostEnvironment>();

                if (!environment.IsDevelopment()) return;

                Log.Information("Migrating database...");
                var context = services.GetRequiredService<IntegrationEventDbContext>();
                context.Database.Migrate();
            }
        }
    }
}