using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public interface IEducationalInstitutionQueryService
    {
        public Task<GrpcCallResponse<EducationalInstitutionGetResponse>> GetEducationalInstitutionByIDAsync(EducationalInstitutionGetByIdRequest request);
    }
}