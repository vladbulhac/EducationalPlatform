using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using MediatR;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    public class DTOEducationalInstitutionByLocationQuery : IRequest<GetEducationalInstitutionByLocationQueryResult>
    {
        public string LocationID { get; init; }
    }
}