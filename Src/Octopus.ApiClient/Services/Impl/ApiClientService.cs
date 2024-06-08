using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Services.Interfaces;

namespace Octopus.ApiClient.Services.Impl
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClientService> _logger;

        // Rate limit information
        private int _remainingCalls = int.MaxValue;
        private DateTime _rateLimitReset = DateTime.MinValue;

        public ApiClientService(HttpClient httpClient, ILogger<ApiClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public void SetRateLimitInfo(int remainingCalls, DateTime rateLimitReset)
        {
            _remainingCalls = remainingCalls;
            _rateLimitReset = rateLimitReset;
        }

        public int GetRemainingCalls()
        {
            return _remainingCalls;
        }

        public DateTime GetRateLimitReset()
        {
            return _rateLimitReset;
        }

        public async Task<string> GetAsync(string endpoint)
        {
            try
            {
                // Check rate limit before making the call
                if (_remainingCalls <= 0 && DateTime.UtcNow < _rateLimitReset)
                {
                    throw new InvalidOperationException($"Rate limit exceeded. Try again after {_rateLimitReset}.");
                }

                Console.WriteLine($"Making HTTP GET request to: {endpoint}");

                var response = await _httpClient.GetAsync(endpoint);

                Console.WriteLine("HTTP GET request completed.");
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Response status code ensured.");

                // Update rate limit information from headers 
                UpdateRateLimitInfo(response.Headers);

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Read response content as string.");

                return content;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                throw;
            }
        }

        private void UpdateRateLimitInfo(HttpResponseHeaders headers)
        {
            if (headers.Contains("X-RateLimit-Remaining"))
            {
                var remainingCalls = headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
                _remainingCalls = int.Parse(remainingCalls);
                Console.WriteLine($"Remaining calls: {_remainingCalls}");
            }

            if (headers.Contains("X-RateLimit-Reset"))
            {
                var rateLimitReset = headers.GetValues("X-RateLimit-Reset").FirstOrDefault();
                _rateLimitReset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(rateLimitReset)).UtcDateTime;
                Console.WriteLine($"Rate limit reset time: {_rateLimitReset}");
            }
        }
    }
}
