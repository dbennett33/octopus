using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl
{
    using Octopus.EF.Data;

    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly OctopusDbContext _context;
        private readonly ILogger<SystemSettingsRepository> _logger;
        
        public SystemSettingsRepository(OctopusDbContext context, ILogger<SystemSettingsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<SystemSettings> GetSystemSettingsAsync()
        {

            _logger.LogInformation("Getting system settings from database");
            var settings = await _context.SystemSettings.FirstOrDefaultAsync();

            return settings;
        }
        
        public void UpdateSystemSettingsAsync(SystemSettings systemSettings)
        {
            _logger.LogInformation("Updating system settings in database");
            _context.SystemSettings.Update(systemSettings);
        }
        
        public async Task AddSystemSettingsAsync(SystemSettings systemSettings)
        {
            var exists = await GetSystemSettingsAsync();
            if (exists != null)
            {
                _logger.LogInformation("System settings already exist in database");
            }
            else
            {
                _logger.LogInformation("Adding system settings to database");
                await _context.SystemSettings.AddAsync(systemSettings);
            }
        }
        
    }
}