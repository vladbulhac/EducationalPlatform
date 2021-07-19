using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;

namespace EducationalInstitution.Application.Queries
{
    public class GetAllEducationalInstitutionsByNameQuery : IRequest<Response<GetAllEducationalInstitutionsByNameQueryResult>>
    {
        public string Name { get; init; }
        public int OffsetValue { get; init; }
        public int ResultsCount { get; init; }
    }
}