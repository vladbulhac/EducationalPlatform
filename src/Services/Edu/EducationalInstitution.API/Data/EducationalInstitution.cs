using EducationalInstitutionAPI.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationalInstitutionAPI.Data
{
    public class EducationalInstitution
    {
        public Guid EducationalInstitutionID { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime JoinDate { get; init; }
        public string LocationID { get; private set; }
        public ICollection<EducationalInstitutionBuilding> Buildings { get; private set; }

        ///<summary>Contains the Educational Institutions that are part of the current Educational Institution</summary>
        /// <remarks>
        /// <para>For example an university can have multiple faculties that are part of it</para>
        /// OPTIONAL
        /// </remarks>
        public ICollection<EducationalInstitution> ChildInstitutions { get; private set; }

        ///<summary>Contains the Educational Institution that is the parent of this entity</summary>
        /// <remarks>
        /// <para>For example a faculty can have an university as a parent</para>
        /// OPTIONAL
        /// </remarks>
        public EducationalInstitution ParentInstitution { get; private set; }

        public ICollection<EducationalInstitutionAdmin> Admins { get; private set; }

        public Access EntityAccess { get; }

        public EducationalInstitution(string name, string description, string locationID,
            ICollection<string> buildingsIDs, ICollection<Guid> adminsIDs, EducationalInstitution parentInstitution = null)
        {
            Name = name;
            Description = description ?? "NO_DESCRIPTION";
            EducationalInstitutionID = Guid.NewGuid();
            LocationID = locationID ?? "LOCATION_UNKNOWN";
            JoinDate = DateTime.UtcNow;
            EntityAccess = new();

            Buildings = new HashSet<EducationalInstitutionBuilding>();
            ChildInstitutions = new HashSet<EducationalInstitution>();
            Admins = new HashSet<EducationalInstitutionAdmin>();
            ParentInstitution = parentInstitution;

            CreateAndAddBuildings(buildingsIDs);
            CreateAndAddAdmins(adminsIDs);
        }

        public EducationalInstitution()
        {
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
                    var childInstitution = ChildInstitutions.SingleOrDefault(ci => ci.EducationalInstitutionID == childInstitutionID);

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

        public void SetName(string name) => Name = name ?? Name;

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
                    Buildings.Add(new(buildingID, EducationalInstitutionID));
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
                        building.EntityAccess.ScheduleForDeletion();
                }
            }
        }

        public void CreateAndAddAdmins(ICollection<Guid> addAdminsIDs)
        {
            if (addAdminsIDs is not null && addAdminsIDs.Count > 0)
            {
                foreach (var adminID in addAdminsIDs)
                    Admins.Add(new(adminID, EducationalInstitutionID));
            }
        }

        public void RemoveAdmins(ICollection<Guid> removeAdminsIDs)
        {
            if (removeAdminsIDs is not null && removeAdminsIDs.Count > 0)
            {
                foreach (var adminID in removeAdminsIDs)
                {
                    var admin = Admins.SingleOrDefault(a => a.AdminID == adminID);
                    if (admin is not null)
                        Admins.Remove(admin);
                }
            }
        }
    }
}