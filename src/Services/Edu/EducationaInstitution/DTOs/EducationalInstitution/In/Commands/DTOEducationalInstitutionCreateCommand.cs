using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.Utils;
using MediatR;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands
{
    /// <summary>
    /// Encapsulates the request body of a Create operation 
    /// </summary>
    public class DTOEducationalInstitutionCreateCommand : IRequest<Response<CreateEducationalInstitutionCommandResult>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public string BuildingID { get; init; }

        public DTOEducationalInstitutionCreateCommand()
        {
        }

        public DTOEducationalInstitutionCreateCommand(string name, string description, string locationID, string buildingID)
        {
            Name = name;
            Description = description;
            LocationID = locationID;
            BuildingID = buildingID;
        }
    }
}