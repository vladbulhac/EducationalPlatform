using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOEducationalInstitutionsByBuildingQuery : IRequest<Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>>
    {
        public string BuildingID { get; init; }
    }
}