using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
