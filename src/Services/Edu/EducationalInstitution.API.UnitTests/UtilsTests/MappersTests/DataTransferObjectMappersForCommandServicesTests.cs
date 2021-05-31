using EducationalInstitutionAPI.DTOs.Commands;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests.MappersTests
{
    public class DataTransferObjectMappersForCommandServicesTests
    {
        #region MapToDTOEducationalInstitutionCreateCommand extension method TESTS

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

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
            request.AdminsIds.Add(Guid.NewGuid().ToProtoUuid());

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(default, mappedRequest.ParentInstitutionID);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnCollectionAdminsIDsWithOneElement()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();
            var adminID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");
            request.AdminsIds.Add(adminID.ToProtoUuid());

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Single(mappedRequest.AdminsIDs);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedAdminID()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();
            var adminID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");
            request.AdminsIds.Add(adminID.ToProtoUuid());

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionCreateCommand();

            //Assert
            Assert.Equal(adminID, mappedRequest.AdminsIDs.ElementAt(0));
        }

        #endregion MapToDTOEducationalInstitutionCreateCommand extension method TESTS

        #region MapToDTOEducationalInstitutionDeleteCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionDeleteRequest_ShouldReturnDTOEducationalInstitutionDeleteCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionDeleteCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionDeleteCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAnEducationalInstitutionDeleteRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionDeleteCommand();

            //Assert
            Assert.Equal(mappedRequest.EducationalInstitutionID, id);
        }

        #endregion MapToDTOEducationalInstitutionDeleteCommand extension method TESTS

        #region MapToDTOEducationalInstitutionAdminUpdateCommand extension method TESTS

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnDTOEducationalInstitutionAdminUpdateCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>() { Guid.NewGuid() };
            var removeAdminsID = new List<Guid>() { Guid.NewGuid() };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionAdminUpdateCommand>(mappedRequest);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>() { Guid.NewGuid() };
            var removeAdminsID = new List<Guid>() { Guid.NewGuid() };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.Equal(id, mappedRequest.EducationalInstitutionID);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAddAdminsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>() { Guid.NewGuid() };
            var removeAdminsID = new List<Guid>() { Guid.NewGuid() };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.Equal(addAdminsID, mappedRequest.AddAdminsIDs);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedRemoveAdminsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>() { Guid.NewGuid() };
            var removeAdminsID = new List<Guid>() { Guid.NewGuid() };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.Equal(removeAdminsID, mappedRequest.RemoveAdminsIDs);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_WithEmptyAddAdminsIDs_ShouldReturnEmptyAddAdminsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>();
            var removeAdminsID = new List<Guid>() { Guid.NewGuid() };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.Empty(mappedRequest.AddAdminsIDs);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_WithEmptyRemoveAdminsIDs_ShouldReturnEmptyRemoveAdminsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var addAdminsID = new List<Guid>() { Guid.NewGuid() };
            var removeAdminsID = new List<Guid>();

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                AddAdminsIds = { addAdminsID.ConvertAll(a => a.ToProtoUuid()) },
                RemoveAdminsIds = { removeAdminsID.ConvertAll(a => a.ToProtoUuid()) }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionAdminUpdateCommand();

            //Assert
            Assert.Empty(mappedRequest.RemoveAdminsIDs);
        }

        #endregion MapToDTOEducationalInstitutionAdminUpdateCommand extension method TESTS

        #region MapToDTOEducationalInstitutionParentUpdateCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionParentUpdateRequest_ShouldReturnDTOEducationalInstitutionParentUpdateCommandType()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentID = Guid.NewGuid();

            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentID.ToProtoUuid()
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionParentUpdateCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionParentUpdateCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAnEducationalInstitutionParentUpdateRequest_ShouldReturnExpectedEducationalInstitutionID()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentID = Guid.NewGuid();

            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentID.ToProtoUuid()
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionParentUpdateCommand();

            //Assert
            Assert.Equal(educationalInstitutionID, mappedRequest.EducationalInstitutionID);
        }

        [Fact]
        public void GivenAnEducationalInstitutionParentUpdateRequest_ShouldReturnExpectedParentID()
        {
            //Arrange
            var educationalInstitutionID = Guid.NewGuid();
            var parentID = Guid.NewGuid();

            EducationalInstitutionParentUpdateRequest request = new()
            {
                EducationalInstitutionId = educationalInstitutionID.ToProtoUuid(),
                ParentInstitutionId = parentID.ToProtoUuid()
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionParentUpdateCommand();

            //Assert
            Assert.Equal(parentID, mappedRequest.ParentInstitutionID);
        }

        #endregion MapToDTOEducationalInstitutionParentUpdateCommand extension method TESTS

        #region MapToDTOEducationalInstitutionLocationUpdateCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnDTOEducationalInstitutionLocationUpdateCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionLocationUpdateCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(id, mappedRequest.EducationalInstitutionID);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedUpdateLocation()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(request.UpdateLocation, mappedRequest.UpdateLocation);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedLocationID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(request.LocationId, mappedRequest.LocationID);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedUpdateBuildings()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(request.UpdateBuildings, mappedRequest.UpdateBuildings);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedAddBuildingsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(request.AddBuildingsIds, mappedRequest.AddBuildingsIDs);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnExpectedRemoveBuildingsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Equal(request.RemoveBuildingsIds, mappedRequest.RemoveBuildingsIDs);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_WithEmptyAddBuildingsIDs_ShouldReturnEmptyAddBuildingsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Empty(mappedRequest.AddBuildingsIDs);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_WithEmptyRemoveBuildingsIDs_ShouldReturnEmptyRemoveBuildingsIDsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                LocationId = "location123",
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Empty(mappedRequest.RemoveBuildingsIDs);
        }

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_WithEmptyLocationId_ShouldReturnEmptyLocationID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionLocationUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateLocation = true,
                UpdateBuildings = true,
                AddBuildingsIds = { "building123" },
                RemoveBuildingsIds = { "building0" }
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionLocationUpdateCommand();

            //Assert
            Assert.Empty(mappedRequest.LocationID);
        }

        #endregion MapToDTOEducationalInstitutionLocationUpdateCommand extension method TESTS

        #region MapToDTOEducationalInstitutionUpdateCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnDTOEducationalInstitutionUpdateCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.IsType<DTOEducationalInstitutionUpdateCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(id, mappedRequest.EducationalInstitutionID);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnUpdateName()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(request.UpdateName, mappedRequest.UpdateName);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnExpectedName()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(request.Name, mappedRequest.Name);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnExpectedUpdateDescription()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(request.UpdateDescription, mappedRequest.UpdateDescription);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_WithEmptyName_ShouldReturnEmptyName()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = string.Empty,
                UpdateDescription = true,
                Description = "newDescription"
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(request.Name, mappedRequest.Name);
        }

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_WithEmptyDescription_ShouldReturnEmptyDescription()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                UpdateName = true,
                Name = "newName",
                UpdateDescription = true,
                Description = string.Empty
            };

            //Act
            var mappedRequest = request.MapToDTOEducationalInstitutionUpdateCommand();

            //Assert
            Assert.Equal(request.Description, mappedRequest.Description);
        }

        #endregion MapToDTOEducationalInstitutionUpdateCommand extension method TESTS
    }
}