using Octopus.EF.Data.Entities;

namespace Octopus.EF.Repositories.Interfaces
{
    public interface IInstallInfoRepository
    {
        Task<List<InstallInfo>> GetAllInstallInfoAsync();
        Task<InstallInfo?> GetInstallInfoByIdAsync(int installInfoId);
        void UpdateInstallInfoAsync(InstallInfo installInfo);
        Task AddInstallInfoAsync(InstallInfo installInfo);
    }
}