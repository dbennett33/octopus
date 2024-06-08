namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics for home, away, and total games for a team.
    /// </summary>
    public class StatsHomeAwayTotal
    {
        /// <summary>
        /// Gets or sets the unique identifier of the statistics.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the team statistics.
        /// </summary>
        public int TeamStatsId { get; set; }

        /// <summary>
        /// Gets or sets the number of home games played by the team.
        /// </summary>
        public int Home { get; set; }

        /// <summary>
        /// Gets or sets the number of away games played by the team.
        /// </summary>
        public int Away { get; set; }

        /// <summary>
        /// Gets or sets the total number of games played by the team.
        /// </summary>
        public int Total { get; set; }
    }
}