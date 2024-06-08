using Octopus.ApiClient.Services.Impl;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Services.Impl;

public class InitializerService : IInitializerService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IApiClientService _apiClientService;
    private readonly IInstallerService _installerService;
    private readonly ILogger<InitializerService> _logger;

    private SystemSettings? _systemSettings;
    private InstallInfo? _installInfo;
    private bool _needsInstall = true;

    public InitializerService(IRepositoryManager repositoryManager, IApiClientService apiClientService, IInstallerService installerService, ILogger<InitializerService> logger)
    {
        _repositoryManager = repositoryManager;
        _apiClientService = apiClientService;
        _installerService = installerService;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        await InitDatabase();
        await InitSystemSettings();

        if (_needsInstall)
        {
            await _installerService.Install(_installInfo!);
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
}