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

        public Access EntityAccess { get; }

        public EducationalInstitution(string name, string description, string locationID, EducationalInstitution parentInstitution = null)
        {
            Name = name;
            Description = description;
            EducationalInstitutionID = Guid.NewGuid();
            LocationID = locationID ?? "LOCATION_UNKNOWN";
            Buildings = new HashSet<EducationalInstitutionBuilding>();
            ChildInstitutions = new HashSet<EducationalInstitution>();
            ParentInstitution = parentInstitution;
            EntityAccess = new();
            JoinDate = DateTime.UtcNow;
        }

        public EducationalInstitution(string name, string description, string locationID,
            ICollection<string> buildingsIDs, EducationalInstitution parentInstitution = null) : this(name, description, locationID, parentInstitution)
        {
            CreateAndAddBuildings(buildingsIDs);
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

        public void UpdateParentInstitution(EducationalInstitution newParentInstitution) => ParentInstitution = newParentInstitution;

        public void Update(string name, string description, string locationID)
        {
            Name = name;
            Description = description;
            LocationID = locationID;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void UpdateName(string name) => Name = name;

        public void UpdateDescription(string description) => Description = description;

        public void UpdateLocation(string locationID) => LocationID = locationID;

        public void CreateAndAddBuildings(ICollection<string> addBuildingsIDs)
        {
            if (addBuildingsIDs is not null && addBuildingsIDs.Count > 0)
            {
                foreach (var buildingID in addBuildingsIDs)
                {
                    EducationalInstitutionBuilding newBuilding = new(buildingID, EducationalInstitutionID);
                    Buildings.Add(newBuilding);
                }
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
                        Buildings.Remove(building);
                }
            }
        }

        public void UpdateEntireLocation(string locationID, ICollection<string> addBuildingsIDs, ICollection<string> removeBuildingsIDs)
        {
            LocationID = locationID;
            CreateAndAddBuildings(addBuildingsIDs);
            RemoveBuildings(removeBuildingsIDs);
        }
    }
}