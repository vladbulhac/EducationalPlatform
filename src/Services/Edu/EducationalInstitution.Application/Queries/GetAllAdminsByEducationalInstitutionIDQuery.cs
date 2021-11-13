using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;

namespace EducationalInstitution.Application.Queries;

public class GetAllAdminsByEducationalInstitutionIDQuery : IRequest<Response<GetAllAdminsOfEducationalInstitutionQueryResult>>
{
    public Guid EducationalInstitutionID { get; init; }
}