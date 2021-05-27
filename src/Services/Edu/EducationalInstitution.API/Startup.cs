using DatabaseCleanerService;
using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Unit_of_Work.Command_Unit_of_Work;
using EducationalInstitutionAPI.Unit_of_Work.Query_Unit_of_Work;
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

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionToWriteDB"), providerOptions => providerOptions.EnableRetryOnFailure(1));
            });

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

            services.RegisterProjectServices(Configuration);

            services.AddMediatR(typeof(Startup));
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

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseSerilogRequestLogging()
               .UseAuthorization()
               .UseEndpoints(endpoints => endpoints.RegisterGrpcServices());
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEventBus(configuration)
                    .AddTransient<IValidationHandler, ValidationHandler>()
                    .AddTransient<IUnitOfWorkForQueries, UnitOfWorkForQueries>()
                    .AddTransient<IUnitOfWorkForCommands, UnitOfWorkForCommands>()
                    .AddHostedService(serviceProvider =>
                    {
                        var connectionString = configuration.GetSection("ConnectionStrings")["ConnectionToWriteDB"];
                        var logger = serviceProvider.GetRequiredService<ILogger<Worker>>();

                        return new Worker(connectionString, retryInHours: 12, logger);
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
                    DispatchConsumersAsync = true
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
    }
}