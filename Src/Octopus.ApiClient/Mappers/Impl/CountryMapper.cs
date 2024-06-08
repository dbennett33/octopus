using System.Collections.Generic;
using Octopus.ApiClient.Models;
using Octopus.ApiClient.Mappers.Interfaces;
using Octopus.EF.Data.Entities;

namespace Octopus.ApiClient.Mappers.Impl
{
    public class CountryMapper : ICountryMapper
    {
        public IEnumerable<Country> Map(IEnumerable<ApiCountry> apiCountries)
        {
            var countries = new List<Country>();

            if (apiCountries != null)
            {
                foreach (var apiCountry in apiCountries)
                {
                    var country = new Country
                    {
                        Name = apiCountry.Name ?? string.Empty,
                        Code = apiCountry.Code ?? string.Empty,
                        Flag = apiCountry.Flag ?? string.Empty
                    };

                    countries.Add(country);
                }
            } 

            return countries;
        }
    }
}