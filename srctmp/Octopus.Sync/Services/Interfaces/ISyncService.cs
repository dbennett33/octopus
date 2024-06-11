namespace Octopus.Sync.Services.Interfaces
{
    public interface ISyncService
    {
        Task Run();
        Task Init();
    }
}