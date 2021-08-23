using DatabaseTasks.ApplyMigrations_Task;
using DatabaseTasks.Cleanup_Task;
using DataValidation;
using DataValidation.Abstractions;
using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands.Validators;
using EducationalInstitution.Infrastructure;
using EducationalInstitution.Infrastructure.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitution.Infrastructure.Unit_of_Work.Query_Unit_of_Work;
using EducationalInstitutionAPI.Presentation.Grpc;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using Serilog;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc(options => options.EnableDetailedErrors = true);

            var writesDBConnectionString = Configuration.GetConnectionString("WritesDB");
            var readsDBConnectionString = Configuration.GetConnectionString("ReadsDB");

            services.AddDatabasesContexts(writesDBConnectionString);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddApplicationServices(Configuration, writesDBConnectionString, readsDBConnectionString);

            services.AddMediatR(typeof(HandlerBase<>).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app//.UseHttpsRedirection() commented in order to use the http url to make grpc calls when using docker
               .UseStaticFiles()
               .UseRouting()
               .UseSerilogRequestLogging()
               .AddEventBusSubscriptions()
               .UseEndpoints(endpoints => endpoints.AddGrpcServices());
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, string writesDBConnectionString, string readsDBConnectionString)
        {
            services.AddEventBus(configuration)
                    .AddDatabaseCleanupHostedService(writesDBConnectionString)
                    .AddSingleton(_ => new ValidatorFactory(typeof(CreateEducationalInstitutionCommandValidator).Assembly))
                    .AddTransient<IValidationHandler, ValidationHandler>()
                    .AddTransient<IUnitOfWorkForQueries>(_ => new UnitOfWorkForQueries(readsDBConnectionString))
                    .AddTransient<IUnitOfWorkForCommands, UnitOfWorkForCommands>()
                    .AddApplyEntityFrameworkMigrationsHostedService(readsDBConnectionString);

            return services;
        }

        public static IServiceCollection AddDatabaseCleanupHostedService(this IServiceCollection services, string connectionString)
        {
            return services.AddHostedService(serviceProvider =>
                            {
                                var logger = serviceProvider.GetRequiredService<ILogger<DatabaseCleanup>>();

                                return new DatabaseCleanup(connectionString, retryInHours: 12, logger);
                            });
        }

        public static IServiceCollection AddApplyEntityFrameworkMigrationsHostedService(this IServiceCollection services, string connectionString)
        {
            return services.AddHostedService(serviceProvider =>
                            {
                                var contexts = new List<DataContext>();

                                var context = serviceProvider.CreateScope()
                                                             .ServiceProvider
                                                             .GetRequiredService<DataContext>();
                                contexts.Add(context);

                                //create new DataContext with the connection string to the read database
                                var dbOptions = new DbContextOptionsBuilder<DataContext>()
                                                    .UseSqlServer(connectionString, providerOptions =>
                                                    {
                                                        providerOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                                                                             maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                                             errorNumbersToAdd: null);

                                                        providerOptions.MigrationsAssembly("EducationalInstitution.Infrastructure");
                                                    });

                                contexts.Add(new DataContext(dbOptions.Options));

                                var logger = serviceProvider.GetRequiredService<ILogger<ApplyEFMigrationsToSQLServerDatabase>>();
                                return new ApplyEFMigrationsToSQLServerDatabase(contexts, logger);
                            });
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPersistentConnectionHandler>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<PersistentConnectionHandler>>();

                var factory = new ConnectionFactory
                {
                    HostName = configuration.GetSection("EventBus")["HostName"],
                    DispatchConsumersAsync = true,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(30)
                };

                return new PersistentConnectionHandler(logger, factory);
            })
            .AddSingleton<IEventBus, EventBus>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<EventBus>>();
                var queueName = configuration.GetSection("EventBus")["QueueName"];
                var connectionHandler = serviceProvider.GetRequiredService<IPersistentConnectionHandler>();

                return new(queueName, logger, connectionHandler, services);
            });

            return services;
        }

        public static IEndpointRouteBuilder AddGrpcServices(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGrpcService<EducationalInstitutionCommandService>();
            endpoints.MapGrpcService<EducationalInstitutionQueryService>();

            return endpoints;
        }

        public static IServiceCollection AddDatabasesContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
                    {
                        options.UseSqlServer(connectionString,
                                             providerOptions =>
                                             {
                                                 providerOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                                                                      maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                                      errorNumbersToAdd: null);
                                                 providerOptions.MigrationsAssembly("EducationalInstitution.Infrastructure");
                                             });

                        //options.LogTo(Console.WriteLine);
                    }, ServiceLifetime.Scoped);

            return services;
        }

        public static IApplicationBuilder AddEventBusSubscriptions(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<IEventBus>();

            return app;
        }
    }
}