using EducationalInstitutionAPI.Repositories.EducationalInstitutionRepository;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests.RepositoriesTests
{
    [Collection("Database collection")]
    public class EducationalInstitutionRepositoryTests
    {
        private readonly DatabaseFixture dbFixture;
        private readonly IEducationalInstitutionRepository repository;

        /// <remarks>Called before each test</remarks>
        public EducationalInstitutionRepositoryTests(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
            repository = new EducationalInstitutionRepository(dbFixture.DbConnection);
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithTwoElements()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Name.Equals("University of Testing"));
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWitDescription_Testdata()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Description.Equals("Test data"));
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Name.Equals("University of Testing One"));
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldContainAnEntityWitDescription_TestMoreData()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Contains(result, ei => ei.Description.Equals("Test More Data"));
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_WithResultsCount1_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task GivenValidName_OffsetValue_WithResultsCount1_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEntityWithName_UniversityofTesting()
        {
            //Arrange
            string name = "University";
            int offsetValue = 0;
            int resultsCount = 1;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal("University of Testing", result.ElementAt(0).Name);
        }

        [Fact]
        public async Task GivenValidName_ResultsCount_WithOffsetValue1_ToGetAllLikeNameAsyncMethod_ShouldReturnACollectionWithOneElement()
        {
            //Arrange
            string name = "University";
            int offsetValue = 1;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task GivenValidName_ResultsCount_WithOffsetValue1_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEntityWithName_UniversityofTestingOne()
        {
            //Arrange
            string name = "University";
            int offsetValue = 1;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Equal("University of Testing One", result.ElementAt(0).Name);
        }

        [Fact]
        public async Task GivenValidName_ResultsCount_WithOffsetValueGreaterThanTotalQueryResults_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            string name = "University";
            int offsetValue = 3;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GivenAnInvalidName_ThatDoesntExistInDatabase_OffsetValue_ResultsCount_ToGetAllLikeNameAsyncMethod_ShouldReturnAnEmptyCollection()
        {
            //Arrange
            string name = "InvalidName";
            int offsetValue = 0;
            int resultsCount = 10;

            //Act
            var result = await repository.GetAllLikeNameAsync(name, offsetValue, resultsCount);

            //Assert
            Assert.Empty(result);
        }
    }
}