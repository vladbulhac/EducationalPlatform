namespace EducationalInstitutionAPI.Authorization;

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
                                                                    { Permissions.All, null },
                                                                    { Permissions.Delete, null }
                                                                },
                                                                minimumClaimsNeeded: 1);
}

public record UpdateEducationalInstitutionPolicy : ResourcePolicy
{
    public UpdateEducationalInstitutionPolicy() : base(new ResourceAccessConstraints[1] { ResourceOwnerPermissionsConstraints() })
    { }
    private static ResourceAccessConstraints ResourceOwnerPermissionsConstraints() => new(
                                                                 claims: new(2)
                                                                 {
                                                                     { Permissions.All, null },
                                                                     { Permissions.UpdateDetails, null }
                                                                 },
                                                                 minimumClaimsNeeded: 1);
}

public record UpdateAdministratorsPolicy : ResourcePolicy
{
    public UpdateAdministratorsPolicy() : base(new ResourceAccessConstraints[1] { ResourceOwnerPermissionsConstraints() })
    { }

    private static ResourceAccessConstraints ResourceOwnerPermissionsConstraints() => new(
                                                                 claims: new(2)
                                                                 {
                                                                     { Permissions.All, null },
                                                                     { Permissions.ChangeAdministrators, null }
                                                                 },
                                                                 minimumClaimsNeeded: 1);
}