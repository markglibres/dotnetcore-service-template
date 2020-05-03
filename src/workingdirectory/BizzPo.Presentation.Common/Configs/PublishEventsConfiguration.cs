using BizzPo.Application.Integration.Publish;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class PublishEventsConfiguration
    {
        public static void AddPublishEvents(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var azureServiceBusConnectionString = connectionStringSection["AzureServiceBus"];
            var contactEventTopic = configuration.GetValue<string>("Events:Publish:ContactAddedEvent:Topic");

            services.AddPublishEvent<ContactAddedEvent>(azureServiceBusConnectionString, contactEventTopic);
        }
    }
}