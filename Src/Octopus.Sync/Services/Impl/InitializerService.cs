using Microsoft.Extensions.Options;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Sync.Configurations;
using Octopus.Sync.Services.Interfaces;
using System.Text.Json;

namespace Octopus.Sync.Services.Impl;

public class InitializerService : IInitializerService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IApiClientService _apiClientService;
    private readonly IInstallerService _installerService;
    private readonly EnabledEntitiesConfig _enabledEntitiesConfig;
    private readonly IScheduleManagerService _scheduleManagerService;
    private readonly ILogger<InitializerService> _logger;

    private SystemSettings? _systemSettings;
    private InstallInfo? _installInfo;
    private bool _needsInstall = true;

    public InitializerService(IRepositoryManager repositoryManager,
                              IApiClientService apiClientService,
                              IInstallerService installerService,
                              IOptions<EnabledEntitiesConfig> enabledEntitiesConfig,
                              IScheduleManagerService scheduleManagerService,
                              ILogger<InitializerService> logger)
    {
        _repositoryManager = repositoryManager;
        _apiClientService = apiClientService;
        _installerService = installerService;
        _enabledEntitiesConfig = enabledEntitiesConfig.Value;
        _scheduleManagerService = scheduleManagerService;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        _logger.LogInformation("Initializing system");
        await InitDatabase();
        await InitSystemSettings();
        await InitInstallData();

        if (_needsInstall)
        {
            await Install();
        }

        await InitRecurringTasks();
        _logger.LogInformation("System initialized");
    }

    private async Task InitRecurringTasks()
    {
        await _scheduleManagerService.CountryScheduler.ScheduleRecurringCountryJobs();
        await _scheduleManagerService.LeagueScheduler.ScheduleRecurringLeagueJobs();
    }

    private async Task Install()
    {
        _installInfo = await _installerService.InstallStageOne(_installInfo!);

        await InitEnabledEntities();

        _installInfo = await _installerService.InstallStageTwo(_installInfo!);

        if (_installInfo.IsComplete)
        {
            _repositoryManager.InstallInfo.UpdateInstallInfoAsync(_installInfo);
            await _repositoryManager.CompleteAsync();
            _logger.LogInformation("Install complete");
        }
        else
        {
            _logger.LogError("Install failed");
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
                systemSettings.CurrentVersion = "0";

                await _repositoryManager.SystemSettings.AddSystemSettingsAsync(systemSettings);
                await _repositoryManager.CompleteAsync();
            }

            if (systemSettings != null)
            {
                _systemSettings = systemSettings;
                _logger.LogInformation($"System exists - v{_systemSettings.CurrentVersion}");               
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize system settings");
            throw;
        }
    }

    private async Task InitInstallData()
    {
        var installData = await _repositoryManager.InstallInfo.GetAllInstallInfoAsync();

        if (installData.Count != 0)
        {
            var latest = installData.OrderByDescending(i => i.Version).First();
            if (latest != null)
            {
                string enabledEntitiesJson = JsonSerializer.Serialize(_enabledEntitiesConfig);

                if (string.Compare(enabledEntitiesJson, latest.EnabledEntitiesJson) != 0)
                {
                    _logger.LogInformation("Enabled entities json config has changed");
                    _logger.LogInformation("The system will be updated to reflect the new enabled entities");
                    var newInstallInfo = new InstallInfo
                    {
                        CountriesInstalled = true,
                        LeaguesInstalled = true,
                        SystemSettings = latest.SystemSettings,
                        SystemSettingsId = latest.SystemSettingsId,
                        Version = latest.Version + 1,     
                        EnabledEntitiesJson = enabledEntitiesJson,
                        InstallStartDate = DateTime.Now
                    };

                    await _repositoryManager.InstallInfo.AddInstallInfoAsync(newInstallInfo);
                    await _repositoryManager.CompleteAsync();

                    _installInfo = newInstallInfo;         
                }
                else if (latest.IsComplete)
                {
                    _needsInstall = false;
                    _logger.LogInformation("Current version is upto date - no install required");
                }
                else
                {
                    _installInfo = latest;
                    _logger.LogInformation("Partial install required");
                }
            }
        }
        else
        {
            _logger.LogInformation("No InstallData found - install required");
            _installInfo = new InstallInfo();
            _installInfo.SystemSettings = _systemSettings;
            _installInfo.SystemSettingsId = _systemSettings!.Id;
            _installInfo.Version = 1;
            _installInfo.EnabledEntitiesJson = JsonSerializer.Serialize(_enabledEntitiesConfig);
            _installInfo.InstallStartDate = DateTime.Now;

            await _repositoryManager.InstallInfo.AddInstallInfoAsync(_installInfo);
            await _repositoryManager.CompleteAsync();
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

                // Reset all countries and leagues to disabled
                var countries = await _repositoryManager.Countries.GetCountriesIncludeLeaguesAsync();
                foreach (var country in countries)
                {
                    country.IsEnabled = false;
                    foreach (var league in country.Leagues)
                    {
                        league.IsEnabled = false;
                    }
                    await _repositoryManager.Countries.AddOrUpdateCountryAsync(country);
                    await _repositoryManager.CompleteAsync();
                }
                            
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
                            _logger.LogInformation($"Country enabled - [{enabledCountry.Name}]");
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
                                    _logger.LogInformation($"League enabled - [{enabledLeague}]");
                                    league.IsEnabled = true;
                                }
                            }

                            await _repositoryManager.Countries.AddOrUpdateCountryAsync(country);
                        }
                    }
                }

                _installInfo!.EnabledEntitiesApplied = true;

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