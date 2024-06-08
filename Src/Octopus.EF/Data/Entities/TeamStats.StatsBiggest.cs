namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics for the biggest events in a team's performance.
    /// </summary>
    public class StatsBiggest
    {
        public int Id { get; set; }
        public int TeamStatsId { get; set; }
        public TeamStats TeamStats { get; set; }
        /// <summary>
        /// Gets or sets the number of biggest goals scored at home.
        /// </summary>
        public int BiggestGoalsScoredHome { get; set; }

        /// <summary>
        /// Gets or sets the number of biggest goals scored away.
        /// </summary>
        public int BiggestGoalsScoredAway { get; set; }

        /// <summary>
        /// Gets or sets the number of biggest goals conceded at home.
        /// </summary>
        public int BiggestGoalsConcededHome { get; set; }

        /// <summary>
        /// Gets or sets the number of biggest goals conceded away.
        /// </summary>
        public int BiggestGoalsConcededAway { get; set; }

        /// <summary>
        /// Gets or sets the description of the biggest home win.
        /// </summary>
        public string BiggestHomeWin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the biggest away win.
        /// </summary>
        public string BiggestAwayWin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the biggest home loss.
        /// </summary>
        public string BiggestHomeLoss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the biggest away loss.
        /// </summary>
        public string BiggestAwayLoss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of consecutive wins in a streak.
        /// </summary>
        public int StreakWins { get; set; }

        /// <summary>
        /// Gets or sets the number of consecutive draws in a streak.
        /// </summary>
        public int StreakDraws { get; set; }

        /// <summary>
        /// Gets or sets the number of consecutive losses in a streak.
        /// </summary>
        public int StreakLosses { get; set; }
    }
}
