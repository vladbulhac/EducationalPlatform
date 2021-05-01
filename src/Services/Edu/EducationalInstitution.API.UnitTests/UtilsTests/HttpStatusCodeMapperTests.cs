using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests
{
    public class HttpStatusCodeMapperTests
    {
        [Fact]
        public void GivenASystemNetHttpStatusCodeCreated_SholdReturnTrue()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.Created;

            //Act
            var result = httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeCreated_SholdReturnProtoHttpStatusCodeCreated()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.Created;

            //Act
            httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(HttpStatusCode.Created, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeMultiStatus_SholdReturnTrue()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.MultiStatus;

            //Act
            var result = httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeMultiStatus_SholdReturnProtoHttpStatusCodeMultiStatus()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.MultiStatus;

            //Act
            httpStatusCode.CanMapToProto(out HttpStatusCode protoCode);

            //Assert
            Assert.Equal(HttpStatusCode.MultiStatus, protoCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeBadRequest_SholdReturnTrue()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.BadRequest;

            //Act
            var result = httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeBadRequest_SholdReturnProtoHttpStatusCodeBadRequest()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.BadRequest;

            //Act
            httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeInternalServerError_SholdReturnTrue()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.InternalServerError;

            //Act
            var result = httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeInternalServerError_SholdReturnProtoHttpStatusCodeInternalServerError()
        {
            //Arrange
            var httpStatusCode = System.Net.HttpStatusCode.InternalServerError;

            //Act
            httpStatusCode.CanMapToProto(out HttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, protoHttpStatusCode);
        }
    }
}