using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Octopus.EF.Data;
using Octopus.EF.Repositories.Interfaces;

namespace Octopus.EF.Repositories.Impl
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly OctopusDbContext _context;
        private readonly ILogger<RepositoryManager> _logger;
        private readonly ICountryRepository _countries;
        private readonly ILeagueRepository _leagues;
        private readonly ITeamRepository _teams;
        private readonly ISystemSettingsRepository _systemSettings;
        private readonly IInstallInfoRepository _installInfo;

        private IDbContextTransaction? _transaction;        

        public ICountryRepository Countries => _countries;
        public ILeagueRepository Leagues => _leagues;
        public ITeamRepository Teams => _teams;
        public ISystemSettingsRepository SystemSettings => _systemSettings;
        public IInstallInfoRepository InstallInfo => _installInfo;

        public RepositoryManager(OctopusDbContext context, 
                                ILogger<RepositoryManager> logger, 
                                ICountryRepository countries, 
                                ILeagueRepository leagues,
                                ITeamRepository teams,
                                ISystemSettingsRepository systemSettings,
                                IInstallInfoRepository installInfo)
        {            
            _context = context;
            _logger = logger;
            _countries = countries;
            _leagues = leagues;
            _teams = teams;
            _systemSettings = systemSettings;
            _installInfo = installInfo;
        }

        public async Task<int> CompleteAsync()
        {
            _logger.LogTrace("Saving changes to database");
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _logger.LogInformation("Starting transaction");
            _transaction = await _context.Database.BeginTransactionAsync();
            _logger.LogInformation("Transaction started");
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                _logger.LogError("Transaction has not been started yet");
                throw new InvalidOperationException("Transaction has not been started yet");
            }

            try
            {
                _logger.LogInformation("Committing transaction");
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                _logger.LogInformation("Transaction committed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while committing transaction");
                _logger.LogInformation("Rolling back transaction");
                await _transaction.RollbackAsync();
                throw new Exception("Error occurred while committing transaction", ex);
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                _logger.LogError("Transaction has not been started yet");
                throw new InvalidOperationException("Transaction has not been started yet");
            }
                
            _logger.LogInformation("Rolling back transaction");
            await _transaction.RollbackAsync();
            _logger.LogInformation("Transaction rolled back");
        }

        public async Task MigrateAsync()
        {
            _logger.LogInformation("Migrating database");
            await _context.Database.MigrateAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
