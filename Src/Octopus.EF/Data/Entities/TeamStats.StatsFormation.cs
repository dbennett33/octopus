namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics for a formation used by a team.
    /// </summary>
    public class StatsFormation
    {
        /// <summary>
        /// Gets or sets the unique identifier of the formation.
        /// </summary>
        public int Id { get; set; }
        public int TeamStatsId { get; set; }       

        /// <summary>
        /// Gets or sets the name of the formation.
        /// </summary>
        public string Formation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of times the formation has been played.
        /// </summary>
        public int Played { get; set; }
    }
}