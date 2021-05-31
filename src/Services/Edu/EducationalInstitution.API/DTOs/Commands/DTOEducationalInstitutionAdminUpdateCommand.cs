using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.DTOs.Commands
{
    public class DTOEducationalInstitutionAdminUpdateCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }
        public ICollection<Guid> AddAdminsIDs { get; init; }
        public ICollection<Guid> RemoveAdminsIDs { get; init; }
    }
}