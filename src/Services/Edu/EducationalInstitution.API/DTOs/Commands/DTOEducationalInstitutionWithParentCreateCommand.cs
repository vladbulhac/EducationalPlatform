using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    /// <summary>
    /// Encapsulates the body of a create request
    /// </summary>
    public class DTOEducationalInstitutionWithParentCreateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
        public Guid ParentInstitutionID { get; init; }

        public DTOEducationalInstitutionWithParentCreateCommand()
        {
        }

        public DTOEducationalInstitutionWithParentCreateCommand(string name, string description, string locationID, ICollection<string> buildingsIDs, Guid parentInstitutionID)
        {
            Name = name;
            Description = description;
            LocationID = locationID;
            BuildingsIDs = buildingsIDs;
            ParentInstitutionID = parentInstitutionID;
        }
    }
}