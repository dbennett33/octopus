namespace Octopus.ApiClient.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Octopus.ApiClient.Mappers.Interfaces;
    using Octopus.ApiClient.Models;
    using Octopus.ApiClient.Services.Interfaces;
    using Octopus.EF.Data.Entities;

    public class ApiCountryService : IApiCountryService
    {
        private readonly IApiClientService _apiClient;
        private readonly ICountryMapper _countryMapper;
        private readonly ILogger<ApiCountryService> _logger;
        private readonly string _countriesEndpoint = ApiGlobal.Endpoints.COUNTRIES;

        public ApiCountryService(IApiClientService apiClient, ICountryMapper countryMapper, ILogger<ApiCountryService> logger)
        {
            _apiClient = apiClient;
            _countryMapper = countryMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            try
            {
                _logger.LogInformation("Calling API to get countries...");
                var response = await _apiClient.GetAsync(_countriesEndpoint);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogInformation("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                _logger.LogInformation("Parsing JSON response...");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiCountry>>(response);
                if (apiResponse == null || apiResponse.Response == null)
                {
                    throw new Exception("Failed to deserialize API response.");
                }
                _logger.LogInformation("JSON parsing completed.");

                // Map to Country objects
                var countries = _countryMapper.Map(apiResponse.Response);
                _logger.LogInformation("Mapping completed.");
        
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error fetching countries: {ex.Message}");
                throw;
            }
        }
    }
}
