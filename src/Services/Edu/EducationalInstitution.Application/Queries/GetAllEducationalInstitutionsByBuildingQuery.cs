using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;

namespace EducationalInstitution.Application.Queries
{
    public class GetAllEducationalInstitutionsByBuildingQuery : IRequest<Response<GetAllEducationalInstitutionsWithSameBuildingQueryResult>>
    {
        public string BuildingID { get; init; }
    }
}