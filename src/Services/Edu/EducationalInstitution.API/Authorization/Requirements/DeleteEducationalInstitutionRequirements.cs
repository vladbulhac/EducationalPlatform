using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalInstitutionAPI.Authorization.Requirements
{
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
}