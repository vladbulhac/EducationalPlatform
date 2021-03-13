using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries
{
    public class DTOEducationalInstitutionsFromCollectionOfIDsQuery : IRequest<Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        public ICollection<Guid> EducationalInstitutionsIDs { get; init; }
    }
}