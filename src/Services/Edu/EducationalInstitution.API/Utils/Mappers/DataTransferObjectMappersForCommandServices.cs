using EducationalInstitution.Application;
using EducationalInstitution.Application.Commands;
using EducationalInstitutionAPI.Proto;

namespace EducationalInstitutionAPI.Utils.Mappers;

/// <summary>
/// Contains extension methods used in rpc Command services to map between an rpc request message and a dto
/// </summary>
public static partial class DataTransferObjectMappers
{
    public static CreateEducationalInstitutionCommand MapToCreateEducationalInstitutionCommand(this EducationalInstitutionCreateRequest request)
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
            AdminId = request.AdminId
        };
    }

    public static DisableEducationalInstitutionCommand MapToDisableEducationalInstitutionCommand(this EducationalInstitutionDeleteRequest request)
            => new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };

    public static UpdateEducationalInstitutionAdminsCommand MapToUpdateEducationalInstitutionAdminsCommand(this EducationalInstitutionAdminUpdateRequest request)
            => new()
            {
                EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                NewAdmins = request.NewAdmins.Select(item => new AdminDetails { Identity = item.Identity, Permissions = item.Permissions }).ToList(),
                AdminsWithNewPermissions = request.AdminsWithNewPermissions.Select(item => new AdminDetails { Identity = item.Identity, Permissions = item.Permissions }).ToList(),
                AdminsWithRevokedPermissions = request.AdminsWithRevokedPermissions.Select(item => new AdminDetails { Identity = item.Identity, Permissions = item.Permissions }).ToList()
            };

    public static UpdateEducationalInstitutionParentCommand MapToUpdateEducationalInstitutionParentCommand(this EducationalInstitutionParentUpdateRequest request)
            => new()
            {
                EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                ParentInstitutionID = request.ParentInstitutionId.ToGuid()
            };

    public static UpdateEducationalInstitutionLocationCommand MapToUpdateEducationalInstitutionLocationCommand(this EducationalInstitutionLocationUpdateRequest request)
            => new()
            {
                EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                UpdateLocation = request.UpdateLocation,
                LocationID = request.LocationId,
                UpdateBuildings = request.UpdateBuildings,
                AddBuildingsIDs = request.AddBuildingsIds,
                RemoveBuildingsIDs = request.RemoveBuildingsIds
            };

    public static UpdateEducationalInstitutionCommand MapToUpdateEducationalInstitutionCommand(this EducationalInstitutionUpdateRequest request)
            => new()
            {
                EducationalInstitutionID = request.EducationalInstitutionId.ToGuid(),
                UpdateName = request.UpdateName,
                Name = request.Name,
                UpdateDescription = request.UpdateDescription,
                Description = request.Description
            };
}