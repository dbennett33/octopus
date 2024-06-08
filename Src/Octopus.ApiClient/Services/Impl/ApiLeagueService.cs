namespace Octopus.ApiClient.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Octopus.ApiClient.Mappers.Interfaces;
    using Octopus.ApiClient.Models;
    using Octopus.ApiClient.Services.Interfaces;
    using Octopus.EF.Data.Entities;

    public class ApiLeagueService : IApiLeagueService
    {
        private readonly IApiClientService _apiClient;
        private readonly ILeagueMapper _leagueMapper;
        private readonly string _leaguesEndpoint = "leagues";

        public ApiLeagueService(IApiClientService apiClient, ILeagueMapper leagueMapper)
        {
            _apiClient = apiClient;
            _leagueMapper = leagueMapper;
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            try
            {
                Console.WriteLine("Calling API to get leagues...");
                var response = await _apiClient.GetAsync(_leaguesEndpoint);
                Console.WriteLine("API call completed.");

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                Console.WriteLine("Parsing JSON response...");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiLeague>>(response);
                if (apiResponse == null || apiResponse.Response == null || apiResponse.Response.Count == 0)
                {
                    throw new Exception("Failed to deserialize API response.");
                }
                Console.WriteLine("JSON parsing completed.");

                // Map to League objects
                var leagues = new List<League>();
                foreach (var apiLeague in apiResponse.Response)
                {
                    leagues.Add(_leagueMapper.Map(apiLeague));
                }
                Console.WriteLine("Mapping completed.");

                return leagues;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching leagues: {ex.Message}");
                throw;
            }
        }

        public async Task<League> GetLeagueByIdAsync(int leagueId)
        {
            try
            {
                Console.WriteLine("Calling API to get league...");
                var response = await _apiClient.GetAsync($"{_leaguesEndpoint}?id={leagueId}");
                Console.WriteLine("API call completed.");

                if (string.IsNullOrEmpty(response))
                {
                    Console.WriteLine("API response is null or empty.");
                    throw new Exception("API response is null or empty");
                }

                Console.WriteLine("Parsing JSON response...");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<ApiLeague>>(response);
                if (apiResponse == null || apiResponse.Response == null || apiResponse.Response.Count == 0)
                {
                    throw new Exception("Failed to deserialize API response.");
                }
                Console.WriteLine("JSON parsing completed.");

                // Map to League object
                var league = _leagueMapper.Map(apiResponse.Response[0]);
                Console.WriteLine("Mapping completed.");

                return league;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching league: {ex.Message}");
                throw;
            }
        }
    }
}
