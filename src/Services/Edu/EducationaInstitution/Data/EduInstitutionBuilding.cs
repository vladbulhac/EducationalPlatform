using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationaInstitutionAPI.Data
{
    public class EduInstitutionBuilding
    {
        public string BuildingID { get; private set; }
        public Guid EduInstitutionID { get; init; }
        public Availability Availability { get; private set; }
    }
}
