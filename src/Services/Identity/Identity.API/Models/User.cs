using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public UserPermissions Permissions { get; set; }

        public string LocationId { get; set; }

        public string GetFullName() => FirstName + " " + LastName;
    }
}