using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOEducationalInstitutionByIDQuery : IRequest<Response<GetEducationalInstitutionByIDQueryResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}