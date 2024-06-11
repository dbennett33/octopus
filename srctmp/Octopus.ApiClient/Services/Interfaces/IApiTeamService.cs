using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiTeamService
    {
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsByCountryNameAsync(string teamName);
        Task<IEnumerable<Team>> GetTeamsByLeagueIdAsync(int leagueId, string season);
    }
}