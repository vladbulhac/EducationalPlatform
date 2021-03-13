using Microsoft.Extensions.Configuration;

namespace EducationaInstitutionAPI.Utils
{
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