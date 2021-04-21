using EducationalInstitutionAPI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EducationalInstitution.API.Tests.UnitTests
{
    /// <summary>
    /// Extracts from a JSON file data that is used in tests
    /// </summary>
    public class TestDataFromJSONParser
    {
        public IList<EducationalInstitutionAPI.Data.EducationalInstitution> EducationalInstitutions { get; set; }

        public TestDataFromJSONParser()
        {
            var testDataPath = ConfigurationHelper.GetCurrentSettings("TestDataFilesPaths:EducationalInstitutionsFilePath") ?? throw new Exception("Could not find the test data file path in appsettings.json file!");
            EducationalInstitutions = GetEducationalInstitutionsFromJSONFile(testDataPath);
        }

        private static IList<EducationalInstitutionAPI.Data.EducationalInstitution> GetEducationalInstitutionsFromJSONFile(string path)
        {
            string file = File.ReadAllText(path);
            dynamic jsonEducationalInstitutions = JsonConvert.DeserializeObject(file);

            List<EducationalInstitutionAPI.Data.EducationalInstitution> listOFEducationalInstitutions = new();
            foreach (var educationalInstitution in jsonEducationalInstitutions.EducationalInstitutions)
            {
                listOFEducationalInstitutions.Add(new((
                    string)educationalInstitution.Name,
                    (string)educationalInstitution.Description,
                    (string)educationalInstitution.LocationID,
                    educationalInstitution.BuildingsIDs.ToObject<List<string>>()
                    ));
            }

            return listOFEducationalInstitutions;
        }
    }
}