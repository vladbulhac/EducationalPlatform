using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public interface IEducationalInstitutionCommandService
    {
        public Task<GrpcCallResponse<EducationalInstitutionCreateResponse>> CreateEducationalInstitutionAsync(EducationalInstitutionCreateRequest request);
    }
}