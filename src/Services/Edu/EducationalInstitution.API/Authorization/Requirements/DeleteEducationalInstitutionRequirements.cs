namespace EducationalInstitutionAPI.Authorization.Requirements;

public class DeleteEducationalInstitutionRequirements : RequirementBase
{
    public DeleteEducationalInstitutionRequirements() : base(new DeleteEducationalInstitutionPolicy())
    { }

    public DeleteEducationalInstitutionRequirements(ResourcePolicy policy) : base(policy)
    { }
}