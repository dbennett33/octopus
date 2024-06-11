namespace Octopus.Scheduler.Services.Interfaces
{
    public interface IScheduleManagerService
    {
        public IScheduleSystemInstall InstallScheduler { get; }
        public IScheduleCountryService CountryScheduler { get; }
        public IScheduleLeagueService LeagueScheduler { get; }
    }
}