using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Unit_of_Work;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });

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

            services.RegisterProjectServices();

            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Educational Institution API v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.RegisterGrpcServices());
        }
    }

    public static class StartupExtensionMethods
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services)
        {
            services.AddTransient<IValidationHandler, ValidationHandler>();
            //services.AddTransient<IEducationalInstitutionRepository, EducationalInstitutionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

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