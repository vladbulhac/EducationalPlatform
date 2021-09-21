namespace Aggregator.Authorization.Policies
{
    /// <summary>
    /// Defines a set of rules that must be abided by in order to access a resource
    /// </summary>
    public record ResourcePolicies
    {
        public readonly ResourceConstraints[] constraints;

        public ResourcePolicies(ResourceConstraints[] constraints) => this.constraints = constraints;
    }

    public record CreateEducationalInstitutionPolicy : ResourcePolicies
    {
        public CreateEducationalInstitutionPolicy() : base(new ResourceConstraints[1] { ScopesConstraints() }) { }

        private static ResourceConstraints ScopesConstraints() => new(
                                                                claims: new(2)
                                                                {
                                                                    { "oi_scp", new(1) { "client.educational_institution.all" } },
                                                                    { "oi_aud", new(1) { "educational_institution_api" } }
                                                                },
                                                                minimumClaimsNeeded: 2);
    }

    public record DeleteEducationalInstitutionPolicy : ResourcePolicies
    {
        public DeleteEducationalInstitutionPolicy() : base(new ResourceConstraints[3] {AudienceConstraints(),
                                                                                       ClientScopesConstraints(),
                                                                                       ResourceOwnerPermissionsConstraints()})
        { }

        private static ResourceConstraints AudienceConstraints() => new(claims: new(1) { { "oi_aud", new(1) { "educational_institution_api" } } },
                                                                        minimumClaimsNeeded: 1);
        private static ResourceConstraints ClientScopesConstraints() => new(
                                                                    claims: new(2)
                                                                    {
                                                                        { "oi_scp", new(2) { "client.educational_institution.all", "client.educational_institution.delete" } }
                                                                    },
                                                                    minimumClaimsNeeded: 1);
        private static ResourceConstraints ResourceOwnerPermissionsConstraints() => new(
                                                                                claims: new(2)
                                                                                {
                                                                                    { "user.educational_institution.all", new(1) { "granted" } },
                                                                                    { "user.educational_institution.delete", new(1) { "granted" } }
                                                                                },
                                                                                minimumClaimsNeeded: 1);
    }
}