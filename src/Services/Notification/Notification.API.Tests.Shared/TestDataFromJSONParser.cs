using Newtonsoft.Json;
using Notification.Domain.Models.Aggregates;

namespace Notification.API.Tests.Shared;

public abstract class TestDataReader
{
    protected string filePath;

    protected TestDataReader(string filePath = "Resources//Events.examples.json") => this.filePath = !string.IsNullOrEmpty(filePath) ? filePath : throw new ArgumentNullException(nameof(filePath));

    protected abstract dynamic GetDataFromFile();

    protected abstract void ParseTheExtractedDataFromFile(dynamic data);
}

/// <summary>
/// Extracts from a JSON file data that is used in tests
/// </summary>
public class JSONDataParser : TestDataReader
{
    public List<Event> Events { get; private set; }

    public JSONDataParser() : base(ConfigurationHelper.GetCurrentSettings("Tests:ResourcesPaths:EventsFilePath", configFilenames: "tests_config.json"))
    {
        Events = new();
        ParseTheExtractedDataFromFile(GetDataFromFile());
    }

    protected override dynamic GetDataFromFile()
    {
        string data = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject(data);
    }

    protected override void ParseTheExtractedDataFromFile(dynamic data)
    {
        foreach (var @event in data.Events)
        {
            var recipients = new string[3] { "fixed-recipient-id",
                                             Guid.NewGuid().ToString(),
                                             Guid.NewGuid().ToString() };

            Events.Add(new((string)@event.Name,
                           (string)@event.Message,
                           (string)@event.Uri,
                           (DateTime)@event.TimeIssued,
                           (string)@event.TriggeredByAction,
                           (string)@event.IssuedBy,
                           recipients,
                           (string)@event.Id));
        }
    }
}