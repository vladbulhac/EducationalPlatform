namespace EducationalInstitutionAPI.Authorization
{
    /// <summary>
    /// Defines a set of rules that must be abided by in order to access a resource
    /// </summary>
    public record ResourcePolicy
    {
        public readonly ResourceAccessConstraints[] constraints;

        public ResourcePolicy(ResourceAccessConstraints[] constraints) => this.constraints = constraints;
    }

    public record DeleteEducationalInstitutionPolicy : ResourcePolicy
    {
        public DeleteEducationalInstitutionPolicy() : base(new ResourceAccessConstraints[1] { ResourceOwnerPermissionsConstraints() })
        { }
        private static ResourceAccessConstraints ResourceOwnerPermissionsConstraints() => new(
                                                                    claims: new(2)
                                                                    {
                                                                        { "user.educational_institution.all", null },
                                                                        { "user.educational_institution.delete", null }
                                                                    },
                                                                    minimumClaimsNeeded: 1);
    }
}