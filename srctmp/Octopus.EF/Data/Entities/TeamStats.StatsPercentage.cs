namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics percentage.
    /// </summary>
    public class StatsPercentage
    {
        /// <summary>
        /// Gets or sets the ID of the statistics percentage.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the percentage value.
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// Gets or sets the total value.
        /// </summary>
        public int Total { get; set; }
    }
}