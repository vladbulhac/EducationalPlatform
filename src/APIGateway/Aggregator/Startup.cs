using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Services.EducationalInstitution;
using Aggregator.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

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

            services.AddScoped<IEducationalInstitutionCommandService, EducationalInstitutionCommandService>();
            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddGrpcClient<Command.CommandClient>(options => options.Address = new Uri("https://localhost:53001"))
                    .AddInterceptor<GrpcExceptionInterceptor>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}