namespace Octopus.EF.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a league.
    /// </summary>
    public class League
    {
        /// <summary>
        /// Gets or sets the ID of the league.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the league.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the league.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the logo of the league.
        /// </summary>
        public string Logo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the country associated with the league.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country associated with the league.
        /// </summary>
        public Country? Country { get; set; }

        /// <summary>
        /// Gets or sets the seasons associated with the league.
        /// </summary>
        public ICollection<Season> Seasons { get; set; } = new List<Season>();
    }
}
