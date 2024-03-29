﻿using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string LocationId { get; set; }

    public string GetFullName() => FirstName + " " + LastName;
}