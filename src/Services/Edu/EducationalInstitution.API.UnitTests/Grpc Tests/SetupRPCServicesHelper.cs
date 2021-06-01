using Moq;

namespace EducationalInstitution.API.UnitTests.Grpc_Tests
{
    public abstract class SetupRPCServicesHelper<TTestedClass> where TTestedClass : class
    {
        protected readonly MockDependenciesHelper<TTestedClass> dependenciesHelper;

        protected SetupRPCServicesHelper(MockDependenciesHelper<TTestedClass> dependenciesHelper)
        {
            this.dependenciesHelper = dependenciesHelper;
        }

        protected void SetupMockedDependenciesToFailValidation<DTO>() where DTO : class
        {
            dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsRequestValid(It.IsAny<DTO>(), out It.Ref<string>.IsAny))
                                                      .Returns(false);
        }
    }
}