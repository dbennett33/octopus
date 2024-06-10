using Microsoft.Extensions.Options;
using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Sync.Configurations;
using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Services.Impl;

public class InitializerService : IInitializerService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IApiClientService _apiClientService;
    private readonly IInstallerService _installerService;
    private readonly EnabledEntitiesConfig _enabledEntitiesConfig;
    private readonly ILogger<InitializerService> _logger;

    private SystemSettings? _systemSettings;
    private InstallInfo? _installInfo;
    private bool _needsInstall = true;

    public InitializerService(IRepositoryManager repositoryManager, IApiClientService apiClientService, IInstallerService installerService, IOptions<EnabledEntitiesConfig> enabledEntitiesConfig, ILogger<InitializerService> logger)
    {
        _repositoryManager = repositoryManager;
        _apiClientService = apiClientService;
        _installerService = installerService;
        _enabledEntitiesConfig = enabledEntitiesConfig.Value;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        await InitDatabase();
        await InitSystemSettings();        

        // Need to install Countries and Leagues before we can initialize enabled entities
        if (_needsInstall)
        {
            _installInfo = await _installerService.InstallStageOne(_installInfo!);
        }

        await InitEnabledEntities();

        if (_needsInstall)
        {

        }
    }

    private async Task InitSystemSettings()
    {
        try
        {
            var systemSettings = await _repositoryManager.SystemSettings.GetSystemSettingsAsync();

            if (systemSettings == null)
            {
                systemSettings = new SystemSettings();
                systemSettings.CurrentVersion = "0.0.1";

                await _repositoryManager.SystemSettings.AddSystemSettingsAsync(systemSettings);
                 await _repositoryManager.CompleteAsync();
            }

            if (systemSettings != null)
            {
                _systemSettings = systemSettings;
                _logger.LogInformation($"System exists - v{_systemSettings.CurrentVersion}");

                var installData = await _repositoryManager.InstallInfo.GetAllInstallInfoAsync();

                if (installData.Count != 0)
                {
                    ScanInstallData(installData);
                }
                else
                {
                    _logger.LogInformation("No InstallData found - install required");
                    _installInfo = new InstallInfo();
                    _installInfo.SystemSettings = systemSettings;
                    _installInfo.SystemSettingsId = _systemSettings.Id;
                    _installInfo.Version = "0.0.1";

                    await _repositoryManager.InstallInfo.AddInstallInfoAsync(_installInfo);
                    await _repositoryManager.CompleteAsync();
                }
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize system settings");
            throw;
        }
    }

    private void ScanInstallData(List<InstallInfo> installData)
    {
        foreach (var install in installData)
        {
            if (_systemSettings?.CurrentVersion == install.Version)
            {
                if (install.IsComplete)
                {
                    _needsInstall = false;
                    _logger.LogInformation("Current version is upto date - no install required");
                }
                else
                {
                    _installInfo = install;
                    _logger.LogInformation("Partial install required");
                }
            }
        }
    }

    private async Task InitDatabase()
    {
        try
        {
            _logger.LogInformation("Initializing database");
            await _repositoryManager.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize database");
            throw;
        }
        _logger.LogInformation("Database initialized");
    }

    private async Task InitEnabledEntities()
    {
        if (_enabledEntitiesConfig.EnabledCountries == null)
        {
            _logger.LogError("No enabled countries found in configuration");
            throw new Exception("No enabled countries found in configuration");
        }
        else
        {
            try
            {
                await _repositoryManager.BeginTransactionAsync();

                foreach (var enabledCountry in _enabledEntitiesConfig.EnabledCountries)
                {
                    if (enabledCountry.Name == null)
                    {
                        _logger.LogError("Country name not found in configuration");
                        throw new Exception("Country name not found in configuration");
                    }
                    else
                    {
                        var country = await _repositoryManager.Countries.GetCountryByNameIncludeLeaguesAsync(enabledCountry.Name);
                        if (country == null)
                        {
                            _logger.LogError($"Country [{enabledCountry.Name}] not found in database");
                            throw new Exception($"Country [{enabledCountry.Name}] not found in database");
                        }
                        else
                        {
                            
                            country.IsEnabled = true;

                            foreach (var enabledLeague in enabledCountry.Leagues)
                            {
                                var league = country.Leagues.FirstOrDefault(l => l.Name == enabledLeague);

                                if (league == null)
                                {
                                    _logger.LogError($"League [{enabledLeague}] not found in database");
                                    throw new Exception($"League [{enabledLeague}] not found in database");
                                }
                                else
                                {
                                    league.IsEnabled = true;
                                }
                            }

                            await _repositoryManager.Countries.AddOrUpdateCountryAsync(country);
                        }
                    }
                }

                await _repositoryManager.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize enabled entities");
                await _repositoryManager.RollbackTransactionAsync();
                throw;
            }
  
        }
    }
}