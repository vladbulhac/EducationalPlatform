using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Identity.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Description { get; private set; }
        public ICollection<string> Skills { get; private set; }
        public ICollection<string> Interests { get; private set; }
        public string Country { get; private set; }
        public ICollection<string> Languages { get; private set; }
        public DateTime Birthdate { get; private set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public UserSettings.UserSettings Settings { get; private set; }
        public Student StudentDetails { get; private set; }
        public Professor ProfessorDetails { get; private set; }
        public Admin AdminDetails { get; private set; }

        #region Set methods

        public static ApplicationUser Create(string firstname, string lastname, string country, string birthdate, ICollection<string> languages, string description = null)
        {
            return new ApplicationUser
            {
                FirstName = firstname.ToUpper(),
                LastName = lastname.ToUpper(),
                Country = country.ToUpper(),
                Languages = new HashSet<string>(languages),
                Birthdate = DateTime.ParseExact(birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture),

                Description = description,
                Skills = new HashSet<string>(),
                Interests = new HashSet<string>(),
            };
        }

        public void SetAccountDetails(Student studentDetails = null, Professor professorDetails = null, Admin adminDetails = null)
        {
            StudentDetails = studentDetails;
            ProfessorDetails = professorDetails;
            AdminDetails = adminDetails;
        }

        public void SetAccountSettings(UserSettings.UserSettings settings)
        {
            Settings = settings;
        }

        public void UpdateBaseInfo(string firstname = null, string lastname = null, string country = null, string birthdate = null, ICollection<string> languages = default, string description = null)
        {
            FirstName = firstname == null ? FirstName : firstname.ToUpper();
            LastName = lastname == null ? LastName : lastname.ToUpper();
            Description = description == null ? Description : description;
            Country = country == null ? Country : country.ToUpper();
            Birthdate = birthdate == null ? Birthdate : DateTime.ParseExact(birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Languages = languages.Count == 0 ? Languages : languages;
        }

        #endregion Set methods
    }
}