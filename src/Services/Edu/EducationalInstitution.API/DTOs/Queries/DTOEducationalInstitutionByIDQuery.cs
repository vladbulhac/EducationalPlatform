using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    /// <summary>
    /// Encapsulates the parameter of a get by id request
    /// </summary>
    public class DTOEducationalInstitutionByIDQuery : IRequest<Response<GetEducationalInstitutionByIDQueryResult>>
    {
        public Guid EduInstitutionID { get; init; }

        public DTOEducationalInstitutionByIDQuery()
        {
        }

        public DTOEducationalInstitutionByIDQuery(Guid eduInstitutionID)
        {
            EduInstitutionID = eduInstitutionID;
        }
    }
}