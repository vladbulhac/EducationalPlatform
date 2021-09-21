using Identity.API.Configuration.Client_Scopes;
using Identity.API.Configuration.Clients;
using Identity.API.Configuration.Resource_Servers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Infrastructure
{
    /// <summary>
    /// Saves to the database the clients configuration, the resource servers and the custom scopes
    /// </summary>
    public class DatabaseSeedService : IHostedService
    {
        private readonly IServiceProvider services;
        private readonly List<IScope> customScopes;
        private readonly IClients<OpenIddictApplicationDescriptor> clients;
        private readonly IResourceServers<OpenIddictApplicationDescriptor> resourceServers;

        public DatabaseSeedService(IServiceProvider services, List<IScope> customScopes, IClients<OpenIddictApplicationDescriptor> clients, IResourceServers<OpenIddictApplicationDescriptor> resourceServers)
        {
            this.clients = clients ?? throw new ArgumentNullException(nameof(clients));
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.customScopes = customScopes ?? throw new ArgumentNullException(nameof(customScopes));
            this.resourceServers = resourceServers ?? throw new ArgumentNullException(nameof(resourceServers));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var temporaryScope = services.CreateScope();

            var appManager = temporaryScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            var scopeManager = temporaryScope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();
            var context = temporaryScope.ServiceProvider.GetRequiredService<IdentityContext>();

            await context.Database.MigrateAsync();

            await CreateClientsAsync(appManager);
            await CreateResourceServersAsync(appManager);
            await CreateScopesAsync(scopeManager);
        }

        private async Task CreateClientsAsync(IOpenIddictApplicationManager appManager)
        {
            foreach (var client in clients.GetClients())
            {
                if (await appManager.FindByClientIdAsync(client.ClientId) is not null) continue;

                await appManager.CreateAsync(client);
            }
        }

        private async Task CreateResourceServersAsync(IOpenIddictApplicationManager appManager)
        {
            foreach (var resourceServer in resourceServers.GetResourceServers())
            {
                if (await appManager.FindByClientIdAsync(resourceServer.ClientId) is not null) continue;

                await appManager.CreateAsync(resourceServer);
            }
        }

        private async Task CreateScopesAsync(IOpenIddictScopeManager scopeManager)
        {
            foreach (var scope in customScopes)
                await scope.RegisterScopes(scopeManager);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}