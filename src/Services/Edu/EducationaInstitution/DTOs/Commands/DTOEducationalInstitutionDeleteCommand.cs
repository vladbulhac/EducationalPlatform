using EducationaInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;

namespace EducationaInstitutionAPI.DTOs.Commands
{
    /// <summary>
    /// Encapsulates the body of a delete <see cref="EduInstitution>"/> request
    /// </summary>
    public class DTOEducationalInstitutionDeleteCommand : IRequest<Response<DeleteEducationalInstitutionCommandResult>>
    {
        public Guid EduInstitutionID { get; init; }

        public DTOEducationalInstitutionDeleteCommand()
        {
        }

        public DTOEducationalInstitutionDeleteCommand(Guid eduInstitutionID)
        {
            EduInstitutionID = eduInstitutionID;
        }
    }
}