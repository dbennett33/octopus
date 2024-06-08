using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Services.Interfaces;

namespace Octopus.ApiClient.Services.Impl
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiState _apiState;
        private readonly ILogger<ApiClientService> _logger;

        // Rate limit information
        

        public ApiClientService(HttpClient httpClient, ApiState apiState, ILogger<ApiClientService> logger)
        {
            _httpClient = httpClient;
            _apiState = apiState;
            _logger = logger;
        }

        public int GetRemainingCalls() => _apiState.CallsRemaining;

        public async Task<string> GetAsync(string endpoint)
        {
            string content = string.Empty;
            try
            {
                if (_apiState.CallsRemaining <= 0)
                {
                    _logger.LogWarning("Rate limit exceeded. No more requests can be made.");
                }
                else
                {
                    _logger.LogInformation($"Making HTTP GET request to: {endpoint}");

                    var response = await _httpClient.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();

                    // Update rate limit information from headers 
                    UpdateRateLimitInfo(response.Headers);

                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError($"Unexpected error: {e.Message}");
            }

            return content;     
        }

        public void UpdateRateLimitInfo(HttpResponseHeaders? headers)
        {
            try
            {
                var remainingCalls = headers?.GetValues(ApiGlobal.ResponseHeaders.NAME_CALLS_REMAINING).FirstOrDefault();
                if (remainingCalls != null && int.TryParse(remainingCalls, out var calls))
                {
                    _apiState.CallsRemaining = calls;
                }
                else
                {
                    _logger.LogError("Error extracting rate limit information from response headers.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating rate limit information: {e.Message}");
            }
        }
    }
}
