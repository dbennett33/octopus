using System.Collections.Generic;
using Octopus.ApiClient.Models;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Mappers.Interfaces
{
    public interface ICountryMapper
    {
        IEnumerable<Country> Map(IEnumerable<ApiCountry> apiCountries);
    }
}