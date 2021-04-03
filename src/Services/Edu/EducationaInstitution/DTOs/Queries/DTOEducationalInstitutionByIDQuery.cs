using EducationaInstitutionAPI.DTOs.EducationalInstitution.Out;
using EducationaInstitutionAPI.Utils;
using MediatR;
using System;

namespace EducationaInstitutionAPI.DTOs.EducationalInstitution.In
{
    /// <summary>
    /// Encapsulates the parameter of a get by id request
    /// </summary>
    public class DTOEducationalInstitutionByIDQuery : IRequest<Response<GetEducationalInstitutionByIDQueryResult>>
    {
        public Guid EduInstitutionID { get; init; }

        public DTOEducationalInstitutionByIDQuery()
        {
        }

        public DTOEducationalInstitutionByIDQuery(Guid eduInstitutionID)
        {
            EduInstitutionID = eduInstitutionID;
        }
    }
}