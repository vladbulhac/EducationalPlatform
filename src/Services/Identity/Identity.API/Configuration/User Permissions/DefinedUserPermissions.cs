namespace Identity.API.Configuration.User_Permissions;

public static class DefinedUserPermissions
{
    public class EducationalInstitutionPermissions : IUserPermission
    {
        public const string All = "user.educational_institution.all";
        public const string UpdateDetails = "user.educational_institution.update_details";
        public const string ChangeAdministrators = "user.educational_institution.change_administrators";
        public const string Delete = "user.educational_institution.delete";

        public IEnumerable<string> GetAllPermissions()
        {
            yield return All;
            yield return UpdateDetails;
            yield return ChangeAdministrators;
            yield return Delete;
        }
    }
}