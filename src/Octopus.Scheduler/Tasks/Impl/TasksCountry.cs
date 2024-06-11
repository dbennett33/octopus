using Octopus.Importer.Services.Interfaces;
using Octopus.Scheduler.Tasks.Interfaces;

namespace Octopus.Scheduler.Tasks.Impl
{
    public class TasksCountry : ITasksCountry
    {
        private readonly IImportCountryService _importCountryService;

        public TasksCountry(IImportCountryService importCountryService)
        {
            _importCountryService = importCountryService;
        }

        public async Task GetCountries()
        {
            await _importCountryService.ImportCountries();
        }
    }
}
