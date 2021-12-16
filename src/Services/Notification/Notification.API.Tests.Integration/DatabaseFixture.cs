using Microsoft.EntityFrameworkCore;
using Notification.API.Tests.Shared;
using Notification.Infrastructure;

namespace Notification.API.Tests.Integration;

public class DatabaseFixture : IDisposable
{
    public string DbConnection { get; init; }
    public NotificationContext Context { get; private set; }
    public readonly JSONDataParser testDataHelper;

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

        return Path.Combine(Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName, "Notification.API");
    }

    private void SetupContext()
    {
        var dbOptions = new DbContextOptionsBuilder<NotificationContext>()
                       .UseSqlServer(DbConnection, providerOptions => providerOptions.EnableRetryOnFailure(1))
                       .EnableSensitiveDataLogging();

        Context = new(dbOptions.Options);
        Context.Database.Migrate();
    }

    private void SeedDatabase(JSONDataParser testDataHelper)
    {
        foreach (var @event in testDataHelper.Events)
            Context.Events.Add(@event);

        Context.SaveChanges();
    }

    private void CleanupDatabase()
    {
        var testEducationalInstitutions = Context.Events.ToList();
        Context.RemoveRange(testEducationalInstitutions);

        var testBuilding = Context.Recipients.ToList();
        Context.RemoveRange(testBuilding);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        CleanupDatabase();
    }
}