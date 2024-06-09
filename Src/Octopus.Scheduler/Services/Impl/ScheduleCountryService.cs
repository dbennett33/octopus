using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Scheduler.Tasks;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleCountryService : IScheduleCountryService
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public ScheduleCountryService(IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;
        }

        public async Task ScheduleRecurringCountryJobs()
        {
            RecurringJob.AddOrUpdate(
                "daily-sync-task",
                () => new TasksCountry().GetCountries(),
                Cron.Minutely);    

        }
    }
}   
