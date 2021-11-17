namespace EducationalInstitutionAPI.Authorization.Requirements
{
    public class UpdateAdministratorsRequirements : RequirementBase
    {
        public UpdateAdministratorsRequirements() : base(new UpdateAdministratorsPolicy())
        {
        }

        public UpdateAdministratorsRequirements(ResourcePolicy policy) : base(policy)
        { }
    }
}