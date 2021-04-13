using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Queries
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