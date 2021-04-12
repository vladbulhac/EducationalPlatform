using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands
{
    /// <summary>
    /// Encapsulates the body of an update request
    /// </summary>
    public class DTOEducationalInstitutionLocationUpdateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public Guid EduInstitutionID { get; init; }

        public bool UpdateLocation { get; init; }
        public string LocationID { get; init; }

        public bool BuildingsUpdate { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }

        public DTOEducationalInstitutionLocationUpdateCommand()
        {
        }

        public DTOEducationalInstitutionLocationUpdateCommand(Guid eduInstitutionID, string newLocationID, ICollection<string> newBuildingsIDs)
        {
            EduInstitutionID = eduInstitutionID;
            LocationID = newLocationID;
            BuildingsIDs = newBuildingsIDs;
        }
    }
}