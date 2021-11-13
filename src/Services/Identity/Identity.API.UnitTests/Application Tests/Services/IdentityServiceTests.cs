using Identity.API.Application.Services;
using Identity.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using System.Collections.Immutable;
using System.Security.Claims;
using Xunit;

namespace Identity.API.UnitTests.Application_Tests.Services;

public class IdentityServiceTests
{
    private readonly IIdentityService<User> identityService;
    private readonly Mock<IOpenIddictApplicationManager> appManagerMock;
    private readonly Mock<IOpenIddictAuthorizationManager> authManagerMock;
    private readonly Mock<IOpenIddictScopeManager> scopeManagerMock;

    private readonly Mock<SignInManager<User>> signInManagerMock;
    private readonly Mock<UserManager<User>> userManagerMock;

    public IdentityServiceTests()
    {
        appManagerMock = new();
        authManagerMock = new();
        scopeManagerMock = new();

        userManagerMock = new(new Mock<IUserStore<User>>().Object,
                                  new Mock<IOptions<IdentityOptions>>().Object,
                                  new Mock<IPasswordHasher<User>>().Object,
                                  new IUserValidator<User>[0],
                                  new IPasswordValidator<User>[0],
                                  new Mock<ILookupNormalizer>().Object,
                                  new Mock<IdentityErrorDescriber>().Object,
                                  new Mock<IServiceProvider>().Object,
                                  new Mock<ILogger<UserManager<User>>>().Object);
        signInManagerMock = new(userManagerMock.Object,
                                new Mock<IHttpContextAccessor>().Object,
                                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                                new Mock<IOptions<IdentityOptions>>().Object,
                                new Mock<ILogger<SignInManager<User>>>().Object,
                                new Mock<IAuthenticationSchemeProvider>().Object,
                                new Mock<IUserConfirmation<User>>().Object);

        identityService = new IdentityService(appManagerMock.Object, authManagerMock.Object, scopeManagerMock.Object, signInManagerMock.Object, userManagerMock.Object);
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnANotNullClaimsPrincipal()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>(); scopes.Add("openid"); scopes.Add("profile");

        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = Guid.NewGuid().ToString()
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithAnExpectedResource()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>(); scopes.Add("openid"); scopes.Add("profile");

        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = Guid.NewGuid().ToString()
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.True(result.HasResource("test_resource"));
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithTwoScopes()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = Guid.NewGuid().ToString()
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.Equal(2, result.GetScopes().Length);
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithOpenIdScope()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = Guid.NewGuid().ToString()
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.True(result.HasScope("openid"));
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithProfileScope()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = Guid.NewGuid().ToString()
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.True(result.HasScope("profile"));
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithExpectedAuthorizationId()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        var authorizationId = Guid.NewGuid().ToString();
        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = authorizationId
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.Equal(authorizationId, result.GetAuthorizationId());
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipalWithClaimDestinationSetToAccessToken()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        var authorizationId = Guid.NewGuid().ToString();
        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = authorizationId
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.Contains(result.Claims, c => c.Type.Equals("testType") && c.HasDestination("access_token"));
    }

    [Fact]
    public async Task GivenAnUser_Application_Scopes_Authorizations_AndANameClaim_ToCreateUserPrincipalAsync_ShouldReturnAClaimsPrincipal_WithClaimNameDestinationSetToAccessTokenAndIdentityToken()
    {
        //Arrange
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "TestF",
            LastName = "TestL",
            PhoneNumber = "0771777177",
            LocationId = "test_location",
            BirthDate = new(1992, 1, 2)
        };

        OpenIddictEntityFrameworkCoreApplication application = new()
        {
            ClientId = Guid.NewGuid().ToString(),
            DisplayName = "test_app",
            Id = Guid.NewGuid().ToString()
        };

        ImmutableArray<string> scopes = ImmutableArray.Create<string>("openid", "profile");

        var authorizationId = Guid.NewGuid().ToString();
        List<object> authorizations = new(1)
        {
            new OpenIddictEntityFrameworkCoreAuthorization()
            {
                Application = application,
                Scopes = "openid profile",
                Subject = user.Id,
                Id = authorizationId
            }
        };

        var resources = new List<string>(1) { "test_resource" };
        List<Claim> claims = new() { new("testType", "testValue"), new("name", "test") };
        SetupMockedDependencies(scopes, user, application, authorizations[0], claims, resources);

        //Act
        var result = await identityService.CreateUserPrincipalAsync(user, application, scopes, authorizations);

        //Assert
        Assert.Contains(result.Claims, c => c.Type.Equals("name") && c.HasDestination("access_token") && c.HasDestination("id_token"));
    }

    private void SetupMockedDependencies(ImmutableArray<string> scopes, User user, OpenIddictEntityFrameworkCoreApplication application, object authorization, List<Claim> claims, List<string> resources)
    {
        scopeManagerMock.Setup(smm => smm.ListResourcesAsync(scopes, It.IsAny<CancellationToken>())).Returns(resources.ToAsyncEnumerable());

        var claimsIdentity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(claimsIdentity);
        signInManagerMock.Setup(simm => simm.CreateUserPrincipalAsync(user)).ReturnsAsync(principal);

        authManagerMock.Setup(amm => amm.CreateAsync(principal, user.Id, application.Id, It.IsAny<string>(), scopes, It.IsAny<CancellationToken>())).ReturnsAsync(authorization);
    }
}