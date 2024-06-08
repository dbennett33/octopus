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
            try
            {
                if (_apiState.CallsRemaining <= 0)
                {
                    throw new InvalidOperationException($"Rate limit exceeded.");
                }

                _logger.LogInformation($"Making HTTP GET request to: {endpoint}");

                var response = await _httpClient.GetAsync(endpoint);          
                response.EnsureSuccessStatusCode();

                // Update rate limit information from headers 
                UpdateRateLimitInfo(response.Headers);

                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError($"Unexpected error: {e.Message}");
                throw;
            }
        }

        private void UpdateRateLimitInfo(HttpResponseHeaders? headers)
        {
            var remainingCalls = headers?.GetValues(ApiGlobal.ResponseHeaders.NAME_CALLS_REMAINING).FirstOrDefault();
            if (remainingCalls != null && int.TryParse(remainingCalls, out var calls))
            {
                _apiState.CallsRemaining = calls;
            }
        }
    }
}
