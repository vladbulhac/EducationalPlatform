using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notification.API.Hubs;
using Notification.API.Hubs.Management;
using Notification.Application.Integration_Events.Events;
using Notification.Application.Integration_Events.Handlers;
using Notification.Application.Services;
using Notification.Infrastructure;
using Notification.Infrastructure.Repositories;
using OpenIddict.Validation.AspNetCore;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using Serilog;

namespace Notification.API;

public class Startup
{
    private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<NotificationContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("ConnectionToNotificationDB"),
                                 providerOptions =>
                                 {
                                     providerOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     providerOptions.MigrationsAssembly("Notification.Infrastructure");
                                 });
            //options.LogTo(Console.WriteLine);
        });

        services.AddOpenIDDictConfiguration(Configuration);

        services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        services.AddTransient<INotificationRepository, NotificationRepository>();
        services.AddTransient<INotificationService, NotificationService>();

        services.AddSingleton<IUserIdProvider, ResourceOwnerIdentification>();
        services.AddSignalR(options => { options.EnableDetailedErrors = true; });

        services.AddMediatR(typeof(Startup), typeof(AssignedAdminsToEducationalInstitutionIntegrationEventHandler));

        services.AddEventBusConfiguration(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseHttpsRedirection();

        app.UseCors(MyAllowSpecificOrigins);

        app.UseRouting();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSerilogRequestLogging();

        app.UseAuthentication()
           .UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<NotificationHub>("/notifications");
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
                DispatchConsumersAsync = true,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(30)
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

        services.AddTransient<AssignedAdminsToEducationalInstitutionIntegrationEventHandler>();
        services.AddTransient<NotificationEventHandler<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent>>();
        services.AddTransient<NotificationEventHandler<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent>>();
        services.AddTransient<NotificationEventHandler<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent>>();

        return services;
    }

    public static void AddEventBusSubscriptions(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        eventBus.Subscribe<AssignedAdminsToEducationalInstitutionIntegrationEvent,
                           AssignedAdminsToEducationalInstitutionIntegrationEventHandler>();

        eventBus.Subscribe<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent,
                           NotificationEventHandler<NotifyAdminsOfEducationalInstitutionScheduledForDeletionIntegrationEvent>>();

        eventBus.Subscribe<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent,
                           NotificationEventHandler<NotifyAdminsOfNewEducationalInstitutionChildIntegrationEvent>>();

        eventBus.Subscribe<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent,
                           NotificationEventHandler<NotifyAdminsOfEducationalInstitutionUpdateIntegrationEvent>>();
    }

    public static IServiceCollection AddOpenIDDictConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    options.SetIssuer(configuration.GetSection("Identity")["Issuer"]);
                    options.AddAudiences("notification_api");

                    // Register the encryption credentials, a symmetric
                    // encryption key that is shared between the server and all the resource servers
                    // (that performs local token validation instead of using introspection).
                    //
                    // Note: in a real world application, this encryption key should be
                    // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
                    options.AddEncryptionKey(new SymmetricSecurityKey(
                            Convert.FromBase64String(configuration.GetSection("Identity")["SharedKey"])));

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

        return services;
    }
}