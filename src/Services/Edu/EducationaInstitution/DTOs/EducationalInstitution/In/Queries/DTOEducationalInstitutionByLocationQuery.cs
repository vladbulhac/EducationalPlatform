using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    /// <summary>
    /// Encapsulates the request parameters of a Get by LocationID operation
    /// </summary>
    public class DTOEducationalInstitutionByLocationQuery : IRequest<Response<GetEducationalInstitutionByLocationQueryResult>>
    {
        public string LocationID { get; init; }

        public DTOEducationalInstitutionByLocationQuery()
        {
        }

        public DTOEducationalInstitutionByLocationQuery(string locationID)
        {
            LocationID = locationID;
        }
    }
}