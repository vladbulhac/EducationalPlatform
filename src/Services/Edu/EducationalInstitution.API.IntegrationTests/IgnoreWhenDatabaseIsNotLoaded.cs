using EducationalInstitution.API.Tests.Shared;
using Xunit;

namespace EducationalInstitution.API.IntegrationTests
{
    /// <summary>
    /// Controls when Integration Tests that use an external database can be triggered
    /// </summary>
    public sealed class IgnoreWhenDatabaseIsNotLoaded : FactAttribute
    {
        public IgnoreWhenDatabaseIsNotLoaded()
        {
            if (!IsDatabaseLoaded())
                Skip = "Ignore when database is not loaded";
        }

        private static bool IsDatabaseLoaded()
        {
            var IS_DBLOADED = ConfigurationHelper.GetCurrentSettings(key: "Tests:IntegrationTestsVariables:IS_DBLOADED", configFilenames: "tests_config.json");
            if (IS_DBLOADED is not null)
                return bool.Parse(IS_DBLOADED);

            return false;
        }
    }
}