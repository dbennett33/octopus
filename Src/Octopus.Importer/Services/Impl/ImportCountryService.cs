using Microsoft.Extensions.Logging;
using Octopus.ApiClient.Services.Interfaces;
using Octopus.EF.Data.Entities;
using Octopus.EF.Repositories.Interfaces;
using Octopus.Importer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Importer.Services.Impl
{
    public class ImportCountryService : IImportCountryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IApiCountryService _apiCountryService;
        private readonly ILogger<ImportCountryService> _logger;

        public ImportCountryService(IRepositoryManager repositoryManager, IApiCountryService apiCountryService, ILogger<ImportCountryService> logger)
        {
            _repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            _apiCountryService = apiCountryService ?? throw new ArgumentNullException(nameof(apiCountryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> ImportCountries()
        {
            bool success = false;

            try
            {    
                foreach (var country in await _apiCountryService.GetCountriesAsync())
                {
                    await _repositoryManager.Countries.AddOrUpdateCountryAsync(country);
                }

                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong during the Country import - ");
            }

            return success;
        }
    }
}
