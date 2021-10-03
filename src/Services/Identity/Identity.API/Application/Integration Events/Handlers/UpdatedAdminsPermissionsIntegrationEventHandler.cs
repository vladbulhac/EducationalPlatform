using Identity.API.Application.Integration_Events.Events;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Application.Integration_Events.Handlers
{
    public class UpdatedAdminsPermissionsIntegrationEventHandler : IIntegrationEventHandler<UpdatedAdminsPermissionsIntegrationEvent>
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<UpdatedAdminsPermissionsIntegrationEventHandler> logger;

        public UpdatedAdminsPermissionsIntegrationEventHandler(UserManager<User> userManager, ILogger<UpdatedAdminsPermissionsIntegrationEventHandler> logger)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleEvent(UpdatedAdminsPermissionsIntegrationEvent @event)
        {
            logger.LogInformation($"Received integration event: {@event} !");

            foreach (var admin in @event.UpdatedAdmins)
            {
                var user = await userManager.FindByIdAsync(admin.Identity);

                if (@event.TriggeredBy.Action.Equals("Add"))
                    await userManager.AddClaimsAsync(user, CreateClaimsFromPermissions(admin.Permissions, @event.EducationalInstitutionId.ToString()));
                else
                    await userManager.RemoveClaimsAsync(user, CreateClaimsFromPermissions(admin.Permissions, @event.EducationalInstitutionId.ToString()));

                await userManager.UpdateSecurityStampAsync(user);
            }
        }

        private static IEnumerable<Claim> CreateClaimsFromPermissions(IEnumerable<string> permissions, string resourceId)
        {
            foreach (var permission in permissions)
                yield return new Claim(permission, resourceId);
        }
    }
}