using Microsoft.Extensions.Configuration;

namespace EducationalInstitution.API.Tests.Shared;

/// <summary>
/// Contains methods used to parse JSON files to look for a given key
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// Retrieves the value of <paramref name="key"/> from <paramref name="configFilenames"/>
    /// </summary>
    /// <remarks>
    /// <i>Nested keys can be searched by using ':' , for example GetCurrentSettings("parentKey:descendantKey")</i>
    /// </remarks>
    public static string GetCurrentSettings(string key, params string[] configFilenames)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetParent(currentDirectory).Parent.Parent.FullName)
                            .AddEnvironmentVariables();

        foreach (var config in configFilenames)
            builder.AddJsonFile(config, optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        return configuration.GetValue<string>(key);
    }

    /// <inheritdoc cref="GetCurrentSettings(string, string[])"/>
    public static string GetCurrentSettings(string key, string directory, params string[] configFilenames)
    {
        var builder = new ConfigurationBuilder()
                            .SetBasePath(directory)
                            .AddEnvironmentVariables();

        foreach (var config in configFilenames)
            builder.AddJsonFile(config, optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        return configuration.GetValue<string>(key);
    }
}