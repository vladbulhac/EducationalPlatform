using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using EducationalInstitutionAPI.Utils.Mappers;
using System;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests.MappersTests
{
    public class DataTransferObjectMappersForCommandServicesTests
    {
        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnDTOEducationalInstitutionCreateCommand()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionCreateCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedName()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(request.Name, mappedRequest.Name);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedDescription()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(request.Description, mappedRequest.Description);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedLocationID()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(request.LocationId, mappedRequest.LocationID);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnBuildingsIDsCollectionWithOneElement()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Single(mappedRequest.BuildingsIDs);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnBuildingsIDsCollectionWithSameElement()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(request.Buildings, mappedRequest.BuildingsIDs);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedParentInstitutionID()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(parentInstitutionID, mappedRequest.ParentInstitutionID);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_WithNullParentInstitutionID_ShouldReturnDefaultParentInstitutionID()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = null
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(default, mappedRequest.ParentInstitutionID);
        }
    }
}