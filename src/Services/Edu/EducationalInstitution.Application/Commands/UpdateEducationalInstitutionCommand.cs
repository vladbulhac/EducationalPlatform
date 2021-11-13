using MediatR;

namespace EducationalInstitution.Application.Commands;

public class UpdateEducationalInstitutionCommand : IRequest<Response>
{
    public Guid EducationalInstitutionID { get; init; }

    public bool UpdateName { get; init; }
    public string Name { get; init; }

    public bool UpdateDescription { get; init; }
    public string Description { get; init; }
}