using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Services.EducationalInstitution;
using Aggregator.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Net.Http;

namespace Aggregator
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
            services.AddControllers();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator", Version = "v1" }));

            services.AddScoped<IEducationalInstitutionCommandService, EducationalInstitutionCommandService>()
                    .AddScoped<IEducationalInstitutionQueryService, EducationalInstitutionQueryService>();

            services.AddGrpcClients(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
        {
            var httpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
            };

            var eduUri = new Uri(configuration.GetSection("ServicesUrls")["gRPCEdu"]);

            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddGrpcClient<Query.QueryClient>(options => options.Address = eduUri)
                    .ConfigureChannel(ch => ch.HttpHandler = httpHandler)
                    .AddInterceptor<GrpcExceptionInterceptor>();

            services.AddGrpcClient<Command.CommandClient>(options => options.Address = eduUri)
                    .ConfigureChannel(ch => ch.HttpHandler = httpHandler)
                    .AddInterceptor<GrpcExceptionInterceptor>();

            return services;
        }
    }
}