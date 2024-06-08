using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Sync.Services.Interfaces;

namespace Octopus.Sync.Services.Impl
{
    public class InstallerService : IInstallerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IImportCountryService _countryService;
        private readonly IImportLeagueService _leagueService;
        private readonly ILogger<InstallerService> _logger;

        public InstallerService(IImportCountryService countryService, IImportLeagueService leagueService, IRepositoryManager repositoryManager, ILogger<InstallerService> logger)
        {
            _countryService = countryService;
            _leagueService = leagueService;
            _repositoryManager = repositoryManager;
            _logger = logger;
        }

        public async Task Install(InstallInfo installInfo)
        {
            if (installInfo == null)
            {
                throw new ArgumentNullException(nameof(installInfo));
            }            

            if (!installInfo.CountriesInstalled)
            {
                installInfo.CountriesInstalled = await _countryService.ImportCountries();
                await _repositoryManager.CompleteAsync();
            }

            if (!installInfo.LeaguesInstalled)
            {
                await _repositoryManager.BeginTransactionAsync();
                installInfo.LeaguesInstalled = await _leagueService.ImportLeagues();
                await _repositoryManager.CommitTransactionAsync();
            }


            

        }

    }
}
