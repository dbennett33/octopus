using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl
{
    public class TeamRepository : ITeamRepository
    {
        private readonly OctopusDbContext _context;
        private readonly ILogger<TeamRepository> _logger;

        public TeamRepository(OctopusDbContext context, ILogger<TeamRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            _logger.LogTrace("Getting teams from database");
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(int teamId)
        {
            _logger.LogTrace($"Getting team by ID from database - [{teamId}]");
            return await _context.Teams.Include(t => t.TeamStats)
                                       .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task AddOrUpdateTeamAsync(Team team)
        {
            var existingTeam = await _context.Teams.FindAsync(team.Id);
            if (existingTeam != null)
            {
                _logger.LogTrace($"Team [{team.Name}] already exists in database - updating");
                _context.Entry(existingTeam).CurrentValues.SetValues(team);
            }
            else
            {
                _logger.LogTrace($"Adding team [{team.Name}] to database");
                await _context.Teams.AddAsync(team);
            }
        }

        public void DeleteTeam(Team team)
        {
            _logger.LogTrace($"Deleting team [{team.Name}] from database");
            _context.Teams.Remove(team);
        }
    }
}
