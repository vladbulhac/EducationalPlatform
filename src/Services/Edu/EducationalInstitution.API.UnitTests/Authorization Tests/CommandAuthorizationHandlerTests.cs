using EducationalInstitutionAPI.Authorization;
using EducationalInstitutionAPI.Authorization.Handlers;
using EducationalInstitutionAPI.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Security.Claims;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Authorization_Tests;

public class CommandAuthorizationHandlerTests
{
    private readonly Mock<IHttpContextAccessor> contextAcessor;

    public CommandAuthorizationHandlerTests()
    {
        contextAcessor = new();
    }

    #region DeleteEducationalInstitutionRequirement

    [Fact]
    public async Task GivenADeleteEducationalInstitutionRequirement_TheResourceOwnerHasPermissionAll_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new DeleteEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenADeleteEducationalInstitutionRequirement_TheResourceOwnerHasPermissionDelete_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new DeleteEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.Delete, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenADeleteEducationalInstitutionRequirement_TheResourceIdOfTheOwnerPermissionDoesntMatchTheResourceItTriesToAccess_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new DeleteEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, "different_resource_id") });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenADeleteEducationalInstitutionRequirement_TheResourceOwnerDoesntHaveAnyRequiredPermission_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new DeleteEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.UpdateDetails, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenADeleteEducationalInstitutionRequirement_TheRequestDoesntContainTheResourceIdInHeader_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new DeleteEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        contextAcessor.SetupGet(ca => ca.HttpContext.User).Returns(principal);
        contextAcessor.SetupGet(ca => ca.HttpContext.Request.Headers).Returns(new HeaderDictionary());

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    #endregion DeleteEducationalInstitutionRequirement

    #region UpdateEducationalInstitutionRequirement

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionRequirement_TheResourceOwnerHasPermissionAll_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new UpdateEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionRequirement_TheResourceOwnerHasPermissionUpdateDetails_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new UpdateEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.UpdateDetails, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionRequirement_TheResourceOwnerDoesntHaveAnyRequiredPermission_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new UpdateEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.Delete, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateEducationalInstitutionRequirement_TheResourceIdOfTheOwnerPermissionDoesntMatchTheResourceItTriesToAccess_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new UpdateEducationalInstitutionRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, "different_resource_id") });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    #endregion UpdateEducationalInstitutionRequirement

    #region UpdateAdministratorsRequirement

    [Fact]
    public async Task GivenAnUpdateAdministratorsRequirement_TheResourceOwnerHasPermissionAll_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new UpdateAdministratorsRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateAdministratorsRequirement_TheResourceOwnerHasPermissionChangeAdministrators_ShouldReturnTrue()
    {
        //Arrange
        var requirement = new UpdateAdministratorsRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.ChangeAdministrators, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.True(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateAdministratorsRequirement_TheResourceOwnerDoesntHaveAnyRequiredPermission_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new UpdateAdministratorsRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.Delete, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        SetupMockedDependencies(principal, resourceId);

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    [Fact]
    public async Task GivenAnUpdateAdministratorsRequirement_TheRequestDoesntContainTheResourceIdInHeader_ShouldReturnFalse()
    {
        //Arrange
        var requirement = new UpdateAdministratorsRequirements();

        var resourceId = "t35tv4lu3";

        var claimsIdentity = new ClaimsIdentity(new Claim[1] { new Claim(Permissions.All, resourceId) });
        var principal = new ClaimsPrincipal(claimsIdentity);

        contextAcessor.SetupGet(ca => ca.HttpContext.User).Returns(principal);
        contextAcessor.SetupGet(ca => ca.HttpContext.Request.Headers).Returns(new HeaderDictionary());

        var authHandlerContext = new AuthorizationHandlerContext(new IAuthorizationRequirement[1] { requirement }, principal, null);

        var handler = new CommandAuthorizationHandler(contextAcessor.Object);

        //Act
        await handler.HandleAsync(authHandlerContext);

        //Assert
        Assert.False(authHandlerContext.HasSucceeded);
    }

    #endregion UpdateAdministratorsRequirement

    private void SetupMockedDependencies(ClaimsPrincipal principal, string resourceId)
    {
        contextAcessor.SetupGet(ca => ca.HttpContext.User).Returns(principal);

        var header = new HeaderDictionary
        {
            new KeyValuePair<string, StringValues>("resource_id", new(resourceId))
        };
        contextAcessor.SetupGet(ca => ca.HttpContext.Request.Headers).Returns(header);
    }
}