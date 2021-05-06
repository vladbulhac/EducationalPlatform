using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using System;

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
                ParentInstitutionID = parentInstitutionID
            };
        }

        public static DTOEducationalInstitutionDeleteCommand MapToDTOEducationalInstitutionDeleteCommand(this EducationalInstitutionDeleteRequest request)
        {
            return new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };
        }
    }
}