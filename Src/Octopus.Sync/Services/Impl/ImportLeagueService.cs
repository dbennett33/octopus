using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Sync.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Sync.Services.Impl
{
    public class ImportLeagueService : IImportLeagueService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IApiLeagueService _apiLeagueService;
        private readonly ILogger<ImportLeagueService> _logger;

        public ImportLeagueService(IRepositoryManager repositoryManager, IApiLeagueService apiLeagueService, ILogger<ImportLeagueService> logger)
        {
            _repositoryManager = repositoryManager;
            _apiLeagueService = apiLeagueService;
            _logger = logger;
        }

        public async Task<bool> ImportLeagues()
        {
            bool success = false;

            try
            {
                var apiLeagues = await _apiLeagueService.GetLeaguesAsync();
                var dbLeagues = await _repositoryManager.Leagues.GetLeaguesAsync();
                var leaguesToAdd = new List<League>();

                foreach (var league in apiLeagues)
                {
                    if (!dbLeagues.Any(l => l.Name == league.Name))
                    {
                        leaguesToAdd.Add(league);
                    }
                    else
                    {
                        _repositoryManager.Leagues.UpdateLeague(league);
                    }
                }

                await _repositoryManager.Leagues.AddLeagueRangeAsync(leaguesToAdd);
                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the League import - ");
            }

            return success;
        }
    }
}
