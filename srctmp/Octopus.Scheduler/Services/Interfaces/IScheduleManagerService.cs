namespace Octopus.Scheduler.Services.Interfaces
{
    public interface IScheduleManagerService
    {
        public IScheduleCountryService CountryScheduler { get; }
        public IScheduleLeagueService LeagueScheduler { get; }
    }
}