using EducationalInstitutionAPI.Data;
using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using MediatR;
using System;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    /// <summary>
    /// Encapsulates the body of a delete <see cref="EducationalInstitution"/> request
    /// </summary>
    public class DTOEducationalInstitutionDeleteCommand : IRequest<Response<DeleteEducationalInstitutionCommandResult>>
    {
        public Guid EducationalInstitutionID { get; init; }

        public DTOEducationalInstitutionDeleteCommand()
        {
        }

        public DTOEducationalInstitutionDeleteCommand(Guid eduInstitutionID)
        {
            EducationalInstitutionID = eduInstitutionID;
        }
    }
}