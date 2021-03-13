using EducationaInstitutionAPI.Data;
using EducationaInstitutionAPI.Utils;
using EducationaInstitutionAPI.Utils.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EducationalInstitution.API.Tests.UnitTests
{
    public class TestDataFromJSONParser
    {
        public IList<EduInstitution> EduInstitutions { get; set; }

        public TestDataFromJSONParser()
        {
            var testDataPath = ConfigurationHelper.GetCurrentSettings("TestDataFilesPaths:EducationalInstitutionsFilePath") ?? throw new Exception("Could not find the test data file path in appsettings.json file!");
            EduInstitutions = ReadEducationalInstitutionsFromJSONFile(testDataPath);

            EduInstitutions[0].AddStudent(new(Guid.NewGuid(), "2011-10-30", "2014-01-07", EduInstitutions[0], Year.X));
            EduInstitutions[0].AddProfessor(new(Guid.NewGuid(), Rank.Lecturer, "2005-05-05", new List<EduInstitution>() { EduInstitutions[0], EduInstitutions[0] }, "office112"));
            EduInstitutions[0].AddStaff(new(Rank.HeadOfDepartment, Guid.NewGuid(), EduInstitutions[0]));
        }

        private static IList<EduInstitution> ReadEducationalInstitutionsFromJSONFile(string path)
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
                    (string)eduInstitution.BuildingID
                    ));
            }

            return listOFEduInstitutions;
        }
    }
}