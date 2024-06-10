using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using NLog;
using NLog.Extensions.Logging;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Models;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Tests
{
    [TestClass]
    public class ApiCountryServiceTests
    {
        private Mock<IApiClientService>? _apiClientMock;
        private Mock<ICountryMapper>? _countryMapperMock;
        private ApiCountryService? _service;
        private ILogger<ApiCountryService>? _logger;
        private IServiceProvider? _serviceProvider;
        private StringWriter? _consoleOutput;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiClientMock = new Mock<IApiClientService>();
            _countryMapperMock = new Mock<ICountryMapper>();

            // Configure NLog to log to the console
            _serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    loggingBuilder.AddNLog("nlog.config");
                })
                .BuildServiceProvider();

            var factory = _serviceProvider.GetService<ILoggerFactory>();
            _logger = factory!.CreateLogger<ApiCountryService>();

            _service = new ApiCountryService(_apiClientMock.Object, _countryMapperMock.Object, _logger);

            // Capture console output
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            LogManager.Shutdown();  // Flush and dispose NLog

            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }

            // Restore console output
            _consoleOutput!.Dispose();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public async Task GetCountriesAsync_ShouldReturnCountries_WhenApiResponseIsValid()
        {
            // Arrange
            var apiResponse = new ApiResponse<ApiCountry>
            {
                Response = new List<ApiCountry>
                {
                    new ApiCountry { Name = "Country1" },
                    new ApiCountry { Name = "Country2" }
                }
            };
            var apiResponseJson = JsonConvert.SerializeObject(apiResponse);
            _apiClientMock!.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(apiResponseJson);

            var mappedCountries = new List<Country>
            {
                new Country { Name = "Country1" },
                new Country { Name = "Country2" }
            };
            _countryMapperMock!.Setup(x => x.Map(It.IsAny<IEnumerable<ApiCountry>>())).Returns(mappedCountries);

            // Act
            var result = await _service!.GetCountriesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Country1", result.ElementAt(0).Name);
            Assert.AreEqual("Country2", result.ElementAt(1).Name);

            // Check logs
            var logs = _consoleOutput!.ToString();
            //Assert.IsTrue(logs.Contains("Calling API to get countries..."));
            //Assert.IsTrue(logs.Contains("JSON parsing completed."));
        }

        [TestMethod]
        public async Task GetCountriesAsync_ShouldThrowException_WhenApiResponseIsEmpty()
        {
            // Arrange
            _apiClientMock!.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(string.Empty);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service!.GetCountriesAsync());
            Assert.AreEqual("API response is null or empty", ex.Message);

            // Check logs
            var logs = _consoleOutput!.ToString();
            Assert.IsTrue(logs.Contains("API response is null or empty."));
        }

        [TestMethod]
        public async Task GetCountriesAsync_ShouldThrowException_WhenApiResponseIsNull()
        {
            // Arrange
            _apiClientMock!.Setup(x => x.GetAsync(It.IsAny<string>()))!.ReturnsAsync((string)null!);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service!.GetCountriesAsync());
            Assert.AreEqual("API response is null or empty", ex.Message);

            // Check logs
            var logs = _consoleOutput!.ToString();
            Assert.IsTrue(logs.Contains("API response is null or empty."));
        }

        [TestMethod]
        public async Task GetCountriesAsync_ShouldThrowException_WhenJsonParsingFails()
        {
            // Arrange
            _apiClientMock!.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync("invalid json");

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<JsonReaderException>(() => _service!.GetCountriesAsync());
            Assert.IsTrue(ex.Message.Contains("Unexpected character encountered"));

            // Check logs
            var logs = _consoleOutput!.ToString();
            //Assert.IsTrue(logs.Contains("Parsing JSON response..."));
        }

        [TestMethod]
        public async Task GetCountriesAsync_ShouldLogAndThrowException_WhenErrorOccurs()
        {
            // Arrange
            _apiClientMock!.Setup(x => x.GetAsync(It.IsAny<string>())).ThrowsAsync(new Exception("API error"));

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _service!.GetCountriesAsync());
            Assert.AreEqual("API error", ex.Message);

            // Check logs
            var logs = _consoleOutput!.ToString();
            Assert.IsTrue(logs.Contains("Error fetching countries: API error"));
        }
    }
}
