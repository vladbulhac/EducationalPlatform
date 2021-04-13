using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using MediatR;

namespace EducationalInstitutionAPI.DTOs.Queries
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