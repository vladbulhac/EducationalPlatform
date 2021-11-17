using Microsoft.AspNetCore.Authorization;

namespace EducationalInstitutionAPI.Authorization.Requirements
{
    public abstract class RequirementBase : IAuthorizationRequirement
    {
        public ResourcePolicy Policy { get; init; }

        public RequirementBase(ResourcePolicy policy) => Policy = policy ?? throw new ArgumentNullException(nameof(policy));
    }
}