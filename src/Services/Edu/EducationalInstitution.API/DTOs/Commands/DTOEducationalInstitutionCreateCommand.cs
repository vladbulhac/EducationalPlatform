using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Commands_Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionCreateCommand : IRequest<Response<EducationalInstitutionCommandResult>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
        public ICollection<Guid> AdminsIDs { get; init; }

        /// <remarks>
        /// If an Educational Institution is created without a parent then this field will be equal to the default value of Guid: 00000000-0000-0000-0000-000000000000
        /// </remarks>
        public Guid ParentInstitutionID { get; init; }
    }
}