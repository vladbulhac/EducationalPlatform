using EducationalInstitutionAPI.Proto;
using EducationalInstitutionAPI.Utils.Mappers;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Utils_Tests.Mappers_Tests;

public class HttpStatusCodeMapperTests
{
    [Fact]
    public void GivenASystemNetHttpStatusCodeOk_SholdReturnProtoHttpStatusCodeOk()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.OK;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.Ok, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeCreated_SholdReturnProtoHttpStatusCodeCreated()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.Created;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.Created, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeAccepted_SholdReturnProtoHttpStatusCodeAccepted()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.Accepted;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.Accepted, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeMultiStatus_SholdReturnProtoHttpStatusCodeMultiStatus()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.MultiStatus;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.MultiStatus, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeBadRequest_SholdReturnProtoHttpStatusCodeBadRequest()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.BadRequest;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.BadRequest, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeBadRequest_SholdReturnProtoHttpStatusCodeNotFound()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.NotFound;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.NotFound, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCodeInternalServerError_SholdReturnProtoHttpStatusCodeInternalServerError()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.InternalServerError;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.InternalServerError, protoHttpStatusCode);
    }

    [Fact]
    public void GivenASystemNetHttpStatusCode_ThatDoesntHaveAnEquivalentInProtoHttpStatusCode_SholdReturnProtoHttpStatusCodeOk()
    {
        //Arrange
        var httpStatusCode = HttpStatusCode.SeeOther;

        //Act
        var protoHttpStatusCode = httpStatusCode.ToProtoHttpStatusCode();

        //Assert
        Assert.Equal(ProtoHttpStatusCode.Ok, protoHttpStatusCode);
    }
}