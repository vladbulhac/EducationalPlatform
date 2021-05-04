using EducationalInstitutionAPI.Data.Queries_and_Commands_Results.Queries_Results;
using EducationalInstitutionAPI.DTOs.Queries;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using EducationalInstitutionAPI.Utils.Mappers;
using System;
using System.Collections.Generic;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests.MappersTests
{
    public class DataTransferObjectMappersForQueryServicesTests
    {
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
    }
}