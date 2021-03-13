﻿using System;
using System.Collections.Generic;

namespace EducationaInstitutionAPI.Data
{
    public class EduInstitution
    {
        public Guid EduInstitutionID { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string LocationID { get; private set; }
        public string BuildingID { get; private set; }
        public ICollection<Student> Students { get; private set; }
        public ICollection<Professor> Professors { get; private set; }
        public ICollection<Staff> Personnel { get; private set; }
        public Availability Availability { get; private set; }

        public EduInstitution(string name, string description, string locationID, string buildingID)
        {
            EduInstitutionID = Guid.NewGuid();
            Name = name;
            Description = description;
            LocationID = locationID;
            BuildingID = buildingID;
            Students = new HashSet<Student>();
            Personnel = new HashSet<Staff>();
            Professors = new HashSet<Professor>();
            Availability = new();
        }

        public EduInstitution(
            string name,
            string description,
            string locationID,
            string buildingID,
            ICollection<Student> students,
            ICollection<Professor> professors,
            ICollection<Staff> personnel
            ) : this(
                name,
                description,
                locationID,
                buildingID
                )
        {
            Students = students;
            Professors = professors;
            Personnel = personnel;
        }

        public EduInstitution()
        {
        }

        public void Update(string name, string description, string buildingID, string locationID)
        {
            Name = name;
            Description = description;
            BuildingID = buildingID;
            LocationID = locationID;
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student))
                Students.Add(student);
        }

        public void RemoveStudent(Student student) => Students.Remove(student);

        public void AddProfessor(Professor professor)
        {
            if (!Professors.Contains(professor))
                Professors.Add(professor);
        }

        public void RemoveProfessor(Professor professor) => Professors.Remove(professor);

        public void AddStaff(Staff staff)
        {
            if (!Personnel.Contains(staff))
                Personnel.Add(staff);
        }

        public void RemoveStaff(Staff staff) => Personnel.Remove(staff);
    }
}