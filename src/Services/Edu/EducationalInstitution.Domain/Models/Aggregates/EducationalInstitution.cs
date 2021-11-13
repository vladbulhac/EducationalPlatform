using EducationalInstitution.Domain.Building_Blocks;

namespace EducationalInstitution.Domain.Models.Aggregates;

public class EducationalInstitution : GuidEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime JoinDate { get; init; }
    public string LocationID { get; private set; }
    public ICollection<EducationalInstitutionBuilding> Buildings { get; private set; }

    ///<summary>Contains the Educational Institutions that are part of the current Educational Institution</summary>
    /// <remarks>
    /// <i>
    /// <para>For example an university can have multiple faculties that are part of it</para>
    /// OPTIONAL
    /// </i>
    /// </remarks>
    public ICollection<EducationalInstitution> ChildInstitutions { get; private set; }

    ///<summary>Contains the Educational Institution that is the parent of this entity</summary>
    /// <remarks>
    /// <i>
    /// <para>For example a faculty can have an university as a parent</para>
    /// OPTIONAL
    /// </i>
    /// </remarks>
    public EducationalInstitution ParentInstitution { get; private set; }

    public ICollection<EducationalInstitutionAdmin> Admins { get; private set; }

    public EducationalInstitution(string name, string description, string locationID,
        ICollection<string> buildingsIDs, string adminId, EducationalInstitution parentInstitution = null, DateTime? joinDate = null, Guid? id = null) : base(id)
    {
        Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException(nameof(name)) : name;
        Description = description ?? "NO_DESCRIPTION";
        LocationID = locationID ?? "LOCATION_UNKNOWN";
        JoinDate = joinDate ?? DateTime.UtcNow;

        Buildings = new HashSet<EducationalInstitutionBuilding>();
        ChildInstitutions = new HashSet<EducationalInstitution>();
        Admins = new HashSet<EducationalInstitutionAdmin>();
        ParentInstitution = parentInstitution;

        CreateAndAddBuildings(buildingsIDs);
        CreateAndAddAdmin(adminId, new string[1] { "user.educational_institution.all" });
    }

    public EducationalInstitution()
    { }

    public static EducationalInstitution ReconstituteEducationalInstitution(Guid id, string name, string description, string locationID,
        ICollection<EducationalInstitutionBuilding> buildings, ICollection<EducationalInstitutionAdmin> admins, DateTime joinDate, Access access, ICollection<EducationalInstitution> childInstitutions = null, EducationalInstitution parentInstitution = null)
    {
        return new EducationalInstitution
        {
            Id = id,
            Name = name,
            Description = description,
            LocationID = locationID,
            Buildings = buildings,
            Admins = admins,
            JoinDate = joinDate,
            ParentInstitution = parentInstitution,
            ChildInstitutions = childInstitutions,
            Access = access
        };
    }

    public void AddChildInstitutions(ICollection<EducationalInstitution> childInstitutions)
    {
        if (childInstitutions is not null && childInstitutions.Count > 0)
        {
            foreach (var childInstitution in childInstitutions)
                ChildInstitutions.Add(childInstitution);
        }
    }

    public void RemoveChildInstitutions(ICollection<Guid> childInstitutionsIDs)
    {
        if (childInstitutionsIDs is not null && childInstitutionsIDs.Count > 0)
        {
            foreach (var childInstitutionID in childInstitutionsIDs)
            {
                var childInstitution = ChildInstitutions.SingleOrDefault(ci => ci.Id == childInstitutionID);

                if (childInstitution is not null)
                    ChildInstitutions.Remove(childInstitution);
            }
        }
    }

    public void SetParentInstitution(EducationalInstitution newParentInstitution) => ParentInstitution = newParentInstitution;

    public void SetNameAndDescription(string name, string description)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

    public void SetDescription(string description) => Description = description ?? "NO_DESCRIPTION";

    public void SetEntireLocation(string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs)
    {
        SetLocation(locationID);
        CreateAndAddBuildings(addBuildingsIDs);
        RemoveBuildings(removeBuildingsIDs);
    }

    public void SetLocation(string locationID) => LocationID = locationID ?? "LOCATION_UNKNOWN";

    public void CreateAndAddBuildings(ICollection<string> addBuildingsIDs)
    {
        if (addBuildingsIDs is not null && addBuildingsIDs.Count > 0)
        {
            foreach (var buildingID in addBuildingsIDs)
                Buildings.Add(new(buildingID, Id));
        }
    }

    public void RemoveBuildings(ICollection<string> removeBuildingsIDs)
    {
        if (removeBuildingsIDs is not null && removeBuildingsIDs.Count > 0)
        {
            foreach (var buildingId in removeBuildingsIDs)
            {
                var building = Buildings.SingleOrDefault(b => b.Id == buildingId);
                if (building is not null)
                    building.ScheduleForDeletion();
            }
        }
    }

    public void CreateAndAddAdmin(string adminId, ICollection<string> permissions)
    {
        if (adminId is not null)
            Admins.Add(new(adminId, Id, permissions));
    }

    /// <summary>
    /// Removes all the permissions from <paramref name="revokedPermissions"/> and if the admin has no permissions anymore, the admin is scheduled for deletion
    /// </summary>
    public void RevokeAdminPermissions(string adminId, ICollection<string> revokedPermissions)
    {
        var admin = Admins.FirstOrDefault(a => a.Id == adminId);
        if (admin is not null)
        {
            admin.RevokePermissions(revokedPermissions);

            if (admin.Permissions.Count == 0)
                admin.ScheduleForDeletion();
        }
    }

    public void GrantAdminPermissions(string adminId, ICollection<string> grantedPermissions)
    {
        var admin = Admins.FirstOrDefault(a => a.Id == adminId);
        if (admin is not null)
            admin.GrantPermissions(grantedPermissions);
    }

    public void RemoveAdmins(ICollection<string> removeAdminsIds)
    {
        if (removeAdminsIds is not null && removeAdminsIds.Count > 0)
        {
            foreach (var adminID in removeAdminsIds)
            {
                var admin = Admins.SingleOrDefault(a => a.Id == adminID);
                if (admin is not null)
                    admin.ScheduleForDeletion();
            }
        }
    }

    public override void ScheduleForDeletion(double daysUntilDeletion = 30)
    {
        base.ScheduleForDeletion(daysUntilDeletion);

        foreach (var admin in Admins)
            admin.ScheduleForDeletion(daysUntilDeletion);

        foreach (var building in Buildings)
            building.ScheduleForDeletion(daysUntilDeletion);
    }
}