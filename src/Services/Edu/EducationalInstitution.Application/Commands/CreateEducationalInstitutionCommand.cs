using EducationalInstitution.Application.Commands.Results;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Commands
{
    public class CreateEducationalInstitutionCommand : IRequest<Response<CreateEducationalInstitutionCommandResult>>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string LocationID { get; init; }
        public ICollection<string> BuildingsIDs { get; init; }
        public string AdminId { get; init; }

        /// <remarks>
        /// <i>If an Educational Institution is created without a parent then this field will be equal to the default value of Guid: 00000000-0000-0000-0000-000000000000</i>
        /// </remarks>
        public Guid ParentInstitutionID { get; init; }
    }
}