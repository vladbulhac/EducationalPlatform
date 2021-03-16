using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    public class DTOEducationalInstitutionByIDQuery : IRequest<Response<GetEducationalInstitutionByIDQueryResult>>
    {
        public Guid EduInstitutionID { get; init; }

        public DTOEducationalInstitutionByIDQuery() { }
        public DTOEducationalInstitutionByIDQuery(Guid eduInstitutionID)
        {
            EduInstitutionID = eduInstitutionID;
        }
    }
}