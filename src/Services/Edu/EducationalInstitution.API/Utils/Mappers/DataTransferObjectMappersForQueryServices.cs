using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Proto;
using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Utils.Mappers
{
    /// <summary>
    /// Contains mappers used in Query services
    /// </summary>
    public static partial class DataTransferObjectMappers
    {
        public static GetByIDQueryResult MapToEducationalInstitutionGetResponse(this GetEducationalInstitutionByIDQueryResult result)
        {
            BaseQueryResult parentInstitution = null;
            if (result.ParentInstitution is not null)
                parentInstitution = new()
                {
                    EducationalInstitutionId = result.ParentInstitution.EducationalInstitutionID.ToProtoUuid(),
                    Name = result.ParentInstitution.Name,
                    Description = result.ParentInstitution.Description
                };

            GetByIDQueryResult response = new()
            {
                Name = result.Name,
                Description = result.Description,
                JoinDate = Timestamp.FromDateTime(result.JoinDate),
                LocationId = result.LocationID,
                ParentInstitution = parentInstitution
            };

            foreach (var buildingID in result.BuildingsIDs)
                response.Buildings.Add(buildingID);

            if (result.ChildInstitutions.Count > 0)
                AddChildInstitutionToResult(ref response, result.ChildInstitutions);

            return response;
        }

        private static void AddChildInstitutionToResult(ref GetByIDQueryResult result, ICollection<EducationalInstitutionBaseQueryResult> childInstitutions)
        {
            foreach (var childInstitution in childInstitutions)
            {
                BaseQueryResult institution = new()
                {
                    EducationalInstitutionId = childInstitution.EducationalInstitutionID.ToProtoUuid(),
                    Name = childInstitution.Name,
                    Description = childInstitution.Description
                };

                result.ChildInstitutions.Add(institution);
            }
        }

        public static DTOEducationalInstitutionByIDQuery MapToDTOEducationalInstitutionByIDQuery(this EducationalInstitutionGetByIdRequest request)
        {
            return new()
            {
                EducationalInstitutionID = request.EducationalInstitutionId.ToGuid()
            };
        }
    }
}