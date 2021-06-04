using EducationalInstitutionAPI.Business;
using EducationalInstitutionAPI.DTOs;
using System.Net;
using Xunit;

namespace EducationalInstitution.API.UnitTests.BusinessTests.HandlerBase_Tests
{
    public class HandlerBaseTests : HandlerBase<HandlerBaseTests>, IClassFixture<MockDependenciesHelper<HandlerBaseTests>>
    {
        private readonly MockDependenciesHelper<HandlerBaseTests> dependenciesHelper;

        public HandlerBaseTests(MockDependenciesHelper<HandlerBaseTests> dependenciesHelper) : base(dependenciesHelper.mockLogger.Object)
                => this.dependenciesHelper = dependenciesHelper;

        [Fact]
        public void GivenAnExceptionMessage_ShouldReturnFalse()
        {
            //Arrange
            var exceptionMessage = "testException";

            //Act
            var result = HandleException(exceptionMessage);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void GivenAnExceptionMessage_ResponseMessage_ShouldReturnResponseType()
        {
            //Arrange
            var exceptionMessage = "testException";
            var responseMessage = "testResponse";

            //Act
            var result = HandleException<Response>(exceptionMessage, responseMessage);

            //Assert
            Assert.IsType<Response>(result);
        }

        [Fact]
        public void GivenAnExceptionMessage_ResponseMessage_ShouldReturnFalseOperationStatus()
        {
            //Arrange
            var exceptionMessage = "testException";
            var responseMessage = "testResponse";

            //Act
            var result = HandleException<Response>(exceptionMessage, responseMessage);

            //Assert
            Assert.False(result.OperationStatus);
        }

        [Fact]
        public void GivenAnExceptionMessage_ResponseMessage_ShouldReturnExpectedMessage()
        {
            //Arrange
            var exceptionMessage = "testException";
            var responseMessage = "testResponse";

            //Act
            var result = HandleException<Response>(exceptionMessage, responseMessage);

            //Assert
            Assert.Equal(responseMessage, result.Message);
        }

        [Fact]
        public void GivenAnExceptionMessage_ResponseMessage_ShouldReturnStatusCodeInternalServerError()
        {
            //Arrange
            var exceptionMessage = "testException";
            var responseMessage = "testResponse";

            //Act
            var result = HandleException<Response>(exceptionMessage, responseMessage);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public void GivenAnExceptionMessage_ResponseMessage_ShouldReturnNullData()
        {
            //Arrange
            var exceptionMessage = "testException";
            var responseMessage = "testResponse";

            //Act
            var result = HandleException<Response<object>>(exceptionMessage, responseMessage);

            //Assert
            Assert.Null(result.Data);
        }
    }
}