using BizzPo.Application.Commands.CreateContact;
using BizzPo.Domain.Contacts;
using BizzPo.Domain.Contacts.Seedwork;
using BizzPo.Domain.Seedwork;
using BizzPo.Infrastructure.DomainEvents;
using BizzPo.Infrastructure.Repositories;
using BizzPo.Presentation.Common.Configs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BizzPo.Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPublishEvents(Configuration);

            services.AddControllers();
            services.AddHealthChecks();
            services.AddMediatR(typeof(CreateContactCommandHandler).Assembly);

            services.AddTransient<IDomainEventsService, MediatrEventsService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, InMemoryContactRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("health");
            });
        }
    }
}