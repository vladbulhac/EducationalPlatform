using EducationalInstitution.Application.Queries;
using EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;
using EducationalInstitutionAPI.Proto;
using Google.Protobuf.WellKnownTypes;

namespace EducationalInstitutionAPI.Utils.Mappers;

/// <summary>
/// Contains extension methods used in rpc Query services to map between an rpc request message and a dto
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
            JoinDate = Timestamp.FromDateTime(result.JoinDate.ToUniversalTime()),
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

    public static GetAllEducationalInstitutionsByBuildingQuery MapToGetAllEducationalInstitutionsByBuildingQuery(this EducationalInstitutionsGetByBuildingRequest request)
            => new() { BuildingID = request.BuildingId };

    public static GetEducationalInstitutionByIDQuery MapToGetEducationalInstitutionByIDQuery(this EducationalInstitutionGetByIdRequest request)
            => new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };

    public static GetAllEducationalInstitutionsByNameQuery MapToGetAllEducationalInstitutionsByNameQuery(this EducationalInstitutionGetByNameRequest request)
            => new()
            {
                Name = request.Name,
                OffsetValue = request.OffsetValue,
                ResultsCount = request.ResultsCount
            };

    public static ICollection<GetByNameResult> MapToGetByNameResult(this GetAllEducationalInstitutionsByNameQueryResult result)
    {
        List<GetByNameResult> educationalInstitutions = new(result.EducationalInstitutions.Count);
        foreach (var educationalInstitution in result.EducationalInstitutions)
            educationalInstitutions.Add(new()
            {
                EducationalInstitutionId = educationalInstitution.EducationalInstitutionID.ToProtoUuid(),
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description,
                LocationId = educationalInstitution.LocationID
            });

        return educationalInstitutions;
    }

    public static ICollection<BaseQueryResult> MapToBaseQueryResult(this GetAllEducationalInstitutionsWithSameBuildingQueryResult result)
    {
        List<BaseQueryResult> educationalInstitutions = new(result.EducationalInstitutions.Count);
        foreach (var educationalInstitution in result.EducationalInstitutions)
            educationalInstitutions.Add(new()
            {
                EducationalInstitutionId = educationalInstitution.EducationalInstitutionID.ToProtoUuid(),
                Name = educationalInstitution.Name,
                Description = educationalInstitution.Description
            });

        return educationalInstitutions;
    }

    public static GetAllEducationalInstitutionsByLocationQuery MapToGetAllEducationalInstitutionsByLocationQuery(this EducationalInstitutionsGetByLocationRequest request)
            => new() { LocationID = request.LocationId };

    public static ICollection<GetByLocationResult> MapToGetByLocationResult(this GetAllEducationalInstitutionsByLocationQueryResult result)
    {
        List<GetByLocationResult> educationalInstitutions = new(result.EducationalInstitutions.Count);
        foreach (var educationalInstitution in result.EducationalInstitutions)
            educationalInstitutions.Add(new()
            {
                EducationalInstitutionId = educationalInstitution.EducationalInstitutionID.ToProtoUuid(),
                Name = educationalInstitution.Name,
                Buildings = { educationalInstitution.BuildingsIDs },
                Description = educationalInstitution.Description,
            });

        return educationalInstitutions;
    }

    public static GetAllAdminsByEducationalInstitutionIDQuery MapToGetAllAdminsByEducationalInstitutionIDQuery(this AdminsGetByEducationalInstitutionIdRequest request)
            => new() { EducationalInstitutionID = request.EducationalInstitutionId.ToGuid() };
}