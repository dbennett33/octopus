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
            bool success = await _importCountryService.ImportCountries();
            if (!success)
            {
                throw new Exception("Something went wrong during the Country import");
            }
        }
    }
}
