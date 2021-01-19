using System;

namespace Identity.API.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public ApplicationUser UserDetails { get; set; }
    }
}