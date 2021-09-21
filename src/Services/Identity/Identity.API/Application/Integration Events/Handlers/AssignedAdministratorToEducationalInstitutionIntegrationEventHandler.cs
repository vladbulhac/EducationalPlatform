using Identity.API.Application.Integration_Events.Events;
using Identity.API.Configuration.User_Permissions;
using Identity.API.Infrastructure.Repositories;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using RabbitMQEventBus.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.API.Application.Integration_Events.Handlers
{
    public class AssignedAdministratorToEducationalInstitutionIntegrationEventHandler : IIntegrationEventHandler<AssignedAdministratorToEducationalInstitutionIntegrationEvent>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityRepository identityRepository;

        public AssignedAdministratorToEducationalInstitutionIntegrationEventHandler(UserManager<User> userManager, IIdentityRepository identityRepository)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task HandleEvent(AssignedAdministratorToEducationalInstitutionIntegrationEvent @event)
        {
            var user = await userManager.FindByIdAsync(@event.UserId.ToString());
            if (user is null) throw new Exception($"Could not find any user with id:{@event.UserId}");

            var newAdministrator = new EducationalInstitutionAdministrator
            {
                User = user,
                UserId = user.Id,
                EducationalInstitutionId = @event.EducationalInstitutionId,
                CanRemoveEducationalInstitution = @event.Permissions.Any(p => p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.All) || p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.Delete)),
                CanUpdateEducationalInstitutionDetails = @event.Permissions.Any(p => p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.All) || p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.UpdateDetails)),
                CanChangeAdministrators = @event.Permissions.Any(p => p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.All) || p.Equals(DefinedUserPermissions.EducationalInstitutionPermissions.ChangeAdministrators))
            };

            await identityRepository.AddEducationalInstitutionAdministratorAsync(newAdministrator);

            await userManager.AddClaimsAsync(user, CreateClaimsFromPermissions(@event.Permissions));
        }

        private static IEnumerable<Claim> CreateClaimsFromPermissions(IEnumerable<string> permissions)
        {
            foreach (var permission in permissions)
                yield return new Claim(permission, "granted");
        }
    }
}