using EducationalInstitution.Domain.Building_Blocks;
using Model = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.Domain.Models;

public class EducationalInstitutionBuilding : StringEntity
{
    public Model::EducationalInstitution EducationalInstitution { get; init; }
    public Guid EducationalInstitutionId { get; init; }

    public EducationalInstitutionBuilding()
    {
    }

    public EducationalInstitutionBuilding(string buildingId, Guid educationalInstitutionID) : base(buildingId)
            => EducationalInstitutionId = educationalInstitutionID;
}