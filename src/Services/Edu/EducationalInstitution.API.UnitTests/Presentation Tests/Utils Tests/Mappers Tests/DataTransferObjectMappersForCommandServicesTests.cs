using EducationalInstitution.Application.Commands;
using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Utils_Tests.Mappers_Tests
{
    public class DataTransferObjectMappersForCommandServicesTests
    {
        #region MapToCreateEducationalInstitutionCommand extension method TESTS

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnCreateEducationalInstitutionCommand()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

            //Assert
            Assert.IsType<CreateEducationalInstitutionCommand>(mappedRequest);
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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

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
                ParentInstitutionId = null,
                AdminId = Guid.NewGuid().ToString(),
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

            //Assert
            Assert.Equal(default, mappedRequest.ParentInstitutionID);
        }

        [Fact]
        public void GivenAValidEducationalInstitutionCreateRequest_ShouldReturnExpectedAdminId()
        {
            //Arrange
            var parentInstitutionID = Guid.NewGuid();
            var adminID = Guid.NewGuid().ToString();

            EducationalInstitutionCreateRequest request = new()
            {
                Name = "testName",
                Description = "testDescription",
                LocationId = "testLocation",
                AdminId = Guid.NewGuid().ToString(),
                ParentInstitutionId = parentInstitutionID.ToProtoUuid()
            };
            request.Buildings.Add("testBuilding1");

            //Act
            var mappedRequest = request.MapToCreateEducationalInstitutionCommand();

            //Assert
            Assert.Equal(request.AdminId, mappedRequest.AdminId);
        }

        #endregion MapToCreateEducationalInstitutionCommand extension method TESTS

        #region MapToDisableEducationalInstitutionCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionDeleteRequest_ShouldReturnDisableEducationalInstitutionCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            //Act
            var mappedRequest = request.MapToDisableEducationalInstitutionCommand();

            //Assert
            Assert.IsType<DisableEducationalInstitutionCommand>(mappedRequest);
        }

        [Fact]
        public void GivenAnEducationalInstitutionDeleteRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            EducationalInstitutionDeleteRequest request = new() { EducationalInstitutionId = id.ToProtoUuid() };

            //Act
            var mappedRequest = request.MapToDisableEducationalInstitutionCommand();

            //Assert
            Assert.Equal(mappedRequest.EducationalInstitutionID, id);
        }

        #endregion MapToDisableEducationalInstitutionCommand extension method TESTS

        #region MapToUpdateEducationalInstitutionAdminsCommand extension method TESTS

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnUpdateEducationalInstitutionAdminsCommandType()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionAdminsCommand>(mappedRequest);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedID()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(id, mappedRequest.EducationalInstitutionID);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedNewAdminItemIdentity()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(newAdmins[0].Identity, mappedRequest.NewAdmins.ElementAt(0).Identity);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedNewAdminItemPermissionCollectionWithOneItem()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Single(mappedRequest.NewAdmins.ElementAt(0).Permissions);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedNewAdminItemPermission()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(newAdmins[0].Permissions[0], mappedRequest.NewAdmins.ElementAt(0).Permissions.ElementAt(0));
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithNewPermissionsItemIdentity()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(adminsWithNewPermissions[0].Identity, mappedRequest.AdminsWithNewPermissions.ElementAt(0).Identity);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithNewPermissionsItem_PermissionCollectionWithOneItem()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Single(mappedRequest.AdminsWithNewPermissions.ElementAt(0).Permissions);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithNewPermissionsItemPermission()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(adminsWithNewPermissions[0].Permissions[0], mappedRequest.AdminsWithNewPermissions.ElementAt(0).Permissions.ElementAt(0));
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithRevokedPermissionsItemIdentity()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(adminsWithRevokedPermissions[0].Identity, mappedRequest.AdminsWithRevokedPermissions.ElementAt(0).Identity);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithRevokedPermissionsItemPermissionCollectionWithOneItem()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Single(mappedRequest.AdminsWithRevokedPermissions.ElementAt(0).Permissions);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_ShouldReturnExpectedAdminsWithRevokedPermissionsItemPermission()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Equal(adminsWithRevokedPermissions[0].Permissions[0], mappedRequest.AdminsWithRevokedPermissions.ElementAt(0).Permissions.ElementAt(0));
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_WithEmptyNewAdminsCollection_ShouldReturnEmptyNewAdminsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>();
            var adminsWithNewPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.update" } } };
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };

            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Empty(mappedRequest.NewAdmins);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_WithEmptyAdminsWithNewPermissionsCollection_ShouldReturnEmptyAdminsWithNewPermissionsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>();
            var adminsWithRevokedPermissions = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.delete" } } };

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };
            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Empty(mappedRequest.AdminsWithNewPermissions);
        }

        [Fact]
        public void GivenEducationalInstitutionAdminUpdateRequest_WithEmptyAdminsWithRevokedPermissionsCollection_ShouldReturnEmptyAdminsWithRevokedPermissionsCollection()
        {
            //Arrange
            var id = Guid.NewGuid();
            var newAdmins = new List<AdminInformation>() { new() { Identity = Guid.NewGuid().ToString(), Permissions = { "user.test.all" } } };
            var adminsWithNewPermissions = new List<AdminInformation>();
            var adminsWithRevokedPermissions = new List<AdminInformation>();

            EducationalInstitutionAdminUpdateRequest request = new()
            {
                EducationalInstitutionId = id.ToProtoUuid(),
                NewAdmins = { newAdmins },
                AdminsWithNewPermissions = { adminsWithNewPermissions },
                AdminsWithRevokedPermissions = { adminsWithRevokedPermissions }
            };
            //Act
            var mappedRequest = request.MapToUpdateEducationalInstitutionAdminsCommand();

            //Assert
            Assert.Empty(mappedRequest.AdminsWithRevokedPermissions);
        }

        #endregion MapToUpdateEducationalInstitutionAdminsCommand extension method TESTS

        #region MapToUpdateEducationalInstitutionParentCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionParentUpdateRequest_ShouldReturnUpdateEducationalInstitutionParentCommandType()
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionParentCommand();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionParentCommand>(mappedRequest);
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionParentCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionParentCommand();

            //Assert
            Assert.Equal(parentID, mappedRequest.ParentInstitutionID);
        }

        #endregion MapToUpdateEducationalInstitutionParentCommand extension method TESTS

        #region MapToUpdateEducationalInstitutionLocationCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionLocationUpdateRequest_ShouldReturnUpdateEducationalInstitutionLocationCommandType()
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionLocationCommand>(mappedRequest);
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionLocationCommand();

            //Assert
            Assert.Empty(mappedRequest.LocationID);
        }

        #endregion MapToUpdateEducationalInstitutionLocationCommand extension method TESTS

        #region MapToUpdateEducationalInstitutionCommand extension method TESTS

        [Fact]
        public void GivenAnEducationalInstitutionUpdateRequest_ShouldReturnUpdateEducationalInstitutionCommandType()
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

            //Assert
            Assert.IsType<UpdateEducationalInstitutionCommand>(mappedRequest);
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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

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
            var mappedRequest = request.MapToUpdateEducationalInstitutionCommand();

            //Assert
            Assert.Equal(request.Description, mappedRequest.Description);
        }

        #endregion MapToUpdateEducationalInstitutionCommand extension method TESTS
    }
}