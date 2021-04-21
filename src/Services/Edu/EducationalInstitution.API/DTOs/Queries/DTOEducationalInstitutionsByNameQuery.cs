using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    public class DTOEducationalInstitutionsByNameQuery : IRequest<Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        public string Name { get; init; }
        public int OffsetValue { get; init; }
        public int ResultsCount { get; init; }
    }
}