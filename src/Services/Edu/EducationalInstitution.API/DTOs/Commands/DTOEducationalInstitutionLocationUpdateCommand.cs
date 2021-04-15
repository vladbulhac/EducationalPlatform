using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    /// <summary>
    /// Encapsulates the body of an update request
    /// </summary>
    public class DTOEducationalInstitutionLocationUpdateCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }

        public bool UpdateLocation { get; init; }
        public string LocationID { get; init; }

        public bool UpdateBuildings { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }

        public DTOEducationalInstitutionLocationUpdateCommand()
        {
        }

        public DTOEducationalInstitutionLocationUpdateCommand(Guid educationalInstitutionID, string newLocationID, ICollection<string> newBuildingsIDs)
        {
            EducationalInstitutionID = educationalInstitutionID;
            LocationID = newLocationID;
            BuildingsIDs = newBuildingsIDs;
        }
    }
}