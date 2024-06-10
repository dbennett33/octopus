using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Scheduler.Tasks;
using Octopus.Scheduler.Tasks.Interfaces;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleLeagueService : IScheduleLeagueService
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ITasksLeague _tasksLeague;

        public ScheduleLeagueService(IRecurringJobManager recurringJobManager, ITasksLeague tasksLeague)
        {
            _recurringJobManager = recurringJobManager;
            _tasksLeague = tasksLeague;
        }

        public async Task ScheduleRecurringLeagueJobs()
        {
            RecurringJobOptions jobOptions = new()
            {
                TimeZone = TimeZoneInfo.Local
            };

            await Task.Run(() =>
            {
                RecurringJob.AddOrUpdate(
                    "weekly-sync-leagues",
                    () => _tasksLeague.GetLeagues(),
                    Cron.Weekly, jobOptions);
            });
        }
    }
}
