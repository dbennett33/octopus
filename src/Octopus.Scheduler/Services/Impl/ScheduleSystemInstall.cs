using Hangfire;
using Microsoft.Extensions.Logging;
using Octopus.Scheduler.Services.Interfaces;
using Octopus.Scheduler.Tasks.Interfaces;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleSystemInstall : IScheduleSystemInstall
    {
        private readonly ITasksSystemInstall _tasksSystemInstall;
        private readonly ILogger<ScheduleSystemInstall> _logger;

        public ScheduleSystemInstall(ITasksSystemInstall tasksSystemInstall, ILogger<ScheduleSystemInstall> logger)
        {
            _tasksSystemInstall = tasksSystemInstall;
            _logger = logger;
        }

        public async Task InstallSystem()
        {
            BackgroundJob.Enqueue(() => _tasksSystemInstall.Install());
        }

    }
}
