using Aggregator.EducationalInstitutionAPI.Proto;
using Aggregator.Models;
using System.Threading.Tasks;

namespace Aggregator.Services.EducationalInstitution
{
    public interface IEducationalInstitutionCommandService
    {
        public Task<GrpcCallResponse<EducationalInstitutionCreateResponse>> CreateEducationalInstitutionAsync(EducationalInstitutionCreateRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionAsync(EducationalInstitutionUpdateRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionLocationAsync(EducationalInstitutionLocationUpdateRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionParentAsync(EducationalInstitutionParentUpdateRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionUpdateResponse>> UpdateEducationalInstitutionAdminAsync(EducationalInstitutionAdminUpdateRequest request);

        public Task<GrpcCallResponse<EducationalInstitutionDeleteResponse>> DeleteEducationalInstitutionAsync(EducationalInstitutionDeleteRequest request);
    }
}