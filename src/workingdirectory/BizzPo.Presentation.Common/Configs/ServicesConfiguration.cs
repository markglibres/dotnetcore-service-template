using BizzPo.Application.Commands.CreateContact;
using BizzPo.Domain.Contacts;
using BizzPo.Domain.Contacts.Seedwork;
using BizzPo.Domain.Seedwork;
using BizzPo.Infrastructure.DomainEvents;
using BizzPo.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateContactCommandHandler).Assembly);

            services.AddTransient<IDomainEventsService, MediatrEventsService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, InMemoryContactRepository>();
        }
    }
}