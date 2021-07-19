using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;
using System;

namespace EducationalInstitution.Application.Queries
{
    public class GetAllAdminsByEducationalInstitutionIDQuery : IRequest<Response<GetAllAdminsOfEducationalInstitutionQueryResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}