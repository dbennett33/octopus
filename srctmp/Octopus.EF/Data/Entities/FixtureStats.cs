namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents the statistics of a fixture for a specific team.
    /// </summary>
    public class FixtureStats
    {
        /// <summary>
        /// Gets or sets the ID of the fixture statistics.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the fixture associated with the statistics.
        /// </summary>
        public Fixture Fixture { get; set; } = new Fixture();

        /// <summary>
        /// Gets or sets the ID of the fixture associated with the statistics.
        /// </summary>
        public int FixtureId { get; set; }

        /// <summary>
        /// Gets or sets the team associated with the statistics.
        /// </summary>
        public Team Team { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the ID of the team associated with the statistics.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the number of shots on goal.
        /// </summary>
        public int ShotsOnGoal { get; set; }

        /// <summary>
        /// Gets or sets the number of shots off goal.
        /// </summary>
        public int ShotsOffGoal { get; set; }

        /// <summary>
        /// Gets or sets the total number of shots.
        /// </summary>
        public int TotalShots { get; set; }

        /// <summary>
        /// Gets or sets the number of blocked shots.
        /// </summary>
        public int BlockedShots { get; set; }

        /// <summary>
        /// Gets or sets the number of shots inside the box.
        /// </summary>
        public int ShotsInsideBox { get; set; }

        /// <summary>
        /// Gets or sets the number of shots outside the box.
        /// </summary>
        public int ShotsOutsideBox { get; set; }

        /// <summary>
        /// Gets or sets the number of fouls.
        /// </summary>
        public int Fouls { get; set; }

        /// <summary>
        /// Gets or sets the number of corner kicks.
        /// </summary>
        public int CornerKicks { get; set; }

        /// <summary>
        /// Gets or sets the number of offsides.
        /// </summary>
        public int Offsides { get; set; }

        /// <summary>
        /// Gets or sets the ball possession percentage.
        /// </summary>
        public int BallPossession { get; set; }

        /// <summary>
        /// Gets or sets the number of yellow cards.
        /// </summary>
        public int YellowCards { get; set; }

        /// <summary>
        /// Gets or sets the number of red cards.
        /// </summary>
        public int RedCards { get; set; }

        /// <summary>
        /// Gets or sets the number of goalkeeper saves.
        /// </summary>
        public int GoalkeeperSaves { get; set; }

        /// <summary>
        /// Gets or sets the total number of passes.
        /// </summary>
        public int TotalPasses { get; set; }

        /// <summary>
        /// Gets or sets the number of accurate passes.
        /// </summary>
        public int PassesAccurate { get; set; }

        /// <summary>
        /// Gets or sets the percentage of accurate passes.
        /// </summary>
        public int PassesPercentage { get; set; }
    }
}