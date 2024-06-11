using Octopus.EF.Data.Entities;

namespace Octopus.EF.Repositories.Interfaces
{
    /// <summary>
    /// Represents a repository for managing countries.
    /// </summary>
    public interface ICountryRepository : IDisposable
    {
        /// <summary>
        /// Retrieves all countries asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of countries.</returns>
        Task<IEnumerable<Country>> GetCountriesAsync();

        Task<IEnumerable<Country>> GetCountriesIncludeLeaguesAsync();
        /// <summary>
        /// Retrieves a country by its ID asynchronously.
        /// </summary>
        /// <param name="countryId">The ID of the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the country, or null if not found.</returns>
        Task<Country?> GetCountryByIdAsync(int countryId);

        /// <summary>
        /// Retrieves a country by its code asynchronously.
        /// </summary>
        /// <param name="countryCode">The code of the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the country, or null if not found.</returns>
        Task<Country?> GetCountryByCodeAsync(string countryCode);

        /// <summary>
        /// Retrieves a country by its name asynchronously.
        /// </summary>
        /// <param name="countryName">The name of the country.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the country, or null if not found.</returns>
        Task<Country?> GetCountryByNameAsync(string countryName);

        Task<Country?> GetCountryByNameIncludeLeaguesAsync(string countryName);
        /// <summary>
        /// Adds or updates a new country asynchronously.
        /// </summary>
        /// <param name="country">The country to add or update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddOrUpdateCountryAsync(Country country);

        /// <summary>
        /// Deletes a country.
        /// </summary>
        /// <param name="country">The country to delete.</param>
        void DeleteCountry(Country country);

        /// <summary>
        /// Checks if a country with the specified name exists asynchronously.
        /// </summary>
        /// <param name="countryName">The name of the country to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the country exists.</returns>
        Task<bool> ExistsAsync(string countryName);
    }
}