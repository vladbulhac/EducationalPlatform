using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests.MappersTests
{
    public class DataTransferObjectMappersForQueryServicesTests
    {
        #region MapToEducationalInstitutionGetResponse extension method TESTS

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnGetByIDQueryResult()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.IsType<GetByIDQueryResult>(mappedResult);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedName()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(result.Name, mappedResult.Name);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedDescription()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(result.Description, mappedResult.Description);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedJoinDate()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(result.JoinDate, mappedResult.JoinDate.ToDateTime());
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnACollectionOfBuildingsWithOneElement()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Single(mappedResult.Buildings);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedBuildingsCollection()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(result.BuildingsIDs, mappedResult.Buildings);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedChildInstitutionName()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(childInstitution.Name, mappedResult.ChildInstitutions[0].Name);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnACollectionOfChildInstitutionsWithOneElement()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Single(mappedResult.ChildInstitutions);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedChildInstitutionDescription()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(childInstitution.Description, mappedResult.ChildInstitutions[0].Description);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedChildInstitutionID()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(childInstitution.EducationalInstitutionID, mappedResult.ChildInstitutions[0].EducationalInstitutionId.ToGuid());
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedParentInstitutionName()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(parentInstitution.Name, mappedResult.ParentInstitution.Name);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedParentInstitutionDescription()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(parentInstitution.Description, mappedResult.ParentInstitution.Description);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_ShouldReturnExpectedParentInstitutionID()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Equal(parentInstitution.EducationalInstitutionID, mappedResult.ParentInstitution.EducationalInstitutionId.ToGuid());
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_WithEmptyBuildingsIDs_ShouldReturnEmptyBuildingsCollection()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(0),
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Empty(mappedResult.Buildings);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_WithEmptyChildInstitutions_ShouldReturnEmptyChildInstitutions()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult parentInstitution = new()
            {
                Name = "PName",
                Description = "PDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(0),
                ParentInstitution = parentInstitution
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Empty(mappedResult.ChildInstitutions);
        }

        [Fact]
        public void GivenGetEducationalInstitutionByIDQueryResult_WithNullParentInstitution_ShouldReturnNullParentInstitution()
        {
            //Arrange
            EducationalInstitutionBaseQueryResult childInstitution = new()
            {
                Name = "CName",
                Description = "CDescription",
                EducationalInstitutionID = Guid.NewGuid()
            };

            GetEducationalInstitutionByIDQueryResult result = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationID = "testLocationID",
                JoinDate = DateTime.UtcNow,
                BuildingsIDs = new List<string>(1) { "testBuildingID" },
                ChildInstitutions = new List<EducationalInstitutionBaseQueryResult>(1) { childInstitution },
                ParentInstitution = null
            };

            //Act
            var mappedResult = result.MapToEducationalInstitutionGetResponse();

            //Assert
            Assert.Null(mappedResult.ParentInstitution);
        }

        #endregion MapToEducationalInstitutionGetResponse extension method TESTS

        #region MapToDTOEducationalInstitutionByIDQuery extension method TESTS

        [Fact]
        public void GivenEducationalInstitutionGetByIdRequest_ShouldReturnDTOEducationalInstitutionByIDQuery()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();

            EducationalInstitutionGetByIdRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid()
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionByIDQuery();

            //Assert
            Assert.IsType<DTOEducationalInstitutionByIDQuery>(mappedRequest);
        }

        [Fact]
        public void GivenEducationalInstitutionGetByIdRequest_ShouldReturnExpectedEducationalInstitutionID()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();

            EducationalInstitutionGetByIdRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid()
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionByIDQuery();

            //Assert
            Assert.Equal(educationalInstitutionID, mappedRequest.EducationalInstitutionID);
        }

        #endregion MapToDTOEducationalInstitutionByIDQuery extension method TESTS

        #region MapToDTOEducationalInstitutionsByNameQuery extension method TESTS

        [Fact]
        public void GivenEducationalInstitutionGetByNameRequest_ShouldReturnExpectedName()
        {
            //Arrange
            var name = "testName";
            var offsetValue = 1;
            var resultsCount = 1;
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = name,
                OffsetValue = offsetValue,
                ResultsCount = resultsCount
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionsByNameQuery();

            //Assert
            Assert.Equal(name, mappedRequest.Name);
        }

        [Fact]
        public void GivenEducationalInstitutionGetByNameRequest_ShouldReturnExpectedOffsetValue()
        {
            //Arrange
            var name = "testName";
            var offsetValue = 1;
            var resultsCount = 1;
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = name,
                OffsetValue = offsetValue,
                ResultsCount = resultsCount
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionsByNameQuery();

            //Assert
            Assert.Equal(offsetValue, mappedRequest.OffsetValue);
        }

        [Fact]
        public void GivenEducationalInstitutionGetByNameRequest_ShouldReturnExpectedResultsCount()
        {
            //Arrange
            var name = "testName";
            var offsetValue = 1;
            var resultsCount = 1;
            EducationalInstitutionGetByNameRequest request = new()
            {
                Name = name,
                OffsetValue = offsetValue,
                ResultsCount = resultsCount
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionsByNameQuery();

            //Assert
            Assert.Equal(resultsCount, mappedRequest.ResultsCount);
        }

        #endregion MapToDTOEducationalInstitutionsByNameQuery extension method TESTS

        #region MapToGetByNameResult extension method TESTS

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "testName";
            var description = "testDescription";
            var locationID = "testID";

            GetAllEducationalInstitutionsByNameQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                    EducationalInstitutionID = id,
                    Name = name,
                    Description = description,
                    LocationID = locationID } }
            };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Single(mappedResult);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_ShouldReturnExpectedEducationalInstitutionId()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "testName";
            var description = "testDescription";
            var locationID = "testID";

            GetAllEducationalInstitutionsByNameQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                    EducationalInstitutionID = id,
                    Name = name,
                    Description = description,
                    LocationID = locationID } }
            };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Equal(id.ToProtoUuid(), mappedResult.ElementAt(0).EducationalInstitutionId);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_ShouldReturnExpectedName()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "testName";
            var description = "testDescription";
            var locationID = "testID";

            GetAllEducationalInstitutionsByNameQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                    EducationalInstitutionID = id,
                    Name = name,
                    Description = description,
                    LocationID = locationID } }
            };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Equal(name, mappedResult.ElementAt(0).Name);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_ShouldReturnExpectedDescription()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "testName";
            var description = "testDescription";
            var locationID = "testID";

            GetAllEducationalInstitutionsByNameQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                    EducationalInstitutionID = id,
                    Name = name,
                    Description = description,
                    LocationID = locationID } }
            };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Equal(description, mappedResult.ElementAt(0).Description);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_ShouldReturnExpectedLocationID()
        {
            //Arrange
            var id = Guid.NewGuid();
            var name = "testName";
            var description = "testDescription";
            var locationID = "testID";

            GetAllEducationalInstitutionsByNameQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() { new() {
                    EducationalInstitutionID = id,
                    Name = name,
                    Description = description,
                    LocationID = locationID } }
            };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Equal(locationID, mappedResult.ElementAt(0).LocationId);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByNameQueryResult_WithEmptyCollection_ShouldReturnEmptyCollection()
        {
            //Arrange
            GetAllEducationalInstitutionsByNameQueryResult result = new() { EducationalInstitutions = new List<GetEducationalInstitutionQueryResult>() };

            //Act
            var mappedResult = result.MapToGetByNameResult();

            //Assert
            Assert.Empty(mappedResult);
        }

        #endregion MapToGetByNameResult extension method TESTS

        #region MapToGetByLocationResult extension method TESTS

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnCollectionWithOneElement()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Single(mappedResult);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnExpectedID()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Equal(queryResult.EducationalInstitutionID.ToProtoUuid(), mappedResult.ElementAt(0).EducationalInstitutionId);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnExpectedName()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Equal(queryResult.Name, mappedResult.ElementAt(0).Name);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnExpectedDescription()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Equal(queryResult.Description, mappedResult.ElementAt(0).Description);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnBuildingsCollectionWithOneElement()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Single(mappedResult.ElementAt(0).Buildings);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_ShouldReturnBuildingsCollectionWithExpectedID()
        {
            //Arrange
            GetEducationalInstitutionByLocationQueryResult queryResult = new()
            {
                EducationalInstitutionID = Guid.NewGuid(),
                Name = "testName",
                Description = "testDescription",
                BuildingsIDs = new List<string>() { "building123" }
            };

            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>() { queryResult }
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Equal(queryResult.BuildingsIDs.ElementAt(0), mappedResult.ElementAt(0).Buildings[0]);
        }

        [Fact]
        public void GivenGetAllEducationalInstitutionsByLocationQueryResult_WithEmptyCollection_ShouldReturnEmptyCollection()
        {
            //Arrange
            GetAllEducationalInstitutionsByLocationQueryResult result = new()
            {
                EducationalInstitutions = new List<GetEducationalInstitutionByLocationQueryResult>()
            };

            //Act
            var mappedResult = result.MapToGetByLocationResult();

            //Assert
            Assert.Empty(mappedResult);
        }

        #endregion MapToGetByLocationResult extension method TESTS
    }
}