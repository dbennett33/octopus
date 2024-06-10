using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl
{
    public class CountryRepository : ICountryRepository, IDisposable
    {
        private readonly OctopusDbContext _context;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(OctopusDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            _logger.LogTrace("Getting countries from database");
            return await _context.Countries.ToListAsync();
        }

        public async Task<IEnumerable<Country>> GetCountriesIncludeLeaguesAsync()
        {
            _logger.LogTrace("Getting countries from database");
            return await _context.Countries.Include(c => c.Leagues).ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int countryId)
        {
            _logger.LogTrace($"Getting country by ID from database - [{countryId}]");
            return await _context.Countries.FindAsync(countryId);
        }

        public async Task<Country?> GetCountryByCodeAsync(string countryCode)
        {
            _logger.LogTrace($"Getting country by code from database - [{countryCode}]");
            return await _context.Countries.FirstOrDefaultAsync(c => c.Code == countryCode);
        }

        public async Task AddOrUpdateCountryAsync(Country country)
        {
            var existingCountry = await _context.Countries.FirstOrDefaultAsync(c => c.Name == country.Name);
            if (existingCountry != null)
            {
                // need to set this here because the existing country will have autonumbered ID - api country will be 0
                country.Id = existingCountry.Id;

                _logger.LogTrace($"Country [{country.Name}] already exists in database - updating");
                _context.Entry(existingCountry).CurrentValues.SetValues(country);
            }
            else
            {             
                _logger.LogTrace($"Adding country [{country.Name}] to database");
                await _context.Countries.AddAsync(country);
            }
        }   

        public void DeleteCountry(Country country)
        {
            _logger.LogTrace($"Deleting country [{country.Name}] from database");
            _context.Countries.Remove(country);
        }
        public async Task<bool> ExistsAsync(string countryName)
        {
            return await _context.Countries.AnyAsync(e => e.Name == countryName);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<Country?> GetCountryByNameAsync(string countryName)
        {
            _logger.LogTrace($"Getting country by name from database - [{countryName}]");
            return _context.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
        }

        public Task<Country?> GetCountryByNameIncludeLeaguesAsync(string countryName)
        {
            _logger.LogTrace($"Getting country by name from database - [{countryName}]");
            return _context.Countries.Include(c => c.Leagues).FirstOrDefaultAsync(c => c.Name == countryName);
        }
    }
}