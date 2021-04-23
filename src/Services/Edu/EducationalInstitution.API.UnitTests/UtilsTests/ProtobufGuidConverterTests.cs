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
            identifier.Encode(out UInt64 high64, out UInt64 low64);

            //Act
            var decodedIdentifier = ProtobufGuidConverter.DecodeGuid(high64, low64);

            //Assert
            Assert.Equal(identifier, decodedIdentifier);
        }
    }
}