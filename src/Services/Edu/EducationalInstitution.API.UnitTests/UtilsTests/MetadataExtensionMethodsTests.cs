﻿using EducationalInstitutionAPI.Utils;
using Grpc.Core;
using System.Linq;
using Xunit;

namespace EducationalInstitution.API.UnitTests.UtilsTests
{
    public class MetadataExtensionMethodsTests
    {
        [Fact]
        public void GivenAKeyAndValue_MetadataShouldHaveOneEntry()
        {
            //Arrange
            string key_data = "testKey";
            string value_data = "testValue";
            Metadata metadata = new();

            //Act
            metadata.AddMultiple(new (string key, string value)[1] { (key_data, value_data) });

            //Assert
            Assert.Single(metadata);
        }

        [Fact]
        public void GivenAKeyAndValue_ShouldReturnExpectedValue()
        {
            //Arrange
            string key_data = "testKey";
            string value_data = "testValue";
            Metadata metadata = new();

            //Act
            metadata.AddMultiple(new (string key, string value)[1] { (key_data, value_data) });

            //Assert
            Assert.Equal(value_data, metadata.FirstOrDefault(m => m.Key == "testkey").Value);
        }
    }
}