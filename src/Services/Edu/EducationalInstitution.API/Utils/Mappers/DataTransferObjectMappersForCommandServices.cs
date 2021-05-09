using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;

namespace EducationalInstitutionAPI.Utils.Mappers
{
    /// <summary>
    /// Contains mappers used in Command services
    /// </summary>
    public static partial class DataTransferObjectMappers
    {
        public static DTOEducationalInstitutionCreateCommand MapToDTOEducationalInstitutionCreateCommand(this EducationalInstitutionCreateRequest request)
        {
            Guid parentInstitutionID = default;
            if (request.ParentInstitutionId is not null)
                parentInstitutionID = request.ParentInstitutionId.ToGuid();

            return new()
            {
                Name = request.Name,
                Description = request.Description,
                LocationID = request.LocationId,
                BuildingsIDs = request.Buildings,
                ParentInstitutionID = parentInstitutionID,
                AdminsIDs = MapAdminsIDsToCollectionOfGuid(request.AdminsIds)
            };
        }

        private static ICollection<Guid> MapAdminsIDsToCollectionOfGuid(RepeatedField<Uuid> adminsIDs)
        {
            var mappedIDs = new List<Guid>(adminsIDs.Count);
            for (int i = 0; i < adminsIDs.Count; i++)
                mappedIDs.Add(adminsIDs[i].ToGuid());

            return mappedIDs;
        }

        public static DTOEducationalInstitutionDeleteCommand MapToDTOEducationalInstitutionDeleteCommand(this EducationalInstitutionDeleteRequest request)
        {
            return new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };
        }
    }
}