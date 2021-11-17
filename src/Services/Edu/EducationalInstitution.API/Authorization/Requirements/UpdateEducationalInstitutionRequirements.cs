namespace EducationalInstitutionAPI.Authorization.Requirements;

public class UpdateEducationalInstitutionRequirements : RequirementBase
{
    public UpdateEducationalInstitutionRequirements() : base(new UpdateEducationalInstitutionPolicy())
    { }

    public UpdateEducationalInstitutionRequirements(ResourcePolicy policy) : base(policy)
    { }
}