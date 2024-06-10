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

    public class ApiLeagueService : IApiLeagueService
    {
        private readonly IApiClientService _apiClient;
        private readonly ILeagueMapper _leagueMapper;
        private readonly ILogger<ApiLeagueService> _logger;
        private readonly string _leaguesEndpoint = "leagues";

        public ApiLeagueService(IApiClientService apiClient, ILeagueMapper leagueMapper, ILogger<ApiLeagueService> logger)
        {
            _apiClient = apiClient;
            _leagueMapper = leagueMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            try
            {
                _logger.LogInformation("Calling API to get leagues..."); 
                var response = await _apiClient.GetAsync(_leaguesEndpoint);

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogError("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                _logger.LogInformation("Parsing JSON response...");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiLeague>>(response);
                if (apiResponse == null || apiResponse.Response == null || apiResponse.Response.Count == 0)
                {
                    throw new Exception("Failed to deserialize API response.");
                }
                _logger.LogInformation("JSON parsing completed.");

                // Map to League objects
                var leagues = new List<League>();
                foreach (var apiLeague in apiResponse.Response)
                {
                    leagues.Add(_leagueMapper.Map(apiLeague));
                }
                _logger.LogInformation("Mapping completed.");

                return leagues;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching leagues: {ex.Message}");
                throw;
            }
        }

        public async Task<League> GetLeagueByIdAsync(int leagueId)
        {
            try
            {
                _logger.LogInformation("Calling API to get league...");
                var response = await _apiClient.GetAsync($"{_leaguesEndpoint}?id={leagueId}");
                _logger.LogInformation("API call completed.");

                if (string.IsNullOrEmpty(response))
                {
                    _logger.LogError("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                _logger.LogInformation("Parsing JSON response...");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiLeague>>(response);
                if (apiResponse == null || apiResponse.Response == null || apiResponse.Response.Count == 0)
                {
                    _logger.LogError("Failed to deserialize API response.");
                    throw new Exception("Failed to deserialize API response.");
                }
                _logger.LogInformation("JSON parsing completed.");

                // Map to League object
                var league = _leagueMapper.Map(apiResponse.Response[0]);
                _logger.LogInformation("Mapping completed.");

                return league;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching league: {ex.Message}");
                throw;
            }
        }
    }
}
