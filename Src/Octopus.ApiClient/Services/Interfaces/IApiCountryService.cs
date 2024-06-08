using Octopus.EF.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Octopus.ApiClient.Services.Interfaces
{
    public interface IApiCountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}