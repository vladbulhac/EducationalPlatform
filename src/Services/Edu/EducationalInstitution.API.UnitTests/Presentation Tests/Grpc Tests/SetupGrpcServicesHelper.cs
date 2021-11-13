using Moq;

namespace EducationalInstitution.API.UnitTests.Presentation_Tests.Grpc_Tests;

public abstract class SetupGrpcServicesHelper<TTestedClass> where TTestedClass : class
{
    protected readonly MockDependenciesHelper<TTestedClass> dependenciesHelper;

    protected SetupGrpcServicesHelper(MockDependenciesHelper<TTestedClass> dependenciesHelper)
    {
        this.dependenciesHelper = dependenciesHelper;
    }

    protected void SetupMockedDependenciesToFailValidation<DTO>() where DTO : class
    {
        dependenciesHelper.mockValidationHandler.Setup(vh => vh.IsDataTransferObjectValid(It.IsAny<DTO>(), out It.Ref<string>.IsAny))
                                                  .Returns(false);
    }
}