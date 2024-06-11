using Octopus.Scheduler.Services.Interfaces;

namespace Octopus.Scheduler.Services.Impl
{
    public class ScheduleManagerService : IScheduleManagerService
    {
        private readonly IScheduleCountryService _countryScheduler;
        private readonly IScheduleLeagueService _leagueScheduler;

        public ScheduleManagerService(IScheduleCountryService countryScheduler,
                                      IScheduleLeagueService scheduleLeagueService)
        {
            _countryScheduler = countryScheduler;
            _leagueScheduler = scheduleLeagueService;
        }

        public IScheduleCountryService CountryScheduler => _countryScheduler;
        public IScheduleLeagueService LeagueScheduler => _leagueScheduler;
    }
}
