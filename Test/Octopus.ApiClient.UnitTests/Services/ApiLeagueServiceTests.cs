using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Models;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Tests
{
    [TestClass]
    public class ApiLeagueServiceTests
    {
        private Mock<IApiClientService> _mockApiClient;
        private Mock<ILeagueMapper> _mockLeagueMapper;
        private ApiLeagueService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockApiClient = new Mock<IApiClientService>();
            _mockLeagueMapper = new Mock<ILeagueMapper>();
            _service = new ApiLeagueService(_mockApiClient.Object, _mockLeagueMapper.Object);
        }

        [TestMethod]
        public async Task GetLeaguesAsync_ShouldReturnLeagues_WhenApiResponseIsValid()
        {
            // Arrange
            var apiLeagues = new List<ApiLeague>
            {
                new ApiLeague
                {
                    League = new LeagueData { Id = 1, Name = "League1" },
                    Country = new ApiCountry { Name = "Country1" },
                    Seasons = new List<ApiSeason>
                    {
                        new ApiSeason { Year = 2020, Start = "2020-01-01", End = "2020-12-31", Current = true }
                    }
                }
            };
            var response = new ApiResponse<ApiLeague> { Response = apiLeagues };
            var jsonResponse = JsonConvert.SerializeObject(response);

            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(jsonResponse);
            _mockLeagueMapper.Setup(x => x.Map(It.IsAny<ApiLeague>())).Returns(new League { Id = 1, Name = "League1" });

            // Act
            var result = await _service.GetLeaguesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<League>)result).Count);
            Assert.AreEqual(1, ((List<League>)result)[0].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetLeaguesAsync_ShouldThrowException_WhenApiResponseIsNull()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((string)null);

            // Act
            await _service.GetLeaguesAsync();

            // Assert - Exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetLeaguesAsync_ShouldThrowException_WhenApiResponseIsEmpty()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(string.Empty);

            // Act
            await _service.GetLeaguesAsync();

            // Assert - Exception is expected
        }

        [TestMethod]
        public async Task GetLeagueByIdAsync_ShouldReturnLeague_WhenApiResponseIsValid()
        {
            // Arrange
            var apiLeagues = new List<ApiLeague>
            {
                new ApiLeague
                {
                    League = new LeagueData { Id = 1, Name = "League1" },
                    Country = new ApiCountry { Name = "Country1" },
                    Seasons = new List<ApiSeason>
                    {
                        new ApiSeason { Year = 2020, Start = "2020-01-01", End = "2020-12-31", Current = true }
                    }
                }
            };
            var response = new ApiResponse<ApiLeague> { Response = apiLeagues };
            var jsonResponse = JsonConvert.SerializeObject(response);

            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(jsonResponse);
            _mockLeagueMapper.Setup(x => x.Map(It.IsAny<ApiLeague>())).Returns(new League { Id = 1, Name = "League1" });

            // Act
            var result = await _service.GetLeagueByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetLeagueByIdAsync_ShouldThrowException_WhenApiResponseIsNull()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync((string)null);

            // Act
            await _service.GetLeagueByIdAsync(1);

            // Assert - Exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task GetLeagueByIdAsync_ShouldThrowException_WhenApiResponseIsEmpty()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(string.Empty);

            // Act
            await _service.GetLeagueByIdAsync(1);

            // Assert - Exception is expected
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public async Task GetLeagueByIdAsync_ShouldThrowException_WhenDeserializationFails()
        {
            // Arrange
            _mockApiClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync("Invalid JSON");

            // Act
            await _service.GetLeagueByIdAsync(1);

            // Assert - Exception is expected
        }
    }
}
