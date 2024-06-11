using Octopus.EF.Data.Entities;

namespace Octopus.Scheduler.Tasks.Interfaces
{
    public interface ITasksSystemInstall
    {
        Task Install();
        Task<InstallInfo> InstallStageOne(InstallInfo installInfo);
        Task<InstallInfo> InstallStageTwo(InstallInfo installInfo);
    }
}