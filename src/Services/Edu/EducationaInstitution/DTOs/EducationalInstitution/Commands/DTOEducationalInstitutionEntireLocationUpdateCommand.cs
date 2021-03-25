using EducationaInstitutionAPI.Data.Helpers.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In.Commands
{
    /// <summary>
    /// Encapsulates the request body of an entire location Update operation
    /// </summary>
    public class DTOEducationalInstitutionEntireLocationUpdateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public Guid EduInstitutionID { get; init; }
        public string NewLocationID { get; init; }
        public string NewBuildingID { get; init; }
    }
}