﻿using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EducationalInstitution.API.Tests.UnitTests
{
    /// <summary>
    /// Extracts Data from a JSON file, that is used in tests
    /// </summary>
    public class TestDataFromJSONParser
    {
        public IList<EduInstitution> EduInstitutions { get; set; }

        public TestDataFromJSONParser()
        {
            var testDataPath = ConfigurationHelper.GetCurrentSettings("TestDataFilesPaths:EducationalInstitutionsFilePath") ?? throw new Exception("Could not find the test data file path in appsettings.json file!");
            EduInstitutions = GetEducationalInstitutionsFromJSONFile(testDataPath);
        }

        private static IList<EduInstitution> GetEducationalInstitutionsFromJSONFile(string path)
        {
            string file = File.ReadAllText(path);
            dynamic jsonEduInstitutions = JsonConvert.DeserializeObject(file);

            List<EduInstitution> listOFEduInstitutions = new();
            foreach (var eduInstitution in jsonEduInstitutions.EducationalInstitutions)
            {
                listOFEduInstitutions.Add(new((
                    string)eduInstitution.Name,
                    (string)eduInstitution.Description,
                    (string)eduInstitution.LocationID,
                    eduInstitution.BuildingsIDs.ToObject<List<string>>()
                    ));
            }

            return listOFEduInstitutions;
        }
    }
}