using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Interfaces;
using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Services.Impl
{
    public class InstallerService : IInstallerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IImportCountryService _countryService;
        private readonly IImportLeagueService _leagueService;
        private readonly IImportTeamService _teamService;
        private readonly ILogger<InstallerService> _logger;

        public InstallerService(IImportCountryService countryService,
                                IImportLeagueService leagueService,
                                IImportTeamService teamService,
                                IRepositoryManager repositoryManager,
                                ILogger<InstallerService> logger)
        {
            _countryService = countryService;
            _leagueService = leagueService;
            _teamService = teamService;
            _repositoryManager = repositoryManager;
            _logger = logger;
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

            installInfo.TeamsInstalled = await InstallTeams(installInfo);

             






            installInfo.InstallEndDate = DateTime.Now;
            installInfo.IsComplete = true;
            return installInfo;
        }

        private async Task<bool> InstallTeams(InstallInfo installInfo)
        {
            bool success = true;
            if (!installInfo.TeamsInstalled)
            {

                foreach (var league in await _repositoryManager.Leagues.GetLeaguesAsync(true))
                {
                    var result = await _teamService.ImportTeamsByLeagueAsync(league.Id, "2023");
                    if (!result)
                    {
                        success = false;
                    }
                }
            }

            return success;
        }
    }
}
