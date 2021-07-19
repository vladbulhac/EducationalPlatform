﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using edu = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.Tests.Shared
{
    /// <summary>
    /// Extracts from a JSON file data that is used in tests
    /// </summary>
    public class TestDataFromJSONParser
    {
        public IList<edu::EducationalInstitution> EducationalInstitutions { get; private set; }

        public TestDataFromJSONParser()
        {
            var testDataPath = ConfigurationHelper.GetCurrentSettings("Tests:ResourcesPaths:EducationalInstitutionsFilePath") ?? throw new Exception("Could not find the test data file path in appsettings.json file!");
            GetEducationalInstitutionsFromJSONFile(testDataPath);
            SetupEducationalInstitutions();
        }

        private void GetEducationalInstitutionsFromJSONFile(string path)
        {
            string file = File.ReadAllText(path);
            dynamic jsonEducationalInstitutions = JsonConvert.DeserializeObject(file);

            ParseTheExtractedDataAndSaveItToList(jsonEducationalInstitutions);
        }

        private void ParseTheExtractedDataAndSaveItToList(dynamic jsonEducationalInstitutions)
        {
            EducationalInstitutions = new List<edu::EducationalInstitution>();
            foreach (var educationalInstitution in jsonEducationalInstitutions.EducationalInstitutions)
            {
                EducationalInstitutions.Add(new((
                    string)educationalInstitution.Name,
                    (string)educationalInstitution.Description,
                    (string)educationalInstitution.LocationID,
                    educationalInstitution.BuildingsIDs.ToObject<List<string>>(),
                    educationalInstitution.AdminsIDs.ToObject<List<Guid>>()
                    ));
            }
        }

        private void SetupEducationalInstitutions()
        {
            EducationalInstitutions[0].SetParentInstitution(EducationalInstitutions[1]);
            EducationalInstitutions[1].SetParentInstitution(EducationalInstitutions[2]);
            EducationalInstitutions[2].SetParentInstitution(EducationalInstitutions[3]);
        }
    }
}