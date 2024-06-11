using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;

namespace Octopus.ApiClient.Tests
{
    [TestClass]
    public class ApiClientServiceTests
    {
        private Mock<HttpMessageHandler>? _httpMessageHandlerMock;
        private HttpClient? _httpClient;
        private ApiState? _apiState;
        private Mock<ILogger<ApiClientService>>? _loggerMock;
        private ApiClientService? _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _apiState = new ApiState { CallsRemaining = 100 };
            _loggerMock = new Mock<ILogger<ApiClientService>>();
            _service = new ApiClientService(_httpClient, _apiState, _loggerMock.Object);
        }

        [TestMethod]
        public void GetRemainingCalls_ShouldReturnCorrectValue()
        {
            // Arrange
            _apiState!.CallsRemaining = 50;

            // Act
            var result = _service!.GetRemainingCalls();

            // Assert
            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public async Task GetAsync_ShouldLogWarning_WhenRateLimitExceeded()
        {
            // Arrange
            _apiState!.CallsRemaining = 0;
            var endpoint = "http://example.com";

            // Act
            var result = await _service!.GetAsync(endpoint);

            // Assert
            Assert.AreEqual(string.Empty, result);
            _loggerMock!.Verify(
                x => x.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v != null && v.ToString()!.Contains("Rate limit exceeded. No more requests can be made.")),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
                ),
                Times.Once
            );
        }


        [TestMethod]
        public async Task GetAsync_ShouldReturnContent_WhenRequestIsSuccessful()
        {
            // Arrange
            var endpoint = "http://example.com";
            var expectedContent = "response content";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedContent)
            };

            _httpMessageHandlerMock!
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            // Act
            var result = await _service!.GetAsync(endpoint);

            // Assert
            Assert.AreEqual(expectedContent, result);
        }

        [TestMethod]
        public async Task GetAsync_ShouldUpdateRateLimitInfo_WhenHeadersArePresent()
        {
            // Arrange
            var endpoint = "http://example.com";
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("response content")
            };
            httpResponseMessage.Headers.Add(ApiGlobal.ResponseHeaders.NAME_CALLS_REMAINING, "50");

            _httpMessageHandlerMock!
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponseMessage);

            // Act
            await _service!.GetAsync(endpoint);

            // Assert
            Assert.AreEqual(50, _service.GetRemainingCalls());
        } 

        [TestMethod]
        public void UpdateRateLimitInfo_ShouldUpdateCallsRemaining_WhenHeadersArePresent()
        {
            // Arrange
            var headers = new HttpResponseMessage(HttpStatusCode.OK).Headers;
            headers.Add(ApiGlobal.ResponseHeaders.NAME_CALLS_REMAINING, "50");

            // Act
            _service!.UpdateRateLimitInfo(headers);

            // Assert
            Assert.AreEqual(50, _apiState!.CallsRemaining);
        }

        [TestMethod]
        public void UpdateRateLimitInfo_ShouldLogError_WhenInvalidRateLimitValueProvided()
        {
            // Arrange
            var headers = new HttpResponseMessage(HttpStatusCode.OK).Headers;
            headers.Add(ApiGlobal.ResponseHeaders.NAME_CALLS_REMAINING, "invalid");

            // Act
            _service!.UpdateRateLimitInfo(headers);

            // Assert
            _loggerMock!.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Error extracting rate limit information from response headers.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }
    }
}
