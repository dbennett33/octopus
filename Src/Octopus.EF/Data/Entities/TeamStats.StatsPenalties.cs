namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics for penalties of a team.
    /// </summary>
    public class StatsPenalties
    {
        /// <summary>
        /// Gets or sets the ID of the penalties statistics.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the team statistics.
        /// </summary>
        public int TeamStatsId { get; set; }

        /// <summary>
        /// Gets or sets the percentage of penalties scored by the team.
        /// </summary>
        public StatsPercentage Scored { get; set; } = new StatsPercentage();

        /// <summary>
        /// Gets or sets the percentage of penalties missed by the team.
        /// </summary>
        public StatsPercentage Missed { get; set; } = new StatsPercentage();

        /// <summary>
        /// Gets or sets the total number of penalties.
        /// </summary>
        public int Total { get; set; }
    }
}