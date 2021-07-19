using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;
using System;

namespace EducationalInstitution.Application.Queries
{
    public class GetEducationalInstitutionByIDQuery : IRequest<Response<GetEducationalInstitutionByIDQueryResult>>
    {
        public Guid EducationalInstitutionID { get; init; }
    }
}