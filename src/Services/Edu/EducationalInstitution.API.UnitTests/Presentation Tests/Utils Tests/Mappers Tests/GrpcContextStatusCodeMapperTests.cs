using EducationalInstitutionAPI.Utils.Mappers;
using Grpc.Core;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Utils_Tests.Mappers_Tests
{
    public class GrpcContextStatusCodeMapperTests
    {
        [Fact]
        public void GivenHttpStatusCodeNotFound_ShouldReturnStatusCodeNotFound()
        {
            //Arrange
            var code = HttpStatusCode.NotFound;

            //Act
            var result = code.ToGrpcContextStatusCode();

            //Assert
            Assert.Equal(StatusCode.NotFound, result);
        }

        [Fact]
        public void GivenHttpStatusCodeInternalServerError_ShouldReturnStatusCodeAborted()
        {
            //Arrange
            var code = HttpStatusCode.InternalServerError;

            //Act
            var result = code.ToGrpcContextStatusCode();

            //Assert
            Assert.Equal(StatusCode.Aborted, result);
        }

        [Fact]
        public void GivenHttpStatusCodeOK_ShouldReturnStatusCodeOK()
        {
            //Arrange
            var code = HttpStatusCode.OK;

            //Act
            var result = code.ToGrpcContextStatusCode();

            //Assert
            Assert.Equal(StatusCode.OK, result);
        }

        [Fact]
        public void GivenHttpStatusCodeAccepted_ShouldReturnStatusCodeOK()
        {
            //Arrange
            var code = HttpStatusCode.Accepted;

            //Act
            var result = code.ToGrpcContextStatusCode();

            //Assert
            Assert.Equal(StatusCode.OK, result);
        }
    }
}