﻿using Newtonsoft.Json;
using Domain = EducationalInstitution.Domain.Models.Aggregates;

namespace EducationalInstitution.API.Tests.Shared;

/// <summary>
/// Extracts from a JSON file data that is used in tests
/// </summary>
public class TestDataFromJSONParser
{
    public IList<Domain::EducationalInstitution> EducationalInstitutions { get; private set; }

    public TestDataFromJSONParser()
    {
        var testDataPath = ConfigurationHelper.GetCurrentSettings("Tests:ResourcesPaths:EducationalInstitutionsFilePath",
                                                                  configFilenames: "tests_config.json") ?? throw new Exception("Could not find the test data file path in appsettings.json file!");
        GetEducationalInstitutionsFromJSONFile(testDataPath);
        SetupEducationalInstitutions();
    }

    private void GetEducationalInstitutionsFromJSONFile(string path = "Resources\\EducationalInstitutions.examples.json")
    {
        string data = File.ReadAllText(path);
        dynamic jsonEducationalInstitutions = JsonConvert.DeserializeObject(data);

        ParseTheExtractedDataAndSaveItToList(jsonEducationalInstitutions);
    }

    private void ParseTheExtractedDataAndSaveItToList(dynamic jsonEducationalInstitutions)
    {
        EducationalInstitutions = new List<Domain::EducationalInstitution>();
        foreach (var educationalInstitution in jsonEducationalInstitutions.EducationalInstitutions)
        {
            EducationalInstitutions.Add(new((
                string)educationalInstitution.Name,
                (string)educationalInstitution.Description,
                (string)educationalInstitution.LocationID,
                educationalInstitution.BuildingsIDs.ToObject<List<string>>(),
                (string)educationalInstitution.AdminId
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