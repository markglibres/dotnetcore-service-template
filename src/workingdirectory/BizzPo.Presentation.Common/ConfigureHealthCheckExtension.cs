using BizzPo.Application.CreateContact;
using BizzPo.Application.Seedwork;
using BizzPo.Domain.Contacts;
using BizzPo.Domain.Contacts.Seedwork;
using BizzPo.Domain.Seedwork;
using BizzPo.Infrastructure.EventBus;
using BizzPo.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common
{
    public static class ConfigureHealthCheckExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.ConfigureHealthCheck();
            services.AddMediatR(typeof(CreateContactCommandHandler).Assembly);

            services.AddTransient<IDomainEventsService, MediatrEventsService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, InMemoryContactRepository>();
            services.AddTransient<IIntegrationEventPublisherService, AzureServiceBusPublisherService>();

            return services;
        }

        public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
    }
}