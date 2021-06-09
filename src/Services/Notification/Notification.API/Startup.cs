using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Notification.API.Data;
using Notification.API.IntegrationEvents.Events;
using Notification.API.IntegrationEvents.Handlers;
using Notification.API.Repositories;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using Serilog;

namespace Notification.API
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
            services.AddDbContext<NotificationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionToNotificationDB"), providerOptions => providerOptions.EnableRetryOnFailure(1));
            });

            services.AddTransient<INotificationRepository, NotificationRepository>();

            services.AddEventBusConfiguration(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Notification.API", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.AddEventBusSubscriptions();
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddEventBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPersistentConnectionHandler>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<PersistentConnectionHandler>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration.GetSection("EventBus")["HostName"],
                    DispatchConsumersAsync = true
                };

                return new PersistentConnectionHandler(logger, factory);
            });

            services.AddSingleton<IEventBus, EventBus>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<EventBus>>();
                var queueName = configuration.GetSection("EventBus")["QueueName"];
                var connectionHandler = serviceProvider.GetRequiredService<IPersistentConnectionHandler>();

                return new(queueName, logger, connectionHandler, services);
            });

            services.AddTransient<NotificationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>>();
            services.AddTransient<NotificationEventHandler<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent>>();
            services.AddTransient<NotificationEventHandler<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent>>();
            services.AddTransient<NotificationEventHandler<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent>>();

            return services;
        }

        public static void AddEventBusSubscriptions(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<AssignedAdminsToEducationalInstitutionIntegrationEvent,
                               NotificationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>>();

            eventBus.Subscribe<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent,
                               NotificationEventHandler<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent>>();

            eventBus.Subscribe<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent,
                               NotificationEventHandler<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent>>();

            eventBus.Subscribe<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent,
                               NotificationEventHandler<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent>>();
        }
    }
}