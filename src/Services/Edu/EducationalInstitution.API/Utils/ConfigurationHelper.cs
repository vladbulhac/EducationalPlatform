using Microsoft.Extensions.Configuration;

namespace EducationalInstitutionAPI.Utils
{
    /// <summary>
    /// Contains a method that searches for a key in the appsettings.json file
    /// </summary>
    public static class ConfigurationHelper
    {
        public static string GetCurrentSettings(string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            return configuration.GetValue<string>(key);
        }
    }
}