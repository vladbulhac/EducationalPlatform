using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    public class DTOEducationalInstitutionsByNameQuery : IRequest<Response<ICollection<GetEducationalInstitutionQueryResult>>>
    {
        public string Name { get; init; }
        public int OffsetValue { get; init; }
        public int ResultsCount { get; init; }
    }
}