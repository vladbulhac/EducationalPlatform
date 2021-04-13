using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data
{
    /// <summary>
    /// Defines the properties of an Educational Institution
    /// </summary>
    public class EducationalInstitution
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// Defines this Educational Institution's join date on the application
        /// </summary>
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

        public Access EntityAccess { get; private set; }

        public EducationalInstitution(string name, string description, string locationID, EducationalInstitution parentInstitution = null)
        {
            Name = name;
            Description = description;
            EduInstitutionID = Guid.NewGuid();
            LocationID = locationID ?? "LOCATION_UNKNOWN";
            Buildings = new HashSet<EducationalInstitutionBuilding>();
            ChildInstitutions = new HashSet<EducationalInstitution>();
            ParentInstitution = parentInstitution;
            EntityAccess = new();
            JoinDate = DateTime.UtcNow;
        }

        public EducationalInstitution(string name, string description, string locationID, ICollection<string> buildingsIDs, EducationalInstitution parentInstitution = null) : this(name, description, locationID, parentInstitution)
        {
            CreateAndAddABuilding(buildingsIDs);
        }

        public EducationalInstitution()
        {
        }

        public void AddChildInstitutions(ICollection<EducationalInstitution> childInstitutions)
        {
            foreach (var childInstitution in childInstitutions)
                ChildInstitutions.Add(childInstitution);
        }

        public void Update(string name, string description, ICollection<string> buildingsIDs, string locationID)
        {
            Name = name;
            Description = description;
            CreateAndAddABuilding(buildingsIDs);
            LocationID = locationID;
        }

        public void Update(string name, string description, string locationID)
        {
            throw new NotImplementedException();
        }

        public void CreateAndAddABuilding(ICollection<string> buildingsIDs)
        {
            foreach (var buildingID in buildingsIDs)
            {
                CreateAndAddABuilding(buildingID);
            }
        }

        private void CreateAndAddABuilding(string buildingID)
        {
            EducationalInstitutionBuilding newBuilding = new(buildingID, this.EduInstitutionID);
            Buildings.Add(newBuilding);
        }

        public void UpdateEntireLocation(string locationID, ICollection<string> buildingsIDs)
        {
            LocationID = locationID;
            Buildings.Clear();
            CreateAndAddABuilding(buildingsIDs);
        }

        public void UpdateLocation(string locationID) => LocationID = locationID;

        public void AddBuilding(string buildingID) => CreateAndAddABuilding(buildingID);
    }
}