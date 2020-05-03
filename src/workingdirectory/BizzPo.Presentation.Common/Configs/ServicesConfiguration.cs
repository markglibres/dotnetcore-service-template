using BizzPo.Application.Commands.CreateContact;
using BizzPo.Core.Domain;
using BizzPo.Core.Infrastructure.Messaging.MediatR;
using BizzPo.Core.Infrastructure.Repository.InMemory;
using BizzPo.Domain.Contacts;
using BizzPo.Domain.Contacts.Seedwork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BizzPo.Presentation.Common.Configs
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateContactCommandHandler).Assembly);

            services.AddTransient<IDomainEventsService, MediatrDomainEventsService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IRepository<Contact>, InMemoryRepository<Contact>>();
        }
    }
}