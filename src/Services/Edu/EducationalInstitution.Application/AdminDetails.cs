using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Application
{
    public record AdminDetails
    {
        public string Identity { get; init; }
        public ICollection<string> Permissions { get; init; }
    }
}