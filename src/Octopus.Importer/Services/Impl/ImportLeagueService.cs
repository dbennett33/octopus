using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Interfaces;

namespace Octopus.Importer.Services.Impl
{
    public class ImportLeagueService : IImportLeagueService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IApiLeagueService _apiLeagueService;
        private readonly ILogger<ImportLeagueService> _logger;

        public ImportLeagueService(IRepositoryManager repositoryManager,
                                   IApiLeagueService apiLeagueService,
                                   ILogger<ImportLeagueService> logger)
        {
            _repositoryManager = repositoryManager;
            _apiLeagueService = apiLeagueService;
            _logger = logger;
        }

        public async Task<bool> ImportLeagues()
        {
            bool success = true;

            try
            {        
                foreach (var league in await _apiLeagueService.GetLeaguesAsync())
                {
                    await _repositoryManager.Leagues.AddOrUpdateLeagueAsync(league);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the League import - ");
                success = false;
            }

            return success;
        }
    }
}
