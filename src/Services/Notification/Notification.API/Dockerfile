#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

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
WORKDIR "/src/Services/Notification/Notification.API"
RUN dotnet build "Notification.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.API.dll"]