using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using System;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests
{
    public class ProtobufGuidConverterTests
    {
        [Fact]
        public void GivenAGuid_ConvertedToTwoValuesOfTypeUInt64_ShouldReturnTheSameGuidBack()
        {
            //Arrange
            var identifier = Guid.NewGuid();
            var protoUuid = identifier.ToProtoUuid();

            //Act
            var decodedIdentifier = protoUuid.ToGuid();

            //Assert
            Assert.Equal(identifier, decodedIdentifier);
        }

        [Fact]
        public void GivenANullUuid_ShouldReturnDefaultGuid()
        {
            //Arrange
            Uuid identifier = null;

            //Act
            var encodedIdentifier = identifier.ToGuid();

            //Assert
            Assert.Equal(default, encodedIdentifier);
        }

        [Fact]
        public void GivenADefaultGuid_ShouldReturnNullUuid()
        {
            //Arrange
            Guid identifier = default;

            //Act
            var decodedIdentifier = identifier.ToProtoUuid();

            //Assert
            Assert.Null(decodedIdentifier);
        }
    }
}