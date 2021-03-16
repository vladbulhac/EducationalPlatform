using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    public class DTOEducationalInstitutionByLocationQuery : IRequest<Response<GetEducationalInstitutionByLocationQueryResult>>
    {
        public string LocationID { get; init; }

        public DTOEducationalInstitutionByLocationQuery() { }
        public DTOEducationalInstitutionByLocationQuery(string locationID)
        {
            LocationID = locationID;
        }
    }
}