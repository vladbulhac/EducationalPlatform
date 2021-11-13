using Identity.API.Application.Integration_Events.Events;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using RabbitMQEventBus.IntegrationEvents;
using System.Security.Claims;

namespace Identity.API.Application.Integration_Events.Handlers;

public class AssignedAdminsToEducationalInstitutionIntegrationEventHandler : IIntegrationEventHandler<AssignedAdminsToEducationalInstitutionIntegrationEvent>
{
    private readonly UserManager<User> userManager;
    private readonly ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger;

    public AssignedAdminsToEducationalInstitutionIntegrationEventHandler(UserManager<User> userManager, ILogger<AssignedAdminsToEducationalInstitutionIntegrationEventHandler> logger)
    {
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task HandleEvent(AssignedAdminsToEducationalInstitutionIntegrationEvent @event)
    {
        logger.LogInformation($"Received integration event: {@event} !");

        foreach (var newAdmin in @event.NewAdmins)
        {
            var user = await userManager.FindByIdAsync(newAdmin.Identity);
            if (user is null) throw new Exception($"Could not find any user with id:{newAdmin.Identity}");

            await userManager.AddClaimsAsync(user, CreateClaimsFromPermissions(newAdmin.Permissions, @event.EducationalInstitutionId.ToString()));
            await userManager.UpdateSecurityStampAsync(user);
        }
    }

    private static IEnumerable<Claim> CreateClaimsFromPermissions(IEnumerable<string> permissions, string resourceId)
    {
        foreach (var permission in permissions)
            yield return new Claim(permission, resourceId);
    }
}