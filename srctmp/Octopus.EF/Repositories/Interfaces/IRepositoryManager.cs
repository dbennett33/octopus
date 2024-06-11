namespace Octopus.EF.Repositories.Interfaces
{
    public interface IRepositoryManager : IDisposable
    {
        ICountryRepository Countries { get; }
        ILeagueRepository Leagues { get; }
        ITeamRepository Teams { get; }
        ISystemSettingsRepository SystemSettings { get; }
        IInstallInfoRepository InstallInfo { get; }
        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task MigrateAsync();
    }
}
