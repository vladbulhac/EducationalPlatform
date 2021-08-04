using DatabaseCleanerService;
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
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using Serilog;
using System;

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

            services.AddControllers();

            services.AddDatabasesContexts(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Educational Institution API", Version = "v1" });
            });

            services.RegisterApplicationServices(Configuration);

            services.AddMediatR(typeof(HandlerBase<>).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseSwagger()
                   .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Educational Institution API v1"));
            }

            app//.UseHttpsRedirection() commented in order to use the http url to make grpc calls when using docker
               .UseStaticFiles()
               .UseRouting()
               .UseSerilogRequestLogging()
               .UseAuthorization()
               .ApplyMigrationsOnStartup(Configuration)
               .UseEndpoints(endpoints => endpoints.RegisterGrpcServices());
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetSection("ConnectionStrings")["ReadsDB"];

            services.AddEventBus(configuration)
                    .AddSingleton(_ => new ValidatorFactory(typeof(CreateEducationalInstitutionCommandValidator).Assembly))
                    .AddTransient<IValidationHandler, ValidationHandler>()
                    .AddTransient<IUnitOfWorkForQueries>(_ => new UnitOfWorkForQueries(dbConnectionString))
                    .AddTransient<IUnitOfWorkForCommands, UnitOfWorkForCommands>()
                    .AddHostedService(serviceProvider =>
                    {
                        var writesConnectionString = configuration.GetSection("ConnectionStrings")["WritesDB"];
                        var logger = serviceProvider.GetRequiredService<ILogger<Worker>>();

                        return new Worker(writesConnectionString, retryInHours: 12, logger);
                    });

            return services;
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

        public static IEndpointRouteBuilder RegisterGrpcServices(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGrpcService<EducationalInstitutionCommandService>();
            endpoints.MapGrpcService<EducationalInstitutionQueryService>();

            return endpoints;
        }

        public static IServiceCollection AddDatabasesContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("WritesDB"),
                                             providerOptions =>
                                             {
                                                 providerOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                                                                      maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                                      errorNumbersToAdd: null);
                                                 providerOptions.MigrationsAssembly("EducationalInstitution.Infrastructure");
                                             });

                        options.LogTo(Console.WriteLine);
                    }, ServiceLifetime.Scoped);

            return services;
        }

        public static IApplicationBuilder ApplyMigrationsOnStartup(this IApplicationBuilder app, IConfiguration configuration)
        {
            ApplyMigrationsOnWriteDatabases(app);

            ApplyMigrationsOnReadDatabases(configuration);

            return app;
        }

        private static void ApplyMigrationsOnWriteDatabases(IApplicationBuilder app)
        {
            using var writesContext = app.ApplicationServices.CreateScope()
                                                             .ServiceProvider
                                                             .GetRequiredService<DataContext>();
            writesContext.Database.Migrate();
        }

        private static void ApplyMigrationsOnReadDatabases(IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetSection("ConnectionStrings")["ReadsDB"];

            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                                .UseSqlServer(dbConnectionString, providerOptions =>
                                {
                                    providerOptions.EnableRetryOnFailure(maxRetryCount: 10,
                                                                         maxRetryDelay: TimeSpan.FromSeconds(30),
                                                                         errorNumbersToAdd: null);

                                    providerOptions.MigrationsAssembly("EducationalInstitution.Infrastructure");
                                });

            using var readsContext = new DataContext(dbOptions.Options);

            readsContext.Database.Migrate();
        }
    }
}