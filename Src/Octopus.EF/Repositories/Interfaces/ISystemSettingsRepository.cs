using Octopus.EF.Data.Entities;

namespace Octopus.EF.Repositories.Interfaces;

public interface ISystemSettingsRepository
{
    Task<SystemSettings> GetSystemSettingsAsync();
    void UpdateSystemSettingsAsync(SystemSettings systemSettings);
    Task AddSystemSettingsAsync(SystemSettings systemSettings);
}