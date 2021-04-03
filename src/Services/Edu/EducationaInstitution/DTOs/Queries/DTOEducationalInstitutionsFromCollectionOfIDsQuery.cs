using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Queries
{
    /// <summary>
    /// Encapsulates the body of a get by collection of ids request
    /// </summary>
    public class DTOEducationalInstitutionsFromCollectionOfIDsQuery : IRequest<Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        public ICollection<Guid> EducationalInstitutionsIDs { get; init; }

        public DTOEducationalInstitutionsFromCollectionOfIDsQuery()
        {
        }

        public DTOEducationalInstitutionsFromCollectionOfIDsQuery(ICollection<Guid> educationalInstitutionsIDs)
        {
            EducationalInstitutionsIDs = educationalInstitutionsIDs;
        }
    }
}