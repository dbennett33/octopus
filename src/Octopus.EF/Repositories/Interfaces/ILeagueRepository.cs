using Octopus.EF.Data.Entities;

namespace Octopus.EF.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        Task AddOrUpdateLeagueAsync(League league);
        Task AddLeagueRangeAsync(IEnumerable<League> leagues);
        void DeleteLeague(League league);
        Task<bool> ExistsAsync(int leagueId);
        Task<League?> GetLeagueByIdAsync(int leagueId);
        Task<IEnumerable<League>> GetLeaguesAsync(bool enabledOnly);
        Task<IEnumerable<League>> GetLeaguesByCountryId(int countryId);
        void UpdateLeague(League league);
    }
}