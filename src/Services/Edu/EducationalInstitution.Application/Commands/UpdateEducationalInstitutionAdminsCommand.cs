using EducationalInstitution.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;

namespace EducationalInstitution.Application.Commands
{
    public class UpdateEducationalInstitutionAdminsCommand : IRequest<Response>
    {
        public Guid EducationalInstitutionID { get; init; }
        public ICollection<AdminDetails> NewAdmins { get; init; }
        public ICollection<AdminDetails> AdminsWithNewPermissions { get; init; }
        public ICollection<AdminDetails> AdminsWithRevokedPermissions { get; init; }
    }
}