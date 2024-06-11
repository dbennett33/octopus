using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Interfaces;
using Octopus.Scheduler.Tasks.Interfaces;
using Octopus.Sync.Configurations;
using System.Text;
using System.Text.Json;

namespace Octopus.Scheduler.Tasks.Impl
{

    public class TasksSystemInstall : ITasksSystemInstall
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IImportCountryService _countryService;
        private readonly IImportLeagueService _leagueService;
        private readonly IImportTeamService _teamService;
        private readonly ILogger<TasksSystemInstall> _logger;

        private EnabledEntitiesConfig? _enabledEntitiesConfig;

        public TasksSystemInstall(IImportCountryService countryService,
                                IImportLeagueService leagueService,
                                IImportTeamService teamService,
                                IRepositoryManager repositoryManager,
                                ILogger<TasksSystemInstall> logger)
        {
            _countryService = countryService;
            _leagueService = leagueService;
            _teamService = teamService;
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Install()
        {
            _logger.LogInformation("Starting install");
            var installs = await _repositoryManager.InstallInfo.GetAllInstallInfoAsync();
            var installInfo = installs.OrderByDescending(i => i.Version).First();

            try
            {
                _enabledEntitiesConfig = JsonSerializer.Deserialize<EnabledEntitiesConfig>(installInfo.EnabledEntitiesJson)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deserialize enabled entities configuration");
            }

            if (_enabledEntitiesConfig == null)
            {
                _logger.LogError("No enabled entities found in configuration");
                throw new Exception("No enabled entities found in configuration");
            }

            installInfo = await InstallStageOne(installInfo);
            installInfo = await InitEnabledEntities(installInfo);
            installInfo = await InstallStageTwo(installInfo);

            if (installInfo.IsComplete)
            {
                _repositoryManager.InstallInfo.UpdateInstallInfoAsync(installInfo);
                await _repositoryManager.CompleteAsync();
                _logger.LogInformation("Install complete");
            }
            else
            {
                _logger.LogError("Install failed");
                throw new Exception("Install failed");
            }   
        }

        public async Task<InstallInfo> InstallStageOne(InstallInfo installInfo)
        {
            if (installInfo == null)
            {
                throw new ArgumentNullException(nameof(installInfo));
            }

            await _repositoryManager.BeginTransactionAsync();

            if (!installInfo.CountriesInstalled)
            {
                installInfo.CountriesInstalled = await _countryService.ImportCountries();
                await _repositoryManager.CompleteAsync();
            }

            if (!installInfo.LeaguesInstalled)
            {
                installInfo.LeaguesInstalled = await _leagueService.ImportLeagues();
            }

            await _repositoryManager.CommitTransactionAsync();

            return installInfo;
        }

        public async Task<InstallInfo> InstallStageTwo(InstallInfo installInfo)
        {
            if (installInfo == null)
            {
                throw new ArgumentNullException(nameof(installInfo));
            }

            await _repositoryManager.BeginTransactionAsync();

            try
            {
                if (!installInfo.TeamsInstalled)
                {
                    installInfo.TeamsInstalled = await InstallTeams(installInfo);
                }

                installInfo.InstallEndDate = DateTime.Now;
                installInfo.IsComplete = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the Team import - ");
                await _repositoryManager.RollbackTransactionAsync();
            }

            await _repositoryManager.CommitTransactionAsync();
            return installInfo;
        }

        private async Task<bool> InstallTeams(InstallInfo installInfo)
        {
            bool success = true;
            if (!installInfo.TeamsInstalled)
            {

                foreach (var league in await _repositoryManager.Leagues.GetLeaguesAsync(true))
                {
                    var result = (await _teamService.ImportTeamsByLeagueAsync(league.Id, "2023")
                                && await _teamService.ImportTeamsByLeagueAsync(league.Id, "2024"));
                    if (!result)
                    {
                        success = false;
                    }
                }
            }

            return success;
        }

        private async Task<InstallInfo> InitEnabledEntities(InstallInfo installInfo)
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


                    var sb = new StringBuilder();
                    sb.AppendLine("Enabled Countries/Leagues:").AppendLine();
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
                                sb.AppendLine($"|-{country.Name}");
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
                                        sb.AppendLine($"¦---{league.Name}");
                                        league.IsEnabled = true;
                                    }
                                }
                                sb.AppendLine("|");

                                await _repositoryManager.Countries.AddOrUpdateCountryAsync(country);
                            }
                        }
                    }

                    _logger.LogInformation(sb.ToString());
                    installInfo!.EnabledEntitiesApplied = true;

                    await _repositoryManager.CommitTransactionAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to initialize enabled entities");
                    await _repositoryManager.RollbackTransactionAsync();
                    throw;
                }

                return installInfo;
            }
        }
    }
}
