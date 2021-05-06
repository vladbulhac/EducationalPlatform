using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOEducationalInstitutionsByNameQuery : IRequest<Response<GetAllEducationalInstitutionsByNameQueryResult>>
    {
        public string Name { get; init; }
        public int OffsetValue { get; init; }
        public int ResultsCount { get; init; }
    }
}