using Aggregator.DTOs;
using Aggregator.DTOs.EducationalInstitutionDTOs.Requests;
using Aggregator.DTOs.EducationalInstitutionDTOs.Responses;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public interface IEducationalInstitutionCommandService
    {
        public Task<Response<DTOGetEducationalInstitutionByIDResponse>> CreateEducationalInstitutionAsync(DTOCreateEducationalInstitutionRequest request);
    }
}