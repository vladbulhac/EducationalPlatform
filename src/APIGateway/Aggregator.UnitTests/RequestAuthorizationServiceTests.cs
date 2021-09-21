using Aggregator.Authorization;
using Aggregator.Authorization.Policies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xunit;

namespace Aggregator.UnitTests
{
    public class RequestAuthorizationServiceTests
    {
        private readonly IRequestAuthorizationService authorizationService;

        public RequestAuthorizationServiceTests()
        {
            authorizationService = new RequestAuthorizationService();
        }

        protected record TestPolicies : ResourcePolicies
        {
            public TestPolicies(ResourceConstraints[] constraints) : base(constraints) { }
        }

        private static ResourceConstraints TwoClaimsWithAllRequired() => new(
                                                                claims: new(2)
                                                                {
                                                                    { "claimTypeOne", new(1) { "claimValueOne" } },
                                                                    { "claimTypeTwo", new(1) { "claimValueTwo" } }
                                                                },
                                                                minimumClaimsNeeded: 2);

        private static ResourceConstraints TwoClaimsWithOneRequired() => new(
                                                               claims: new(2)
                                                               {
                                                                   { "claimTypeThree", new(1) { "claimValueThree" } },
                                                                   { "claimTypeFour", new(2) { "claimValueThree", "claimValueFour" } }
                                                               },
                                                               minimumClaimsNeeded: 1);

        [Fact]
        public void GivenAClaimPrincipal_WhoseClaimsDontMatchAnyRequiredClaim_ShouldReturnFalse()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("differentClaimTypeOne", "differentClaimTypeOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAClaimPrincipal_WhoseClaimsDontMatchAnyRequiredClaim_ShouldReturnActionResultOfTypeForbidResult()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("differentClaimTypeOne", "differentClaimTypeOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void GivenAPrincipal_WhoseClaimTypeMatchWithARequiredClaimButTheValueDosent_ShouldReturnFalse()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("claimTypeOne", "differentClaimValueOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAPrincipal_WhoseClaimTypeMatchWithARequiredClaimButTheValueDosent_ShouldReturnActionResultOfTypeForbidResult()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("claimTypeOne", "differentClaimValueOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void GivenAPrincipal_WhoseClaimTypeDoesntMatchWithAnyRequiredClaimType_ShouldReturnFalse()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("differentClaimTypeOne", "claimValueOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAPrincipal_WhoseClaimTypeDoesntMatchWithAnyRequiredClaimType_ShouldReturnActionResultOfTypeForbidResult()
        {
            //Arrange
            var claims = new Claim[1] { new Claim("differentClaimTypeOne", "claimValueOne") };
            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void GivenAClaimPrincipal_OnlyOneClaimMatchesTheRequiredOnes_ShouldReturnFalse()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("differentClaimTypeTwo", "differentClaimValueTwo") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAClaimPrincipal_OnlyOneClaimMatchesTheRequiredOnes_ShouldReturnActionResultOfTypeForbidResult()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("differentClaimTypeTwo", "differentClaimValueTwo") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<ForbidResult>(response);
        }

        [Fact]
        public void GivenAClaimPrincipal_AllClaimsMatchTheRequiredOnes_ShouldReturnTrue()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("claimTypeTwo", "claimValueTwo") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenAClaimPrincipal_AllClaimsMatchTheRequiredOnes_ShouldReturnActionResultOfTypeOkResult()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("claimTypeTwo", "claimValueTwo") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithAllRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void GivenAClaimPrincipal_AtLeastOneClaimMatchARequiredOne_ShouldReturnTrue()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("differentClaimTypeTwo", "differentClaimValueTwo"),
                                        new Claim("claimTypeThree", "claimValueThree"), };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenAClaimPrincipal_AtLeastOneClaimMatchARequiredOne_ShouldReturnActionResultOfTypeOkResult()
        {
            //Arrange
            var claims = new Claim[2] { new Claim("differentClaimTypeTwo", "differentClaimValueTwo"),
                                        new Claim("claimTypeThree", "claimValueThree"), };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[1] { TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void GivenAClaimPrincipal_WithTwoGroupsOfClaimsPolicies_AllClaimsFromPolicyOneAreFound_OneClaimFromPolicyTwoIsFound_ShouldReturnTrue()
        {
            //Arrange
            var claims = new Claim[3] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("claimTypeTwo", "claimValueTwo"),
                                        new Claim("claimTypeFour","claimValueFour") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[2] { TwoClaimsWithAllRequired(), TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenAClaimPrincipal_WithTwoGroupsOfClaimsPolicies_AllClaimsFromPolicyOneAreFound_OneClaimFromPolicyTwoIsFound_ShouldReturnActionResultOfTypeOkResult()
        {
            //Arrange
            var claims = new Claim[3] { new Claim("claimTypeOne", "claimValueOne"),
                                        new Claim("claimTypeTwo", "claimValueTwo"),
                                        new Claim("claimTypeFour","claimValueFour") };

            var identity = new ClaimsIdentity(claims);

            var principal = new ClaimsPrincipal(identity);

            var claimPolicy = new ResourceConstraints[2] { TwoClaimsWithAllRequired(), TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void GivenANullClaimPrincipal__ShouldReturnFalse()
        {
            //Arrange
            ClaimsPrincipal principal = null;

            var claimPolicy = new ResourceConstraints[2] { TwoClaimsWithAllRequired(), TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenANullClaimPrincipal__ShouldReturnActionResultOfTypeBadRequestObjectResult()
        {
            //Arrange
            ClaimsPrincipal principal = null;

            var claimPolicy = new ResourceConstraints[2] { TwoClaimsWithAllRequired(), TwoClaimsWithOneRequired() };
            var testPolicies = new TestPolicies(claimPolicy);

            //Act
            var result = authorizationService.IsRequestValid(principal, testPolicies, out ActionResult response);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}