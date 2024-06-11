using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiCountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}