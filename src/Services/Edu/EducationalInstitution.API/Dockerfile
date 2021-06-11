#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY "EducationalPlatform.sln" "EducationalPlatform.sln"

COPY "EventBus/RabbitMQEventBus/RabbitMQEventBus.csproj" "EventBus/RabbitMQEventBus/RabbitMQEventBus.csproj"
COPY "Validation/DataValidation/DataValidation.csproj" "Validation/DataValidation/DataValidation.csproj"
COPY "Services/DatabaseCleaner/DatabaseCleanerService/DatabaseCleanerService.csproj" "Services/DatabaseCleaner/DatabaseCleanerService/DatabaseCleanerService.csproj"
COPY "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj" "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj"
COPY "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj" "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj"
COPY "Services/Edu/EducationalInstitution.API/EducationalInstitution.API.csproj" "Services/Edu/EducationalInstitution.API/EducationalInstitution.API.csproj"
COPY "APIGateway/Aggregator/Aggregator.csproj" "APIGateway/Aggregator/Aggregator.csproj"
COPY "Services/Notification/Notification.API/Notification.API.csproj" "Services/Notification/Notification.API/Notification.API.csproj"
COPY "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj" "Services/Edu/EducationalInstitution.API.UnitTests/EducationalInstitution.API.UnitTests.csproj"
COPY "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj" "Services/Edu/EducationalInstitution.API.IntegrationTests/EducationalInstitution.API.IntegrationTests.csproj"

RUN dotnet restore "EducationalPlatform.sln"

COPY . .
WORKDIR "/src/Services/Edu/EducationalInstitution.API"
RUN dotnet build "EducationalInstitution.API.csproj" -c Release -o /app/build

FROM build as unittest
WORKDIR /src/Services/Edu/EducationalInstitution.API.UnitTests

FROM build as integrationtest
WORKDIR /src/Services/Edu/EducationalInstitution.API.IntegrationTests

FROM build AS publish
RUN dotnet publish "EducationalInstitution.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EducationalInstitution.API.dll"]
