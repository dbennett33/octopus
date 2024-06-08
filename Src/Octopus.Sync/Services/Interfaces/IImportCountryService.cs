namespace Octopus.Sync.Services.Interfaces
{
    public interface IImportCountryService
    {
        Task<bool> ImportCountries();
    }
}