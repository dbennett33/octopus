namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a team in the Octopus system.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Gets or sets the unique identifier of the team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the code of the team.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the country of the team.
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the year the team was founded.
        /// </summary>
        public string Founded { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the team is a national team.
        /// </summary>
        public bool IsNationalTeam { get; set; }

        /// <summary>
        /// Gets or sets the logo of the team.
        /// </summary>
        public string Logo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the venue where the team plays its home matches.
        /// </summary>
        public int? VenueId { get; set; }

        /// <summary>
        /// Gets or sets the venue where the team plays its home matches.
        /// </summary>
        public Venue Venue { get; set; } = new Venue();

        /// <summary>
        /// Gets or sets the list of team statistics.
        /// </summary>
        public ICollection<TeamStats> TeamStats { get; set; } = new List<TeamStats>();
    }
}
