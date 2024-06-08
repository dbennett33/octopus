using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl;

public class InstallInfoRepository : IInstallInfoRepository
{
    private readonly OctopusDbContext _context;
    private readonly ILogger<InstallInfoRepository> _logger;    
    
    public InstallInfoRepository(OctopusDbContext context, ILogger<InstallInfoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<List<InstallInfo>> GetAllInstallInfoAsync()
    {
        _logger.LogInformation("Getting all install info from database");
        return await _context.InstallInfo.ToListAsync();
    }   
    
    public async Task<InstallInfo?> GetInstallInfoByIdAsync(int installInfoId)
    {
        _logger.LogInformation($"Getting install info by ID from database - [{installInfoId}]");
        return await _context.InstallInfo.FindAsync(installInfoId);
    }
    
    public void UpdateInstallInfoAsync(InstallInfo installInfo)
    {
        _logger.LogInformation("Updating install info in database");
        _context.InstallInfo.Update(installInfo);
    }
    
    public async Task AddInstallInfoAsync(InstallInfo installInfo)
    {
        if (installInfo.SystemSettings == null)
        {
            _logger.LogError("Install info SystemSettings is null");
        }
        else if (installInfo.SystemSettingsId == 0)
        {
            _logger.LogError("Install info SystemSettingsId is not set");
        }
        else
        {
            _logger.LogInformation("Adding install info to database");
            await _context.InstallInfo.AddAsync(installInfo);
        }        
    }
}