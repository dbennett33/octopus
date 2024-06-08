namespace Octopus.EF.Repositories.Interfaces
{
    using Octopus.EF.Data.Entities;

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

        /// <summary>
        /// Adds a new country asynchronously.
        /// </summary>
        /// <param name="country">The country to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCountryAsync(Country country);

        Task AddCountryRangeAsync(IEnumerable<Country> countries);
        
        /// <summary>
        /// Updates an existing country.
        /// </summary>
        /// <param name="country">The country to update.</param>
        void UpdateCountry(Country country);

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