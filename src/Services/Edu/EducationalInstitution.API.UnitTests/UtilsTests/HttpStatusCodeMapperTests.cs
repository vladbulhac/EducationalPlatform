using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests
{
    public class HttpStatusCodeMapperTests
    {
        [Fact]
        public void GivenASystemNetHttpStatusCodeOk_SholdReturnProtoHttpStatusCodeOk()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.OK;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeCreated_SholdReturnProtoHttpStatusCodeCreated()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.Created;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Created, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeMultiStatus_SholdReturnProtoHttpStatusCodeMultiStatus()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.MultiStatus;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.MultiStatus, protoCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeBadRequest_SholdReturnProtoHttpStatusCodeBadRequest()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.BadRequest;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.BadRequest, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCodeInternalServerError_SholdReturnProtoHttpStatusCodeInternalServerError()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.InternalServerError;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.InternalServerError, protoHttpStatusCode);
        }

        [Fact]
        public void GivenASystemNetHttpStatusCode_ThatDoesntHaveAnEquivalentInProtoHttpStatusCode_SholdReturnProtoHttpStatusCodeOk()
        {
            //Arrange
            var httpStatusCode = HttpStatusCode.SeeOther;

            //Act
            httpStatusCode.MapToEquivalentProtoHttpStatusCodeOrOK(out ProtoHttpStatusCode protoHttpStatusCode);

            //Assert
            Assert.Equal(ProtoHttpStatusCode.Ok, protoHttpStatusCode);
        }
    }
}