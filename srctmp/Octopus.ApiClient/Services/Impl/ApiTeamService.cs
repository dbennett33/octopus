using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.ApiClient.Models;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Services.Impl
{
    public class ApiTeamService : IApiTeamService
    {
        private readonly IApiClientService _apiClient;
        private readonly ITeamMapper _teamMapper;

        private readonly ILogger<ApiTeamService> _logger;
        private readonly string _teamsEndpoint = ApiGlobal.Endpoints.TEAMS;

        public ApiTeamService(IApiClientService apiClient,
                              ITeamMapper teamMapper,
                              ILogger<ApiTeamService> logger)
        {
            _apiClient = apiClient;
            _teamMapper = teamMapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> GetTeamsByLeagueIdAsync(int leagueId, string season)
        {
            var endpoint = $"{_teamsEndpoint}?league={leagueId}&season={season}";
            var response = await _apiClient.GetAsync(endpoint);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogError("API response is null or empty.");
                throw new Exception("API response is null or empty");
            }

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiTeam>>(response);
            if (apiResponse == null || apiResponse.Response == null)
            {
                _logger.LogError("Failed to deserialize API response.");
                throw new Exception("Failed to deserialize API response.");
            }

            var teams = new List<Team>();
            foreach (var apiTeam in apiResponse.Response)
            {
                teams.Add(_teamMapper.Map(apiTeam));
            }

            return teams;
        }

        public async Task<IEnumerable<Team>> GetTeamsByCountryNameAsync(string teamName)
        {
            var endpoint = $"{_teamsEndpoint}?name={teamName}";
            var response = await _apiClient.GetAsync(endpoint);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogError("API response is null or empty.");
                throw new Exception("API response is null or empty");
            }

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiTeam>>(response);
            if (apiResponse == null || apiResponse.Response == null)
            {
                _logger.LogError("Failed to deserialize API response.");
                throw new Exception("Failed to deserialize API response.");
            }

            var teams = new List<Team>();
            foreach (var apiTeam in apiResponse.Response)
            {
                teams.Add(_teamMapper.Map(apiTeam));
            }

            return teams;
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var endpoint = $"{_teamsEndpoint}?id={teamId}";
            var response = await _apiClient.GetAsync(endpoint);

            if (string.IsNullOrEmpty(response))
            {
                _logger.LogError("API response is null or empty.");
                throw new Exception("API response is null or empty");
            }

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiTeam>>(response);
            if (apiResponse == null || apiResponse.Response == null)
            {
                _logger.LogError("Failed to deserialize API response.");
                throw new Exception("Failed to deserialize API response.");
            }

            var team = _teamMapper.Map(apiResponse.Response[0]);

            return team;
        }
    }
}
