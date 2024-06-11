namespace Octopus.EF.Data.Entities
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the statistics related to goals for a team.
    /// </summary>
    public class StatsGoals
    {
        /// <summary>
        /// Gets or sets the average number of goals scored at home.
        /// </summary>
        [Precision(5, 4)]
        public decimal HomeAverage { get; set; }

        /// <summary>
        /// Gets or sets the average number of goals scored away.
        /// </summary>
        [Precision(5, 4)]
        public decimal AwayAverage { get; set; }

        /// <summary>
        /// Gets or sets the average number of goals scored in total.
        /// </summary>
        [Precision(5, 4)]
        public decimal TotalAverage { get; set; }

        /// <summary>
        /// Gets or sets the total number of goals scored at home.
        /// </summary>
        public int HomeTotal { get; set; }

        /// <summary>
        /// Gets or sets the total number of goals scored away.
        /// </summary>
        public int AwayTotal { get; set; }

        /// <summary>
        /// Gets or sets the total number of goals scored in total.
        /// </summary>
        public int Total { get; set; }
    }
}
