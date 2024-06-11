using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Models;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Services.Impl
{
    public class ApiCountryService : IApiCountryService
    {
        private readonly IApiClientService _apiClient;
        private readonly ICountryMapper _countryMapper;

        private readonly ILogger<ApiCountryService> _logger;
        private readonly string _countriesEndpoint = ApiGlobal.Endpoints.COUNTRIES;

        public ApiCountryService(IApiClientService apiClient,
                                 ICountryMapper countryMapper,
                                 ILogger<ApiCountryService> logger)
        {
            _apiClient = apiClient;
            _countryMapper = countryMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            try
            {
                var response = await _apiClient.GetAsync(_countriesEndpoint);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogError("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiCountry>>(response);
                if (apiResponse == null || apiResponse.Response == null)
                {
                    throw new Exception("Failed to deserialize API response.");
                }

                var countries = _countryMapper.Map(apiResponse.Response);
                
                return countries;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching countries: {ex.Message}");
                throw;
            }
        }
    }
}
