using Identity.API.Application.Integration_Events.Events;
using Identity.API.Application.Integration_Events.Handlers;
using Identity.API.Application.Services;
using Identity.API.Configuration.Client_Scopes;
using Identity.API.Configuration.Clients;
using Identity.API.Configuration.Resource_Servers;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using RabbitMQEventBus;
using RabbitMQEventBus.Abstractions;
using RabbitMQEventBus.ConnectionHandler;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.API;

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
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("IdentityDB"));
            options.UseOpenIddict();
        });

        services.AddCors(options =>
        {
            options.AddPolicy("IdentityAPIPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowCredentials()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        services.AddDefaultIdentity<User>()//options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

        // Configure Identity to use the same JWT claims as OpenIddict instead
        // of the legacy WS-Federation claims it uses by default (ClaimTypes),
        // which saves you from doing the mapping in your authorization controller.
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserNameClaimType = Claims.Name;
            options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
            options.ClaimsIdentity.RoleClaimType = Claims.Role;

            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                           .UseDbContext<IdentityContext>();
                })
                .AddServer(options =>
                {
                    //when using docker compose the Aggregator can access the Identity API using http://identity-api:41110/ as Issuer
                    //however when a new token is created, OpenIddict sets the Issuer automatically to http://localhost:41110/
                    //and the validation fails because the Issuers do not match
                    //so explicitly configuring OpenIddict to set the Issuer as http://identity-api:41110 solves this issue
                    options.SetIssuer(new Uri(Configuration.GetSection("Identity")["Issuer"]));

                    options.SetAuthorizationEndpointUris("/connect/authorize")
                           .SetLogoutEndpointUris("/connect/logout")
                           .SetTokenEndpointUris("/connect/token")
                           .SetUserinfoEndpointUris("/connect/userinfo")
                           .SetIntrospectionEndpointUris("/connect/introspect"); // introspection endpoint validates an access token

                    options.RegisterScopes(Scopes.Email,
                                       Scopes.Profile,
                                       Scopes.Roles,
                                       DefinedScopes.EducationalInstitutionScopes.All,
                                       DefinedScopes.EducationalInstitutionScopes.ChangeAdministrators,
                                       DefinedScopes.EducationalInstitutionScopes.Delete,
                                       DefinedScopes.EducationalInstitutionScopes.UpdateDetails,
                                       DefinedScopes.NotificationScopes.All,
                                       DefinedScopes.NotificationScopes.Receive,
                                       DefinedScopes.NotificationScopes.Delete);

                    options.AllowAuthorizationCodeFlow()
                           //.RequireProofKeyForCodeExchange() globally enforces PKCE
                           .AllowRefreshTokenFlow();

                    // Register the encryption credentials, a symmetric
                    // encryption key that is shared between the server and all the resource servers
                    // (that performs local token validation instead of using introspection).
                    //
                    // Note: in a real world application, this encryption key should be
                    // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
                    options.AddEncryptionKey(new SymmetricSecurityKey(
                    Convert.FromBase64String(Configuration.GetSection("Identity")["SharedKey"])));

                    options.AddDevelopmentEncryptionCertificate()
                           .AddDevelopmentSigningCertificate();

                    options.UseAspNetCore()
                           .DisableTransportSecurityRequirement() //disables the enforcement of https
                           .EnableAuthorizationEndpointPassthrough()
                           .EnableLogoutEndpointPassthrough()
                           .EnableStatusCodePagesIntegration()
                           .EnableTokenEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            // enables immediate logout, after updating the user's information.
            options.ValidationInterval = TimeSpan.FromMinutes(0);
        });

        services.AddEventBusConfiguration(Configuration);

        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddTransient<IIdentityService<User>, IdentityService>();

        services.AddHostedService<DatabaseSeedService>(serviceProvider =>
        {
            var customScopes = new List<IScope>() {
                    new DefinedScopes.EducationalInstitutionScopes(),
                    new DefinedScopes.NotificationScopes(),
            };

            return new(serviceProvider, customScopes, new Clients(), new ResourceServers());
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            // app.UseHsts();
        }

        //app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
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

        return services;
    }

    public static void AddEventBusSubscriptions(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

        eventBus.Subscribe<AssignedAdminsToEducationalInstitutionIntegrationEvent, AssignedAdminsToEducationalInstitutionIntegrationEventHandler>();
    }
}