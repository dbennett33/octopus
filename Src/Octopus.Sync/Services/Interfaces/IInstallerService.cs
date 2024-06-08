using Octopus.EF.Data.Entities;

namespace Octopus.Sync.Services.Interfaces
{
    public interface IInstallerService
    {
        Task Install(InstallInfo installInfo);
    }
}