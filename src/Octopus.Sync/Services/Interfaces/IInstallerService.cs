using Octopus.EF.Data.Entities;

namespace Octopus.Sync.Services.Interfaces
{
    public interface IInstallerService
    {
        Task<InstallInfo> InstallStageOne(InstallInfo installInfo);
        Task<InstallInfo> InstallStageTwo(InstallInfo installInfo);
    }
}