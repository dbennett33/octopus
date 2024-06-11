using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octopus.ApiClient.Mappers.Impl;
using Octopus.ApiClient.Models;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Tests
{
    [TestClass]
    public class CountryMapperTests
    {
        private CountryMapper _mapper;

        [TestInitialize]
        public void Setup()
        {
            _mapper = new CountryMapper();
        }

        [TestMethod]
        public void Map_ShouldReturnEmptyList_WhenApiCountriesIsNull()
        {
            // Arrange
            IEnumerable<ApiCountry> apiCountries = null;

            // Act
            var result = _mapper.Map(apiCountries);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, ((List<Country>)result).Count);
        }

        [TestMethod]
        public void Map_ShouldReturnEmptyList_WhenApiCountriesIsEmpty()
        {
            // Arrange
            var apiCountries = new List<ApiCountry>();

            // Act
            var result = _mapper.Map(apiCountries);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, ((List<Country>)result).Count);
        }

        [TestMethod]
        public void Map_ShouldMapApiCountriesToCountries()
        {
            // Arrange
            var apiCountries = new List<ApiCountry>
            {
                new ApiCountry { Name = "Country1", Code = "C1", Flag = "Flag1" },
                new ApiCountry { Name = "Country2", Code = "C2", Flag = "Flag2" }
            };

            // Act
            var result = _mapper.Map(apiCountries);

            // Assert
            Assert.IsNotNull(result);
            var resultList = (List<Country>)result;
            Assert.AreEqual(2, resultList.Count);
            Assert.AreEqual("Country1", resultList[0].Name);
            Assert.AreEqual("C1", resultList[0].Code);
            Assert.AreEqual("Flag1", resultList[0].Flag);
            Assert.AreEqual("Country2", resultList[1].Name);
            Assert.AreEqual("C2", resultList[1].Code);
            Assert.AreEqual("Flag2", resultList[1].Flag);
        }

        [TestMethod]
        public void Map_ShouldHandleNullPropertiesInApiCountries()
        {
            // Arrange
            var apiCountries = new List<ApiCountry>
            {
                new ApiCountry { Name = null, Code = null, Flag = null }
            };

            // Act
            var result = _mapper.Map(apiCountries);

            // Assert
            Assert.IsNotNull(result);
            var resultList = (List<Country>)result;
            Assert.AreEqual(1, resultList.Count);
            Assert.AreEqual(string.Empty, resultList[0].Name);
            Assert.AreEqual(string.Empty, resultList[0].Code);
            Assert.AreEqual(string.Empty, resultList[0].Flag);
        }
    }
}
