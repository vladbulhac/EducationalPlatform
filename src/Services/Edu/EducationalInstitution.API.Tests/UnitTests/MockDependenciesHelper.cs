﻿using EducationaInstitutionAPI.Business.Validation_Handler;
using EducationaInstitutionAPI.Repositories;
using EducationaInstitutionAPI.Unit_of_Work;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;

namespace EducationalInstitution.API.Tests.UnitTests.BusinessTests.QueriesTests.EducationalInstitutionTests
{
    public class MockDependenciesHelper<T>
    {
        public readonly Mock<ILogger<T>> mockLogger;
        public readonly Mock<IEducationalInstitutionRepository> mockRepository;
        public readonly CancellationToken cancellationToken;
        public readonly Mock<IUnitOfWork> mockUnitOfWork;
        public readonly Mock<IMediator> mockMediator;
        public readonly Mock<IValidationHandler> mockValidationHandler;
        public readonly Mock<ServerCallContext> mockServerCallContext;

        public MockDependenciesHelper()
        {
            mockLogger = new();
            mockRepository = new();
            cancellationToken = new();
            mockUnitOfWork = new();
            mockMediator = new();
            mockValidationHandler = new();
            mockServerCallContext = new();
        }
    }
}