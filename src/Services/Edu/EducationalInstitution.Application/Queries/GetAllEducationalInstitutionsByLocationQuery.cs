using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using MediatR;

namespace EducationalInstitution.Application.Queries;

public class GetAllEducationalInstitutionsByLocationQuery : IRequest<Response<GetAllEducationalInstitutionsByLocationQueryResult>>
{
    public string LocationID { get; init; }
}