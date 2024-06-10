using Octopus.Importer.Services.Interfaces;
using Octopus.Scheduler.Tasks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Scheduler.Tasks.Impl
{
    public class TasksLeague : ITasksLeague
    {
        private readonly IImportLeagueService _importLeagueService;

        public TasksLeague(IImportLeagueService importLeagueService)
        {
            _importLeagueService = importLeagueService;
        }

        public async Task GetLeagues()
        {
            await _importLeagueService.ImportLeagues();
        }
    }
}
