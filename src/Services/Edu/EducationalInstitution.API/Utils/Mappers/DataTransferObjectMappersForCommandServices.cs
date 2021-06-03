using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationalInstitutionAPI.Utils.Mappers
{
    /// <summary>
    /// Contains extension methods used in rpc Command services to map between an rpc request message and a dto
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
                => new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };

        public static DTOEducationalInstitutionAdminUpdateCommand MapToDTOEducationalInstitutionAdminUpdateCommand(this EducationalInstitutionAdminUpdateRequest request)
                => new()
                {
                    EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                    AddAdminsIDs = request.AddAdminsIds.Select(element => element.ToGuid()).ToList(),
                    RemoveAdminsIDs = request.RemoveAdminsIds.Select(element => element.ToGuid()).ToList()
                };

        public static DTOEducationalInstitutionParentUpdateCommand MapToDTOEducationalInstitutionParentUpdateCommand(this EducationalInstitutionParentUpdateRequest request)
                => new()
                {
                    EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                    ParentInstitutionID = request.ParentInstitutionId.ToGuid()
                };

        public static DTOEducationalInstitutionLocationUpdateCommand MapToDTOEducationalInstitutionLocationUpdateCommand(this EducationalInstitutionLocationUpdateRequest request)
                => new()
                {
                    EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                    UpdateLocation = request.UpdateLocation,
                    LocationID = request.LocationId,
                    UpdateBuildings = request.UpdateBuildings,
                    AddBuildingsIDs = request.AddBuildingsIds,
                    RemoveBuildingsIDs = request.RemoveBuildingsIds
                };

        public static DTOEducationalInstitutionUpdateCommand MapToDTOEducationalInstitutionUpdateCommand(this EducationalInstitutionUpdateRequest request)
                => new()
                {
                    EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                    UpdateName = request.UpdateName,
                    Name = request.Name,
                    UpdateDescription = request.UpdateDescription,
                    Description = request.Description
                };
    }
}