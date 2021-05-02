using EducationalInstitutionAPI.Business.Validation_Handler;
using EducationalInstitutionAPI.Data.Contexts;
using EducationalInstitutionAPI.Grpc;
using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using EducationalInstitutionAPI.Unit_of_Work;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            services.AddTransient<IValidationHandler, ValidationHandler>();
            services.AddTransient<IEducationalInstitutionRepository, EducationalInstitutionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

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

            app.UseSwagger();

            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Educational Institution API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<EducationalInstitutionCommandService>();
                endpoints.MapControllers();
            });
        }
    }
}