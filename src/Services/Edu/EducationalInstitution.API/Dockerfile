#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 50001
EXPOSE 53001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY "EducationalPlatform.sln" "EducationalPlatform.sln"

COPY "EventBus/RabbitMQEventBus/RabbitMQEventBus.csproj" "EventBus/RabbitMQEventBus/RabbitMQEventBus.csproj"
COPY "Validation/DataValidation/DataValidation.csproj" "Validation/DataValidation/DataValidation.csproj"
COPY "BackgroundTasks/DatabaseTasks/DatabaseTasks.csproj" "BackgroundTasks/DatabaseTasks/DatabaseTasks.csproj"

COPY "APIGateway/Aggregator/Aggregator.csproj" "APIGateway/Aggregator/Aggregator.csproj"

COPY "Services/Edu/EducationalInstitution.API.Tests.Shared/EducationalInstitution.API.Tests.Shared.csproj" "Services/Edu/EducationalInstitution.API.Tests.Shared/EducationalInstitution.API.Tests.Shared.csproj"
COPY "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj" "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj"
COPY "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj" "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj"
COPY "Services/Edu/EducationalInstitution.API/EducationalInstitution.API.csproj" "Services/Edu/EducationalInstitution.API/EducationalInstitution.API.csproj"
COPY "Services/Edu/EducationalInstitution.Application/EducationalInstitution.Application.csproj" "Services/Edu/EducationalInstitution.Application/EducationalInstitution.Application.csproj"
COPY "Services/Edu/EducationalInstitution.Domain/EducationalInstitution.Domain.csproj" "Services/Edu/EducationalInstitution.Domain/EducationalInstitution.Domain.csproj"
COPY "Services/Edu/EducationalInstitution.Infrastructure/EducationalInstitution.Infrastructure.csproj" "Services/Edu/EducationalInstitution.Infrastructure/EducationalInstitution.Infrastructure.csproj"

COPY "Services/Notification/Notification.API/Notification.API.csproj" "Services/Notification/Notification.API/Notification.API.csproj"
COPY "Services/Notification/Notification.Application/Notification.Application.csproj" "Services/Notification/Notification.Application/Notification.Application.csproj"
COPY "Services/Notification/Notification.Domain/Notification.Domain.csproj" "Services/Notification/Notification.Domain/Notification.Domain.csproj"
COPY "Services/Notification/Notification.Infrastructure/Notification.Infrastructure.csproj" "Services/Notification/Notification.Infrastructure/Notification.Infrastructure.csproj"

COPY "Services/Identity/Identity.API/Identity.API.csproj" "Services/Identity/Identity.API/Identity.API.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "EducationalPlatform.sln"

COPY . .
WORKDIR "/src/Services/Edu/EducationalInstitution.API"
RUN dotnet publish -c Release --no-restore -o /app

FROM build as unittest
WORKDIR /src/Services/Edu/EducationalInstitution.API.UnitTests

FROM build as integrationtest
WORKDIR /src/Services/Edu/EducationalInstitution.API.IntegrationTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EducationalInstitution.API.dll"]