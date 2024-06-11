using Hangfire;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Scheduler.Tasks.Interfaces;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleCountryService : IScheduleCountryService
    {
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly ITasksCountry _tasksCountry;

        public ScheduleCountryService(IRecurringJobManager recurringJobManager, ITasksCountry tasksCountry)
        {
            _recurringJobManager = recurringJobManager;
            _tasksCountry = tasksCountry;
        }

        public async Task ScheduleRecurringCountryJobs()
        {
            RecurringJobOptions jobOptions = new()
            {
                TimeZone = TimeZoneInfo.Local
            };

            await Task.Run(() =>
            {
                RecurringJob.AddOrUpdate(
                    "weekly-sync-countries",
                    () => _tasksCountry.GetCountries(),
                    Cron.Weekly, jobOptions);
            }); 
        }
    }
}   
