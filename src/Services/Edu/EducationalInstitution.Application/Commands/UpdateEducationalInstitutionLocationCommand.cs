using MediatR;

namespace EducationalInstitution.Application.Commands;

public class UpdateEducationalInstitutionLocationCommand : IRequest<Response>
{
    public Guid EducationalInstitutionID { get; init; }

    public bool UpdateLocation { get; init; }
    public string LocationID { get; init; }

    public bool UpdateBuildings { get; init; }
    public ICollection<string> AddBuildingsIDs { get; init; }
    public ICollection<string> RemoveBuildingsIDs { get; init; }
}