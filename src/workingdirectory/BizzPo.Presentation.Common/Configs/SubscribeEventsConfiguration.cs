using BizzPo.Application.Subscribe.AccountCreated;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class SubscribeEventsConfiguration
    {
        public static void AddSubscribeEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var azureServiceBusConnectionString = connectionStringSection["AzureServiceBus"];
            var accountCreatedEventSection = configuration.GetSection("Events:Subscribe:AccountCreatedEvent");

            services.AddSubscribeEvent<AccountCreatedEvent>(azureServiceBusConnectionString,
                accountCreatedEventSection["Topic"],
                accountCreatedEventSection["Subscription"]);
        }
    }
}