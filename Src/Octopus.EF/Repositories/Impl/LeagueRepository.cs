using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.EF.Repositories.Impl
{
    public class LeagueRepository : ILeagueRepository, IDisposable
    {
        private readonly OctopusDbContext _context;
        private readonly ILogger<LeagueRepository> _logger;

        public LeagueRepository(OctopusDbContext context, ILogger<LeagueRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddLeagueAsync(League league)
        {
            var exists = await ExistsAsync(league.Id);
            if (exists)
            {
                _logger.LogInformation($"League [{league.Name}] already exists in database");
            }
            else
            {
                if (league.CountryId == 0)
                {
                    _logger.LogInformation($"Country ID is not set for league [{league.Name}]");
                    var existingCountry = await _context.Countries
                                    .FirstOrDefaultAsync(c => c.Name == league.Country.Name);

                    if (existingCountry != null)
                    {
                        // Attach the existing country to the league
                        _logger.LogInformation($"Attaching existing country [{existingCountry.Name}] to league [{league.Name}]");
                        league.CountryId = existingCountry.Id;
                        league.Country = existingCountry;
                    }
                }

                _logger.LogInformation($"Adding league [{league.Name}] to database");
                await _context.Leagues.AddAsync(league);
            }
        }

        public async Task AddLeagueRangeAsync(IEnumerable<League> leagues)
        {
            var leaguesToAdd = new List<League>();

            foreach (var league in leagues)
            {
                var exists = await ExistsAsync(league.Id);
                if (exists)
                {
                    _logger.LogInformation($"League [{league.Name}] already exists in database");
                }
                else
                {
                    if (league.CountryId == 0)
                    {
                        _logger.LogInformation($"Country ID is not set for league [{league.Name}]");
                        var existingCountry = await _context.Countries
                            .FirstOrDefaultAsync(c => c.Name == league.Country.Name);

                        if (existingCountry != null)
                        {
                            // Attach the existing country to the league
                            _logger.LogInformation($"Attaching existing country [{existingCountry.Name}] to league [{league.Name}]");
                            league.CountryId = existingCountry.Id;
                            league.Country = existingCountry;
                        }
                    }

                    leaguesToAdd.Add(league);
                }
            }

            await _context.Leagues.AddRangeAsync(leaguesToAdd);
        }

        public void DeleteLeague(League league)
        {
            _logger.LogInformation($"Deleting league [{league.Name}] from database");
            _context.Leagues.Remove(league);
        }

        public async Task<bool> ExistsAsync(int leagueId)
        {
            return await _context.Leagues.AnyAsync(e => e.Id == leagueId);
        }

        public async Task<League?> GetLeagueByIdAsync(int leagueId)
        {
            _logger.LogInformation($"Getting league by ID from database - [{leagueId}]");
            return await _context.Leagues.FindAsync(leagueId);
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync()
        {
            _logger.LogInformation("Getting leagues from database");
            return await _context.Leagues.ToListAsync();
        }

        public async Task<IEnumerable<League>> GetLeaguesByCountryId(int countryId)
        {
            _logger.LogInformation($"Getting leagues by country ID from database - [{countryId}]");
            return await _context.Leagues.Where(l => l.CountryId == countryId).ToListAsync();
        }

        public void UpdateLeague(League league)
        {
            _logger.LogInformation($"Updating league [{league.Name}] in database");
            _context.Leagues.Update(league);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
