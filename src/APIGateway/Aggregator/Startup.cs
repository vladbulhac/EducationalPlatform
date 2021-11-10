using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Infrastructure;
using Aggregator.Services.EducationalInstitution;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Serilog;

namespace Aggregator;

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
        services.AddOpenIDDictConfiguration(Configuration);
        services.AddAuthorizationConfiguration();

        services.AddControllers();

        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aggregator", Version = "v1" }));

        services.AddScoped<IEducationalInstitutionCommandService, EducationalInstitutionCommandService>()
                .AddScoped<IEducationalInstitutionQueryService, EducationalInstitutionQueryService>()
                .AddHttpContextAccessor();

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

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseSerilogRequestLogging();
        app.UseHttpLogging();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}

public static class StartupExtensionMethods
{
    public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CreateEducationalInstitutionPolicy", policy => policy.RequireClaim("oi_scp", "client.educational_institution.all"));
            options.AddPolicy("DeleteEducationalInstitutionPolicy", policy => policy.RequireClaim("oi_scp", "client.educational_institution.delete", "client.educational_institution.all"));
        });

        return services;
    }

    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var httpHandler = new SocketsHttpHandler
        {
            EnableMultipleHttp2Connections = true,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
        };

        var eduUri = new Uri(configuration.GetSection("ServicesUrls")["gRPCEdu"]);

        services.AddTransient<GrpcInterceptor>();

        services.AddGrpcClient<Query.QueryClient>(options => options.Address = eduUri)
                .ConfigureChannel(ch => ch.HttpHandler = httpHandler)
                .AddInterceptor<GrpcInterceptor>();

        services.AddGrpcClient<Command.CommandClient>(options => options.Address = eduUri)
                .ConfigureChannel(ch => ch.HttpHandler = httpHandler)
                .AddInterceptor<GrpcInterceptor>();

        return services;
    }

    public static IServiceCollection AddOpenIDDictConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options => options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        var aggregator = configuration.GetSection("Identity")["name"];

        services.AddOpenIddict()
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the address of the introspection endpoint.
                    options.SetIssuer(configuration.GetSection("ServicesUrls")["identity"]);
                    options.AddAudiences(aggregator);

                    // Configure the validation handler to use introspection and register the client
                    // credentials used when communicating with the remote introspection endpoint.
                    options.UseIntrospection()
                       .SetClientId(aggregator)
                       .SetClientSecret(configuration.GetSection("Identity")["secret"]);

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

        return services;
    }
}