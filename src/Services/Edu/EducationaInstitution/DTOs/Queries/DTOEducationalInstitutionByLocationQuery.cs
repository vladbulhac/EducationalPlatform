using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    /// <summary>
    /// Encapsulates the parameter of a get by locationid request
    /// </summary>
    public class DTOEducationalInstitutionByLocationQuery : IRequest<Response<GetAllEducationalInstitutionsByLocationQueryResult>>
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