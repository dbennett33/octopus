using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Interfaces;

namespace Octopus.Importer.Services.Impl
{
    public class ImportTeamService : IImportTeamService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IApiTeamService _apiTeamService;
        private readonly ILogger<ImportTeamService> _logger;

        public ImportTeamService(IRepositoryManager repositoryManager,
                                 IApiTeamService apiTeamService,
                                 ILogger<ImportTeamService> logger)
        {
            _repositoryManager = repositoryManager;
            _apiTeamService = apiTeamService;
            _logger = logger;
        }

        public async Task<bool> ImportTeamsByCountryAsync(string countryName)
        {
            bool success = false;

            try
            {
                foreach (var team in await _apiTeamService.GetTeamsByCountryNameAsync(countryName))
                {
                    await _repositoryManager.Teams.AddOrUpdateTeamAsync(team);
                }

                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the Team import - ");
            }


            return success;
        }

        public async Task<bool> ImportTeamsByLeagueAsync(int leagueId, string season)
        {
            bool success = false;

            try
            {
                foreach (var team in await _apiTeamService.GetTeamsByLeagueIdAsync(leagueId, season))
                {
                    await _repositoryManager.Teams.AddOrUpdateTeamAsync(team);
                }

                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the Team import - ");
            }

            return success;
        }
    }
}
