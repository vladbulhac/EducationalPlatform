using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOEducationalInstitutionByLocationQuery : IRequest<Response<GetAllEducationalInstitutionsByLocationQueryResult>>
    {
        public string LocationID { get; init; }
    }
}