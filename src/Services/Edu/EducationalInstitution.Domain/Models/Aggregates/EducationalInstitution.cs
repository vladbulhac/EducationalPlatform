using EducationalInstitution.Domain.Building_Blocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationalInstitution.Domain.Models.Aggregates
{
    public class EducationalInstitution : Entity, IAggregateRoot
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
            ICollection<string> buildingsIDs, ICollection<string> adminsIDs, EducationalInstitution parentInstitution = null, DateTime? joinDate = null, Guid? id = null) : base(id)
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
            CreateAndAddAdmins(adminsIDs);
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
                foreach (var buildingID in removeBuildingsIDs)
                {
                    var building = Buildings.SingleOrDefault(b => b.BuildingID == buildingID);
                    if (building is not null)
                        building.ScheduleForDeletion();
                }
            }
        }

        public void CreateAndAddAdmins(ICollection<string> addAdminsIDs)
        {
            if (addAdminsIDs is not null && addAdminsIDs.Count > 0)
            {
                foreach (var adminID in addAdminsIDs)
                {
                    if (adminID is not null)
                        Admins.Add(new(adminID, Id));
                }
            }
        }

        public void RemoveAdmins(ICollection<string> removeAdminsIDs)
        {
            if (removeAdminsIDs is not null && removeAdminsIDs.Count > 0)
            {
                foreach (var adminID in removeAdminsIDs)
                {
                    var admin = Admins.SingleOrDefault(a => a.AdminId == adminID);
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
}