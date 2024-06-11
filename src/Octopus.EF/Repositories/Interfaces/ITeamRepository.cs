using Octopus.EF.Data.Entities;

namespace Octopus.EF.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task AddOrUpdateTeamAsync(Team team);
        void DeleteTeam(Team team);
        Task<Team?> GetTeamByIdAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsAsync();
    }
}