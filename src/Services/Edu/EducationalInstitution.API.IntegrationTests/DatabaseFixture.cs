﻿using EducationalInstitution.API.Tests.Shared;
using EducationalInstitution.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EducationalInstitution.API.IntegrationTests;

public class DatabaseFixture : IDisposable
{
    public string DbConnection { get; init; }
    public DataContext Context { get; private set; }
    public readonly TestDataFromJSONParser testDataHelper;

    public DatabaseFixture()
    {
        testDataHelper = new();

        DbConnection = ConfigurationHelper.GetCurrentSettings(key: "ConnectionStrings:IntegrationTestsDB",
                                                              directory: GetApplicationPath(),
                                                              "appsettings.json",
                                                              "appsettings.Development.json") ?? throw new Exception("Could not find the database connection string used for testing!");

        SetupContext();

        CleanupDatabase();
        SeedDatabase(testDataHelper);
    }

    private string GetApplicationPath()
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        return Path.Combine(Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName, "EducationalInstitution.API");
    }

    private void SetupContext()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
                       .UseSqlServer(DbConnection, providerOptions => providerOptions.EnableRetryOnFailure(1))
                       .EnableSensitiveDataLogging();

        Context = new(dbOptions.Options);
        Context.Database.Migrate();
    }

    private void SeedDatabase(TestDataFromJSONParser testDataHelper)
    {
        foreach (var educationalInstitution in testDataHelper.EducationalInstitutions)
            Context.EducationalInstitutions.Add(educationalInstitution);

        Context.SaveChanges();
    }

    private void CleanupDatabase()
    {
        var testBuilding = Context.Buildings.ToList();
        Context.RemoveRange(testBuilding);

        var testAdmins = Context.Admins.ToList();
        Context.RemoveRange(testAdmins);

        var testEducationalInstitutions = Context.EducationalInstitutions.ToList();
        Context.RemoveRange(testEducationalInstitutions);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        CleanupDatabase();
    }
}