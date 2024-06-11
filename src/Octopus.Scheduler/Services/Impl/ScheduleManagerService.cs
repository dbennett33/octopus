using Octopus.Scheduler.Services.Interfaces;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleManagerService : IScheduleManagerService
    {
        private readonly IScheduleSystemInstall _installScheduler;
        private readonly IScheduleCountryService _countryScheduler;
        private readonly IScheduleLeagueService _leagueScheduler;

        public ScheduleManagerService(IScheduleSystemInstall installScheduler,
                                      IScheduleCountryService countryScheduler,
                                      IScheduleLeagueService scheduleLeagueService)
        {
            _installScheduler = installScheduler;
            _countryScheduler = countryScheduler;
            _leagueScheduler = scheduleLeagueService;
        }


        public IScheduleSystemInstall InstallScheduler => _installScheduler;
        public IScheduleCountryService CountryScheduler => _countryScheduler;
        public IScheduleLeagueService LeagueScheduler => _leagueScheduler;
    }
}
