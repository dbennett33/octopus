using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl;

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
        _logger.LogInformation("Getting countries from database");
        return await _context.Countries.ToListAsync();
    }

    public async Task<Country?> GetCountryByIdAsync(int countryId)
    {
        _logger.LogInformation($"Getting country by ID from database - [{countryId}]");
        return await _context.Countries.FindAsync(countryId);
    }

    public async Task<Country?> GetCountryByCodeAsync(string countryCode)
    {
        _logger.LogInformation($"Getting country by code from database - [{countryCode}]");
        return await _context.Countries.FirstOrDefaultAsync(c => c.Code == countryCode);
    }

    public async Task AddCountryAsync(Country country)
    {
        var exists = await ExistsAsync(country.Name);
        if (exists)
        {
            _logger.LogInformation($"Country [{country.Name}] already exists in database");
        }
        else
        {             
            _logger.LogInformation($"Adding country [{country.Name}] to database");
            await _context.Countries.AddAsync(country);
        }
    }
    
    public async Task AddCountryRangeAsync(IEnumerable<Country> countries)
    {
        var countriesToAdd = new List<Country>();
        foreach (var country in countries)
        {
            if (!await ExistsAsync(country.Name))
            {
                countriesToAdd.Add((country));
            }
        }

        await _context.Countries.AddRangeAsync((countriesToAdd));
    }


    public void UpdateCountry(Country country)
    {
        _logger.LogInformation($"Updating country [{country.Name}] in database");
        _context.Countries.Update(country);
    }

    public void DeleteCountry(Country country)
    {
        _logger.LogInformation($"Deleting country [{country.Name}] from database");
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
        _logger.LogInformation($"Getting country by name from database - [{countryName}]");
        return _context.Countries.FirstOrDefaultAsync(c => c.Name == countryName);
    }
}