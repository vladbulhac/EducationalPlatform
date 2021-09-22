using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Commands
{
    public class UpdateEducationalInstitutionAdminsCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }
        public ICollection<string> AddAdminsIDs { get; init; }
        public ICollection<string> RemoveAdminsIDs { get; init; }
    }
}