using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOAdminsByEducationalInstitutionIDQuery : IRequest<Response<GetAllAdminsOfEducationalInstitutionQueryResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}