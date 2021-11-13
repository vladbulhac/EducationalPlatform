using DataValidation;
using DataValidation.Exceptions;
using EducationalInstitution.Application.Commands.Validators;
using EducationalInstitution.Application.Queries;
using EducationalInstitution.Application.Queries.Validators;
using System.Text;
using Xunit;

namespace EducationalInstitution.API.UnitTests.Application_Tests.Queries_Tests;

public class QueriesValidatorFactoryTests
{
    private readonly ValidatorFactory validatorFactory = new(typeof(CreateEducationalInstitutionCommandValidator).Assembly);

    [Fact]
    public void GivenAValidTypeGetEducationalInstitutionByIDQuery_ShouldReturnAnInstanceOfGetEducationalInstitutionByIDQueryValidator()
    {
        //Act
        var validator = validatorFactory.CreateValidator<GetEducationalInstitutionByIDQuery>();

        //Assert
        Assert.IsType<GetEducationalInstitutionByIDQueryValidator>(validator);
    }

    [Fact]
    public void GivenAValidTypeGetAllEducationalInstitutionsByLocationQuery_ShouldReturnAnInstanceOfGetAllEducationalInstitutionsByLocationQueryValidator()
    {
        //Act
        var validator = validatorFactory.CreateValidator<GetAllEducationalInstitutionsByLocationQuery>();

        //Assert
        Assert.IsType<GetAllEducationalInstitutionsByLocationQueryValidator>(validator);
    }

    [Fact]
    public void GivenAValidTypeGetAllEducationalInstitutionsByNameQuery_ShouldReturnAnInstanceOfGetAllEducationalInstitutionsByNameQueryValidator()
    {
        //Act
        var validator = validatorFactory.CreateValidator<GetAllEducationalInstitutionsByNameQuery>();

        //Assert
        Assert.IsType<GetAllEducationalInstitutionsByNameQueryValidator>(validator);
    }

    [Fact]
    public void GivenAValidTypeGetAllAdminsByEducationalInstitutionIdQuery_ShouldReturnAnInstanceOfGetAllAdminsByEducationalInstitutionIDQueryValidator()
    {
        //Act
        var validator = validatorFactory.CreateValidator<GetAllAdminsByEducationalInstitutionIDQuery>();

        //Assert
        Assert.IsType<GetAllAdminsByEducationalInstitutionIDQueryValidator>(validator);
    }

    [Fact]
    public void GivenAValidTypeGetAllEducationalInstitutionsByBuildingQuery_ShouldReturnAnInstanceOfGetAllEducationalInstitutionsByBuildingQueryValidator()
    {
        //Act
        var validator = validatorFactory.CreateValidator<GetAllEducationalInstitutionsByBuildingQuery>();

        //Assert
        Assert.IsType<GetAllEducationalInstitutionsByBuildingQueryValidator>(validator);
    }

    [Fact]
    public void GivenAnInvalidType_ShouldThrowAnRequestTypeNotSupportedException()
    {
        //Assert
        Assert.Throws<RequestTypeNotSupportedException>(() => validatorFactory.CreateValidator<StringBuilder>());
    }
}