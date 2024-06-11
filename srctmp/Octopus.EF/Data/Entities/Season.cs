namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a season in a league.
    /// </summary>
    public class Season
    {
        /// <summary>
        /// Gets or sets the ID of the season.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the league associated with the season.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the league associated with the season.
        /// </summary>
        public League? League { get; set; }

        /// <summary>
        /// Gets or sets the year of the season.
        /// </summary>
        public string Year { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the start date of the season.
        /// </summary> 
        public string? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the season.
        /// </summary>
        public string? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the season is the current season.
        /// </summary>
        public bool Current { get; set; }

        /// <summary>
        /// Gets or sets the coverage settings for the season.
        /// </summary>
        public Coverage SeasonCoverage { get; set; } = new Coverage();
    }
}