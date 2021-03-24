using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    /// <summary>
    /// Encapsulates the request parameters of a Get by Name operation
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