using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Queries
{
    /// <summary>
    /// Encapsulates the body of a get all by name request
    /// </summary>
    public class DTOEducationalInstitutionsByNameQuery : IRequest<Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        public string Name { get; init; }
        public int OffsetValue { get; init; }
        public int ResultsCount { get; init; }

        public DTOEducationalInstitutionsByNameQuery()
        {
        }

        public DTOEducationalInstitutionsByNameQuery(string name, int offsetValue, int resultsCount)
        {
            Name = name;
            OffsetValue = offsetValue;
            ResultsCount = resultsCount;
        }
    }
}