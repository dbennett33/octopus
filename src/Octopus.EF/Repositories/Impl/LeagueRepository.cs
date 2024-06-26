﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

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

        public async Task AddOrUpdateLeagueAsync(League league)
        {
            if (league.CountryId == 0)
            {
                _logger.LogTrace($"Country ID is not set for league [{league.Name}]");
                var existingCountry = await _context.Countries
                    .FirstOrDefaultAsync(c => c.Name == (league.Country != null ? league.Country.Name : null));

                if (existingCountry != null)
                {
                    // Attach the existing country to the league
                    _logger.LogTrace($"Attaching existing country [{existingCountry.Name}] to league [{league.Name}]");
                    league.CountryId = existingCountry.Id;
                    league.Country = existingCountry;
                }
            }

            var existingLeague = await _context.Leagues.FindAsync(league.Id);
            if (existingLeague != null)
            {
                _logger.LogTrace($"League [{league.Name}] already exists in database - updating");
                _context.Entry(existingLeague).CurrentValues.SetValues(league);
            }
            else
            {
                _logger.LogTrace($"Adding league [{league.Name}] to database");
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
                    _logger.LogTrace($"League [{league.Name}] already exists in database");
                }
                else
                {
                    if (league.CountryId == 0)
                    {
                        _logger.LogTrace($"Country ID is not set for league [{league.Name}]");
                        var existingCountry = await _context.Countries
                            .FirstOrDefaultAsync(c => c.Name == (league.Country != null ? league.Country.Name : null));

                        if (existingCountry != null)
                        {
                            // Attach the existing country to the league
                            _logger.LogTrace($"Attaching existing country [{existingCountry.Name}] to league [{league.Name}]");
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
            _logger.LogTrace($"Deleting league [{league.Name}] from database");
            _context.Leagues.Remove(league);
        }

        public async Task<bool> ExistsAsync(int leagueId)
        {
            return await _context.Leagues.AnyAsync(e => e.Id == leagueId);
        }

        public async Task<League?> GetLeagueByIdAsync(int leagueId)
        {
            _logger.LogTrace($"Getting league by ID from database - [{leagueId}]");
            return await _context.Leagues.FindAsync(leagueId);
        }

        public async Task<IEnumerable<League>> GetLeaguesAsync(bool enabledOnly = false)
        {
            _logger.LogTrace("Getting leagues from database");

            var leagues = new List<League>();
            if (enabledOnly)
            {
                leagues = await _context.Leagues.Where(l => l.IsEnabled).ToListAsync();
            }
            else
            {
                leagues = await _context.Leagues.ToListAsync();
            }

            return leagues;
        }

        public async Task<IEnumerable<League>> GetLeaguesByCountryId(int countryId)
        {
            _logger.LogTrace($"Getting leagues by country ID from database - [{countryId}]");
            return await _context.Leagues.Where(l => l.CountryId == countryId).ToListAsync();
        }

        public void UpdateLeague(League league)
        {
            _logger.LogTrace($"Updating league [{league.Name}] in database");
            _context.Leagues.Update(league);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
