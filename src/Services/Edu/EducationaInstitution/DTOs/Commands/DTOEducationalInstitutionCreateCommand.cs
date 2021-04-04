using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands
{
    /// <summary>
    /// Encapsulates the body of a create <see cref="EduInstitution>"/> request
    /// </summary>
    public class DTOEducationalInstitutionCreateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }

        public DTOEducationalInstitutionCreateCommand()
        {
        }

        public DTOEducationalInstitutionCreateCommand(string name, string description, string locationID, ICollection<string> buildingsIDs)
        {
            Name = name;
            Description = description;
            LocationID = locationID;
            BuildingsIDs = buildingsIDs;
        }
    }
}