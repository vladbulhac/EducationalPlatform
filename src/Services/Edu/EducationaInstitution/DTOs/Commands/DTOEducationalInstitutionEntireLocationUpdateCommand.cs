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
    public class DTOEducationalInstitutionEntireLocationUpdateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public Guid EduInstitutionID { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }

        public DTOEducationalInstitutionEntireLocationUpdateCommand()
        {
        }

        public DTOEducationalInstitutionEntireLocationUpdateCommand(Guid eduInstitutionID, string newLocationID, ICollection<string> newBuildingsIDs)
        {
            EduInstitutionID = eduInstitutionID;
            LocationID = newLocationID;
            BuildingsIDs = newBuildingsIDs;
        }
    }
}