namespace Octopus.EF.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a country.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the ID of the country.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the code of the country.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the flag of the country.
        /// </summary>
        public string Flag { get; set; } = string.Empty;

        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the leagues associated with the country.
        /// </summary>
        public ICollection<League> Leagues { get; set; } = new List<League>();
    }
}
