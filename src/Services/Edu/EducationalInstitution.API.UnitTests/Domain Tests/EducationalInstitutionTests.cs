using EducationalInstitution.API.Tests.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.UnitTests.Domain_Tests
{
    public class EducationalInstitutionTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;

        public EducationalInstitutionTests(TestDataFromJSONParser testDataHelper) => this.testDataHelper = testDataHelper;

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedName()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(name, educationalInstituion.Name);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedDescription()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(description, educationalInstituion.Description);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedLocationID()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(locationID, educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnACollectionBuildingsIDsWithOneElement()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution _ = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Single(buildingsIDs);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedBuildingID()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(buildingsIDs[0], educationalInstituion.Buildings.ElementAt(0).BuildingID);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnACollectionAdminsIDsWithOneElement()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Single(educationalInstituion.Admins);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedAdminID()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(adminsIDs[0], educationalInstituion.Admins.ElementAt(0).AdminID);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedJoinDateDate()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            var expectedDate = DateTime.UtcNow;

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(expectedDate.Date, educationalInstituion.JoinDate.Date);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedJoinDateMonth()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            var expectedDate = DateTime.UtcNow;

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(expectedDate.Month, educationalInstituion.JoinDate.Month);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnExpectedJoinDateDay()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            var expectedDate = DateTime.UtcNow;

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal(expectedDate.Day, educationalInstituion.JoinDate.Day);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnNotDefaultGuid()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.NotEqual(Guid.Empty, educationalInstituion.Id);
        }

        [Fact]
        public void GivenAName_Description_NullLocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnLOCATION_UNKNOWNLocationID()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = null;
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal("LOCATION_UNKNOWN", educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenAName_NullDescription_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnNO_DESCRIPTION()
        {
            //Arrange
            string name = "eduTest";
            string description = null;
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Equal("NO_DESCRIPTION", educationalInstituion.Description);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ParentInstitution_ToConstructor_ShouldReturnNotNullParentInstitution()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Domain::EducationalInstitution parentInstitution = new("pnameTest", "pdescTest", "plocation123", buildingsIDs, adminsIDs);

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs, parentInstitution);

            //Assert
            Assert.NotNull(educationalInstituion.ParentInstitution);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ParentInstitution_ToConstructor_ShouldReturnExpectedParentInstitutionID()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Domain::EducationalInstitution parentInstitution = new("pnameTest", "pdescTest", "plocation123", buildingsIDs, adminsIDs);

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs, parentInstitution);

            //Assert
            Assert.Equal(parentInstitution.Id, educationalInstituion.ParentInstitution.Id);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnFalseEntityAccessIsDisabled()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.False(educationalInstituion.Access.IsDisabled);
        }

        [Fact]
        public void GivenAName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldReturnNullEntityAccessIDateForPermanentDeletion()
        {
            //Arrange
            string name = "eduTest";
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act
            Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs);

            //Assert
            Assert.Null(educationalInstituion.Access.DateForPermanentDeletion);
        }

        [Fact]
        public void GivenANullName_Description_LocationID_BuildingsIDs_AdminsIDs_ToConstructor_ShouldThrowArgumentNullException()
        {
            //Arrange
            string name = null;
            string description = "eduDescription";
            string locationID = "location123";
            var buildingsIDs = new List<string>() { "building123" };
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => { Domain::EducationalInstitution educationalInstituion = new(name, description, locationID, buildingsIDs, adminsIDs); });
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnAnInstanceOfEducationalInstitution()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.NotNull(educationalInstitution);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameId()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Id, educationalInstitution.Id);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameName()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Name, educationalInstitution.Name);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameDescription()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Description, educationalInstitution.Description);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameLocationId()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.LocationID, educationalInstitution.LocationID);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameJoinDate()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.JoinDate, educationalInstitution.JoinDate);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameBuildings()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Buildings, educationalInstitution.Buildings);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameAdmins()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Admins, educationalInstitution.Admins);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameAccessSettings()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.Access, educationalInstitution.Access);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameParentInstitution()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.ParentInstitution, educationalInstitution.ParentInstitution);
        }

        [Fact]
        public void GivenExistingEducationalInstitutionData_ToReconstituteEducationalInstitutionMethod_ShouldReturnSameChildInstitutions()
        {
            //Arrange
            var existingEducationalInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            var educationalInstitution = Domain::EducationalInstitution.ReconstituteEducationalInstitution(existingEducationalInstitution.Id,
                                                                                                           existingEducationalInstitution.Name,
                                                                                                           existingEducationalInstitution.Description,
                                                                                                           existingEducationalInstitution.LocationID,
                                                                                                           existingEducationalInstitution.Buildings,
                                                                                                           existingEducationalInstitution.Admins,
                                                                                                           existingEducationalInstitution.JoinDate,
                                                                                                           existingEducationalInstitution.Access,
                                                                                                           existingEducationalInstitution.ChildInstitutions,
                                                                                                           existingEducationalInstitution.ParentInstitution);

            //Assert
            Assert.Equal(existingEducationalInstitution.ChildInstitutions, educationalInstitution.ChildInstitutions);
        }

        [Fact]
        public void GivenAChildInstitution_ToAddChildInstitutionsMethod_ShouldReturnACollectionChildInstitutionsCollectionWithOneElement()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                       "eduDescription",
                                                                       "location123",
                                                                       new List<string>() { "building123" },
                                                                       new List<Guid>() { Guid.NewGuid() });

            var childInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            educationalInstituion.AddChildInstitutions(new List<Domain::EducationalInstitution>(1) { childInstitution });

            //Assert
            Assert.Single(educationalInstituion.ChildInstitutions);
        }

        [Fact]
        public void GivenAChildInstitution_ToAddChildInstitutionsMethod_ShouldReturnExpectedChildInstitutionID()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            var childInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            educationalInstituion.AddChildInstitutions(new List<Domain::EducationalInstitution>(1) { childInstitution });

            //Assert
            Assert.Equal(childInstitution.Id, educationalInstituion.ChildInstitutions.ElementAt(0).Id);
        }

        [Fact]
        public void GivenNullArgument_ToAddChildInstitutionsMethod_ShouldReturnAnEmptyCollectionChildInstitutions()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.AddChildInstitutions(null);

            //Assert
            Assert.Empty(educationalInstituion.ChildInstitutions);
        }

        [Fact]
        public void GivenAChildInstitutionID_ToRemoveChildInstitutionsMethod_ShouldReturnAnEmptyCollectionChildInstitutions()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                     new List<string>() { "building123" },
                                                                     new List<Guid>() { Guid.NewGuid() });

            var childInstitution = testDataHelper.EducationalInstitutions[0];
            educationalInstituion.AddChildInstitutions(new List<Domain::EducationalInstitution>(1) { childInstitution });

            //Act
            educationalInstituion.RemoveChildInstitutions(new List<Guid>(1) { childInstitution.Id });

            //Assert
            Assert.Empty(educationalInstituion.ChildInstitutions);
        }

        [Fact]
        public void GivenANullArgument_ToRemoveChildInstitutionsMethod_ShouldReturnAnUnchangedCollectionChildInstitutions()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            var childInstitution = testDataHelper.EducationalInstitutions[0];
            educationalInstituion.AddChildInstitutions(new List<Domain::EducationalInstitution>(1) { childInstitution });

            //Act
            educationalInstituion.RemoveChildInstitutions(null);

            //Assert
            Assert.Single(educationalInstituion.ChildInstitutions);
        }

        [Fact]
        public void GivenAParentInstitution_ToSetParentInstitutionMethod_ShouldReturnTheNewParentInstitutionID()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            var parentInstitution = testDataHelper.EducationalInstitutions[0];

            //Act
            educationalInstituion.SetParentInstitution(parentInstitution);

            //Assert
            Assert.Equal(parentInstitution.Id, educationalInstituion.ParentInstitution.Id);
        }

        [Fact]
        public void GivenANullArgument_ToSetParentInstitutionMethod_ShouldReturnNullParentInstitution()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetParentInstitution(null);

            //Assert
            Assert.Null(educationalInstituion.ParentInstitution);
        }

        [Fact]
        public void GivenAName_Description_ToSetNameAndDescriptionMethod_ShouldReturnTheNewName()
        {
            //Arrange
            string newName = "newName";
            string newDescription = "newDescription";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetNameAndDescription(newName, newDescription);

            //Assert
            Assert.Equal(newName, educationalInstituion.Name);
        }

        [Fact]
        public void GivenANullName_Description_ToSetNameAndDescriptionMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            string newDescription = "newDescription";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => educationalInstituion.SetNameAndDescription(null, newDescription));
        }

        [Fact]
        public void GivenAName_Description_ToSetNameAndDescriptionMethod_ShouldReturnTheNewDescription()
        {
            //Arrange
            string newName = "newName";
            string newDescription = "newDescription";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetNameAndDescription(newName, newDescription);

            //Assert
            Assert.Equal(newDescription, educationalInstituion.Description);
        }

        [Fact]
        public void GivenAName_NullDescription_ToSetNameAndDescriptionMethod_ShouldReturnNO_DESCRIPTION()
        {
            //Arrange
            string newName = "newName";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetNameAndDescription(newName, null);

            //Assert
            Assert.Equal("NO_DESCRIPTION", educationalInstituion.Description);
        }

        [Fact]
        public void GivenAName_ToSetNameMethod_ShouldReturnTheNewName()
        {
            //Arrange
            string newName = "newName";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetName(newName);

            //Assert
            Assert.Equal(newName, educationalInstituion.Name);
        }

        [Fact]
        public void GivenANullArgument_ToSetNameMethod_ShouldThrowArgumentNullException()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => educationalInstituion.SetName(null));
        }

        [Fact]
        public void GivenADescription_ToSetDescriptionMethod_ShouldReturnTheNewDescription()
        {
            //Arrange
            string newDescription = "newDescription";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetDescription(newDescription);

            //Assert
            Assert.Equal(newDescription, educationalInstituion.Description);
        }

        [Fact]
        public void GivenANullArgument_ToSetDescriptionMethod_ShouldReturnNO_DESCRIPTION()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetDescription(null);

            //Assert
            Assert.Equal("NO_DESCRIPTION", educationalInstituion.Description);
        }

        [Fact]
        public void GivenALocationID_AddBuildingsIDs_RemoveBuildingsIDs_ToSetEntireLocationMethod_ShouldReturnTheNewLocationID()
        {
            //Arrange
            string newLocationID = "newLocationQ43";
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            var removeBuildingsIDs = new List<string>() { "building123" };

            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetEntireLocation(newLocationID, addBuildingsIDs, removeBuildingsIDs);

            //Assert
            Assert.Equal(newLocationID, educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenALocationID_AddBuildingsIDs_RemoveBuildingsIDs_ToSetEntireLocationMethod_BuildingsCollectionShouldContainTheNewBuildingID()
        {
            //Arrange
            string newLocationID = "newLocationQ43";
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            var removeBuildingsIDs = new List<string>() { "building123" };

            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetEntireLocation(newLocationID, addBuildingsIDs, removeBuildingsIDs);

            //Assert
            Assert.Contains(educationalInstituion.Buildings, b => b.BuildingID == addBuildingsIDs[0]);
        }

        [Fact]
        public void GivenALocationID_AddBuildingsIDs_RemoveBuildingsIDs_ToSetEntireLocationMethod_ShouldReturnIsDisabledTrueForTheExpectedBuilding()
        {
            //Arrange
            string newLocationID = "newLocationQ43";
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            var removeBuildingsIDs = new List<string>() { "building123" };

            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetEntireLocation(newLocationID, addBuildingsIDs, removeBuildingsIDs);

            //Assert
            Assert.True(educationalInstituion.Buildings.Where(b => b.BuildingID == removeBuildingsIDs[0]).Select(b => b.Access.IsDisabled).Single());
        }

        [Fact]
        public void GivenANullLocationID_AddBuildingsIDs_RemoveBuildingsIDs_ToSetEntireLocationMethod_ShouldReturnLOCATION_UNKNOWN()
        {
            //Arrange
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            var removeBuildingsIDs = new List<string>() { "building123" };

            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetEntireLocation(null, addBuildingsIDs, removeBuildingsIDs);

            //Assert
            Assert.Equal("LOCATION_UNKNOWN", educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenALocationID_ToSetLocationMethod_ShouldReturnTheNewLocationID()
        {
            //Arrange
            string newLocationID = "newLocationq32";
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetLocation(newLocationID);

            //Assert
            Assert.Equal(newLocationID, educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenANullArgument_ToSetLocationMethod_ShouldReturnLOCATION_UNKNOWN()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.SetLocation(null);

            //Assert
            Assert.Equal("LOCATION_UNKNOWN", educationalInstituion.LocationID);
        }

        [Fact]
        public void GivenBuildingsIDs_ToCreateAndAddBuildingsMethod_ShouldReturnACollectionBuildingsWithTwoElements()
        {
            //Arrange
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddBuildings(addBuildingsIDs);

            //Assert
            Assert.Equal(2, educationalInstituion.Buildings.Count);
        }

        [Fact]
        public void GivenBuildingsIDs_ToCreateAndAddBuildingsMethod_ShouldReturnACollectionBuildingsThatContainsTheNewBuildingID()
        {
            //Arrange
            var addBuildingsIDs = new List<string>() { "newBuildingE32" };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddBuildings(addBuildingsIDs);

            //Assert
            Assert.Contains(educationalInstituion.Buildings, b => b.BuildingID == addBuildingsIDs[0]);
        }

        [Fact]
        public void GivenANullArgument_ToCreateAndAddBuildingsMethod_ShouldReturnAnUnchangedCollectionBuildingsWithOneElement()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddBuildings(null);

            //Assert
            Assert.Single(educationalInstituion.Buildings);
        }

        [Fact]
        public void GivenANullArgument_ToCreateAndAddBuildingsMethod_ShouldReturnAnUnchangedCollectionBuildingsWithExpectedBuildingID()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddBuildings(null);

            //Assert
            Assert.Equal("building123", educationalInstituion.Buildings.ElementAt(0).BuildingID);
        }

        [Fact]
        public void GivenBuildingsIDs_ToRemoveBuildingsMethod_ShouldReturnIsDisabledTrueOfTheBuildingID()
        {
            //Arrange
            var removeBuildingsIDs = new List<string>() { "building123" };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.RemoveBuildings(removeBuildingsIDs);

            //Assert
            Assert.True(educationalInstituion.Buildings.ElementAt(0).Access.IsDisabled);
        }

        [Fact]
        public void GivenBuildingsIDs_ToRemoveBuildingsMethod_ShouldReturnExpectedDateForPermanentDeletionDate()
        {
            //Arrange
            var removeBuildingsIDs = new List<string>() { "building123" };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            educationalInstituion.RemoveBuildings(removeBuildingsIDs);

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Date, educationalInstituion.Buildings.ElementAt(0).Access.DateForPermanentDeletion.Value.Date);
        }

        [Fact]
        public void GivenBuildingsIDs_ToRemoveBuildingsMethod_ShouldReturnExpectedDateForPermanentDeletionDay()
        {
            //Arrange
            var removeBuildingsIDs = new List<string>() { "building123" };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            var expectedDateForPermanentDeletion = DateTime.UtcNow.Date;
            var daysFromConfigFile = ConfigurationHelper.GetCurrentSettings("DaysUntilDeletion");
            int days = int.Parse(daysFromConfigFile);
            expectedDateForPermanentDeletion = expectedDateForPermanentDeletion.AddDays(days).ToUniversalTime();

            //Act
            educationalInstituion.RemoveBuildings(removeBuildingsIDs);

            //Assert
            Assert.Equal(expectedDateForPermanentDeletion.Day, educationalInstituion.Buildings.ElementAt(0).Access.DateForPermanentDeletion.Value.Day);
        }

        [Fact]
        public void GivenAdminsIDs_ToCreateAndAddAdminsMethod_ShouldReturnACollectionAdminsWithTwoElements()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddAdmins(adminsIDs);

            //Assert
            Assert.Equal(2, educationalInstituion.Admins.Count);
        }

        [Fact]
        public void GivenAdminsIDs_ToCreateAndAddAdminsMethod_ShouldReturnAThatContainsCollectionAdminsWithTheNewAdminID()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddAdmins(adminsIDs);

            //Assert
            Assert.Contains(educationalInstituion.Admins, a => a.AdminID == adminsIDs[0]);
        }

        [Fact]
        public void GivenANullArgument_ToCreateAndAddAdminsMethod_ShouldReturnAnUnchangedCollectionAdminsWithOneElement()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.CreateAndAddAdmins(null);

            //Assert
            Assert.Single(educationalInstituion.Admins);
        }

        [Fact]
        public void GivenAdminsIDs_ToRemoveAdminsMethod_ShouldReturnAnEmptyCollectionAdmins()
        {
            //Arrange
            var adminsIDs = new List<Guid>() { Guid.NewGuid() };
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    adminsIDs);

            //Act
            educationalInstituion.RemoveAdmins(adminsIDs);

            //Assert
            Assert.Empty(educationalInstituion.Admins.Where(a => a.Access.IsDisabled == false));
        }

        [Fact]
        public void GivenANullArgument_ToRemoveAdminsMethod_ShouldReturnAnUnchangedCollectionAdminsWithOneElement()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstituion = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstituion.RemoveAdmins(null);

            //Assert
            Assert.Single(educationalInstituion.Admins);
        }

        [Fact]
        public void CallingScheduleForDeletionMethod_ShouldDisableTheEntity()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstitution = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstitution.ScheduleForDeletion();

            //Assert
            Assert.True(educationalInstitution.Access.IsDisabled);
        }

        [Fact]
        public void CallingScheduleForDeletionMethod_ShouldDisableAllBuildings()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstitution = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123", "building234" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstitution.ScheduleForDeletion();

            //Assert
            Assert.DoesNotContain(educationalInstitution.Buildings, b => b.Access.IsDisabled == false);
        }

        [Fact]
        public void CallingScheduleForDeletionMethod_ShouldDisableAllAdmins()
        {
            //Arrange
            Domain::EducationalInstitution educationalInstitution = new("eduTest",
                                                                    "eduDescription",
                                                                    "location123",
                                                                    new List<string>() { "building123", "building234" },
                                                                    new List<Guid>() { Guid.NewGuid() });

            //Act
            educationalInstitution.ScheduleForDeletion();

            //Assert
            Assert.DoesNotContain(educationalInstitution.Admins, a => a.Access.IsDisabled == false);
        }
    }
}