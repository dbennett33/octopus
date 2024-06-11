namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics of a team.
    /// </summary>
    public class TeamStats
    {
        /// <summary>
        /// Gets or sets the ID of the team stats.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the team associated with the stats.
        /// </summary>
        public Team Team { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the ID of the team associated with the stats.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the league associated with the stats.
        /// </summary>
        public League League { get; set; } = new League();

        /// <summary>
        /// Gets or sets the ID of the league associated with the stats.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the season associated with the stats.
        /// </summary>
        public Season Season { get; set; } = new Season();

        /// <summary>
        /// Gets or sets the ID of the season associated with the stats.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the biggest stats of the team.
        /// </summary>
        public StatsBiggest Biggest { get; set; } = new StatsBiggest();

        /// <summary>
        /// Gets or sets the goals stats of the team.
        /// </summary>
        public StatsGoals Goals { get; set; } = new StatsGoals();

        /// <summary>
        /// Gets or sets the clean sheets stats of the team.
        /// </summary>
        public StatsHomeAwayTotal CleanSheets { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the failed to score stats of the team.
        /// </summary>
        public StatsHomeAwayTotal FailedToScore { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the wins stats of the team.
        /// </summary>
        public StatsHomeAwayTotal Wins { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the draws stats of the team.
        /// </summary>
        public StatsHomeAwayTotal Draws { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the losses stats of the team.
        /// </summary>
        public StatsHomeAwayTotal Losses { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the played stats of the team.
        /// </summary>
        public StatsHomeAwayTotal Played { get; set; } = new StatsHomeAwayTotal();

        /// <summary>
        /// Gets or sets the form of the team.
        /// </summary>
        public string Form { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the formations stats of the team.
        /// </summary>
        public ICollection<StatsFormation> Formations { get; set; } = new List<StatsFormation>();

        /// <summary>
        /// Gets or sets the penalties stats of the team.
        /// </summary>
        public StatsPenalties Penalties { get; set; } = new StatsPenalties();
    }
}
