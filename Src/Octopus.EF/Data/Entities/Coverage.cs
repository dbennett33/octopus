namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the coverage options for a season.
    /// </summary>
    public class Coverage
    {
        /// <summary>
        /// Gets or sets the ID of the coverage.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the season.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season associated with the coverage.
        /// </summary>
        public Season Season { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether events are covered.
        /// </summary>
        public bool Events { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether lineups are covered.
        /// </summary>
        public bool Lineups { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether fixture stats are covered.
        /// </summary>
        public bool FixtureStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether player stats are covered.
        /// </summary>
        public bool PlayerStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether standings are covered.
        /// </summary>
        public bool Standings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether players are covered.
        /// </summary>
        public bool Players { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether top scorers are covered.
        /// </summary>
        public bool TopScorers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether top assists are covered.
        /// </summary>
        public bool TopAssists { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether top cards are covered.
        /// </summary>
        public bool TopCards { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether injuries are covered.
        /// </summary>
        public bool Injuries { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether predictions are covered.
        /// </summary>
        public bool Predictions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether odds are covered.
        /// </summary>
        public bool Odds { get; set; }
    }
}
