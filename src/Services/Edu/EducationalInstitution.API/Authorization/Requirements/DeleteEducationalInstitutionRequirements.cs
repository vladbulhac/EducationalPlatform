using Microsoft.AspNetCore.Authorization;

namespace EducationalInstitutionAPI.Authorization.Requirements;

public class DeleteEducationalInstitutionRequirements : IAuthorizationRequirement
{
    public ResourcePolicy Policy { get; init; }

    public DeleteEducationalInstitutionRequirements()
    {
        Policy = new DeleteEducationalInstitutionPolicy();
    }

    public DeleteEducationalInstitutionRequirements(ResourcePolicy policy)
    {
        Policy = policy;
    }
}