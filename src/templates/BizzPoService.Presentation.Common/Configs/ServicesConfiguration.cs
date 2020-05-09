using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Messaging.MediatR;
using BizzPoService.Application.Commands.CreateContact;
using BizzPoService.Domain.Contacts;
using BizzPoService.Domain.Contacts.Seedwork;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPoService.Presentation.Common.Configs
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(CreateContactCommandHandler).Assembly);

            services.AddTransient<IDomainEventsService, MediatrDomainEventsService>();
            services.AddTransient<IContactService, ContactService>();

            services.AddInMemoryPublishEvents();
            // OR services.AddDbPublishEvents(configuration);
            // OR services.AddAzurePublishEvents(Configuration);

            services.AddInMemorySubscribeEvents();
            // OR services.AddDbSubscribeEvents(configuration);
            // OR services.AddAzureSubscribeEvents(Configuration);

            services.AddInMemoryDatabase(); 
            // OR services.AddSqlDatabase(configuration);

        }
    }
}