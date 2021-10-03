using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalInstitution.Application.Integration_Events
{
    public record AdminDetailsForIntegrationEvent : AdminDetails
    {
        public string DetailedMessage { get; init; }
    }
}