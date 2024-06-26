using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Exceptions;
using Octopus.ApiClient.Services.Interfaces;

namespace Octopus.ApiClient.Services.Impl
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiState _apiState;
        private readonly ILogger<ApiClientService> _logger;
                

        public ApiClientService(HttpClient httpClient,
                                ApiState apiState,
                                ILogger<ApiClientService> logger)
        {
            _httpClient = httpClient;
            _apiState = apiState;
            _logger = logger;
        }

        public int GetRemainingCalls() => _apiState.CallsRemaining;
        public DateTime GetResetTime() => _apiState.ResetTime;

        public async Task<string> GetAsync(string endpoint)
        {
            return await CallApiAsync(endpoint);
        }

        private async Task<string> CallApiAsync(string endpoint)
        {
            string content = string.Empty;
            try
            {
                if (_apiState.CallsRemaining <= 0 && (_apiState.ResetTime + TimeSpan.FromMinutes(2)) > DateTime.Now)
                {
                    _logger.LogError($"Rate limit exceeded. No more requests can be made until {_apiState.ResetTime.ToString()}.");
                    throw new RateLimitExceededException(_apiState.ResetTime);
                }
                else
                {
                    _logger.LogTrace($"Making HTTP GET request to: {endpoint}");

                    var response = await _httpClient.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();

                    UpdateRateLimitInfo(response.Headers);

                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
            }
            catch(RateLimitExceededException e)
            {               
                throw;
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
                if (headers?.TryGetValues(ApiGlobal.ResponseHeaders.NAME_CALL_RESET, out var resetTime) == true)
                {
                    if (int.TryParse(resetTime.FirstOrDefault(), out var reset))
                    {
                        _apiState.ResetTime = DateTime.UtcNow.AddSeconds(reset);
                    }
                    else
                    {
                        _logger.LogError("Error extracting rate limit information from response headers.");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating rate limit information: {e.Message}");
            }
        }
    }
}
