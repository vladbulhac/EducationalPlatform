using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.Tests.UnitTests.RepositoriesTests
{
    public class EducationalInstitutionRepositoryTests : IClassFixture<TestDataFromJSONParser>
    {
        private readonly TestDataFromJSONParser testDataHelper;
        private Mock<DbSet<EduInstitution>> mockSet;
        private Mock<DataContext> mockContext;
        private IEducationalInstitutionRepository eduRepository;

        public EducationalInstitutionRepositoryTests(TestDataFromJSONParser testDataHelper)
        {
            //Arrange
            this.testDataHelper = testDataHelper;
            var eduInstitutionsQueryable = this.testDataHelper.EduInstitutions.AsQueryable();

            mockSet = new();
            mockSet.As<IQueryable<EduInstitution>>().Setup(s => s.Provider).Returns(eduInstitutionsQueryable.Provider);
            mockSet.As<IQueryable<EduInstitution>>().Setup(s => s.Expression).Returns(eduInstitutionsQueryable.Expression);
            mockSet.As<IQueryable<EduInstitution>>().Setup(s => s.ElementType).Returns(eduInstitutionsQueryable.ElementType);
            mockSet.As<IQueryable<EduInstitution>>().Setup(s => s.GetEnumerator()).Returns(eduInstitutionsQueryable.GetEnumerator());
            mockSet.Setup(s => s.AddAsync(It.IsNotIn<EduInstitution>(testDataHelper.EduInstitutions), It.IsAny<CancellationToken>())).Callback<EduInstitution, CancellationToken>((ei, cancellationToken) => testDataHelper.EduInstitutions.Add(ei));
            mockSet.Setup(s => s.Remove(It.IsIn<EduInstitution>(testDataHelper.EduInstitutions))).Callback<EduInstitution>(ei => testDataHelper.EduInstitutions.Remove(ei));
            //mockSet.Setup(s => s.SingleOrDefaultAsync(ei=>ei.EduInstitutionID==testDataFixture.EduInstitutions[0].EduInstitutionID),It.IsAny<CancellationToken>())).Callback<Expression<Func<>>,CancellationToken>((ei,cancellationToken)=> testDataFixture.EduInstitutions.SingleOrDefault(edu=>edu==ei));

            mockContext = new();
            mockContext.Setup(c => c.EducationalInstitutions).Returns(mockSet.Object);
            //mockContext.Setup(s => s.EducationalInstitutions.FindAsync(It.IsAny<EduInstitution[]>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(testDataFixture.EduInstitutions.FirstOrDefault(ei => ei.EduInstitutionID == testDataFixture.EduInstitutions[0].EduInstitutionID)));

            eduRepository = new EducationalInstitutionRepository(mockContext.Object);
        }

        [Fact]
        public async Task TestCreateMethod_GivenAnEducationalInstitutionObject_ShouldCallOnce_AddAsyncAndSaveChangesAsyncMethods()
        {
            //Arrange
            EduInstitution data = new("School of Testing",
                "Testing Basics 101",
                "LoC123",
                "BuILD123");

            //Act
            await eduRepository.Create(data, new CancellationToken());

            //Assert
            mockSet.Verify(s => s.AddAsync(It.IsAny<EduInstitution>(), It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task TestCreateMethod_GivenAnEducationalInstitutionObject_ShouldAddItToTheListOfEducationalInstitutions()
        {
            //Arrange
            EduInstitution data = new("School of Testing",
                "Testing Basics 101",
                "LoC123",
                "BuILD123");
            //Act
            await eduRepository.Create(data, new CancellationToken());

            //Assert
            Assert.Contains(data, testDataHelper.EduInstitutions);
        }

        /* [Fact]
         public async Task TestCreateMethod_GivenAnEducationalInstitutionObjectThatExists_ShouldThrowAnException()
         {
             //Act
             await eduRepository.Create(testDataFixture.EduInstitutions[0], new CancellationToken());

             //Assert
             Assert.Contains(data, testDataFixture.EduInstitutions);
         } _*/

        /*[Fact]
        public async Task TestDeleteByIDMethod_GivenAGuidID_ShouldRemoveTheObjectWithThatIDFromTheList()
        {
            //Arrange
            var eduInstitution = testDataFixture.EduInstitutions[0];
            //Act
            await eduRepository.DeleteByID(eduInstitution.EduInstitutionID, new CancellationToken());
            //Assert
            Assert.DoesNotContain(eduInstitution, testDataFixture.EduInstitutions);
        }*/
    }
}