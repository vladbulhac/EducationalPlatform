using Aggregator.DTOs;
using Aggregator.EducationalInstitutionAPI.Proto;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public interface IEducationalInstitutionQueryService
    {
        public Task<GrpcCallResponse<EducationalInstitutionGetResponse>> GetEducationalInstitutionByIDAsync(EducationalInstitutionGetByIdRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionGetByNameResponse>> GetAllEducationalInstitutionsByNameAsync(EducationalInstitutionGetByNameRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionsGetByLocationResponse>> GetAllEducationalInstitutionsByLocationAsync(EducationalInstitutionsGetByLocationRequest request);
    }
}