namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a fixture lineup.
    /// </summary>
    public class FixtureLineup
    {
        /// <summary>
        /// Gets or sets the ID of the fixture lineup.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the team associated with the fixture lineup.
        /// </summary>
        public Team Team { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the ID of the team associated with the fixture lineup.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the formation used in the fixture lineup.
        /// </summary>
        public string Formation { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of players in the starting XI.
        /// </summary>
        public ICollection<StartXI> StartXI { get; set; } = new List<StartXI>();

        /// <summary>
        /// Gets or sets the collection of substitute players.
        /// </summary>
        public ICollection<Substitute> Substitutes { get; set; } = new List<Substitute>();

        /// <summary>
        /// Gets or sets the coach associated with the fixture lineup.
        /// </summary>
        public Coach Coach { get; set; } = new Coach();

        /// <summary>
        /// Gets or sets the ID of the coach associated with the fixture lineup.
        /// </summary>
        public int CoachId { get; set; }
    }

    /// <summary>
    /// Represents a player in the starting XI.
    /// </summary>
    public class StartXI
    {
        /// <summary>
        /// Gets or sets the ID of the player in the starting XI.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the fixture lineup associated with the player in the starting XI.
        /// </summary>
        public FixtureLineup FixtureLineup { get; set; } = new FixtureLineup();

        /// <summary>
        /// Gets or sets the ID of the fixture lineup associated with the player in the starting XI.
        /// </summary>
        public int FixtureLineupId { get; set; }

        /// <summary>
        /// Gets or sets the player in the starting XI.
        /// </summary>
        public Player Player { get; set; } = new Player();

        /// <summary>
        /// Gets or sets the ID of the player in the starting XI.
        /// </summary>
        public int PlayerId { get; set; }
    }

    /// <summary>
    /// Represents a substitute player.
    /// </summary>
    public class Substitute
    {
        /// <summary>
        /// Gets or sets the ID of the substitute player.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the fixture lineup associated with the substitute player.
        /// </summary>
        public FixtureLineup FixtureLineup { get; set; } = new FixtureLineup();

        /// <summary>
        /// Gets or sets the ID of the fixture lineup associated with the substitute player.
        /// </summary>
        public int FixtureLineupId { get; set; }

        /// <summary>
        /// Gets or sets the substitute player.
        /// </summary>
        public Player Player { get; set; } = new Player();

        /// <summary>
        /// Gets or sets the ID of the substitute player.
        /// </summary>
        public int PlayerId { get; set; }
    }

    /// <summary>
    /// Represents a coach.
    /// </summary>
    public class Coach
    {
        /// <summary>
        /// Gets or sets the ID of the coach.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the coach.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the photo of the coach.
        /// </summary>
        public string Photo { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents the colors of a player.
    /// </summary>
    public class Colors
    {
        /// <summary>
        /// Gets or sets the color settings for a player.
        /// </summary>
        public PlayerColor Player { get; set; } = new PlayerColor();

        /// <summary>
        /// Gets or sets the color settings for a goalkeeper.
        /// </summary>
        public PlayerColor Goalkeeper { get; set; } = new PlayerColor();
    }

    /// <summary>
    /// Represents the color settings for a player.
    /// </summary>
    public class PlayerColor
    {
        /// <summary>
        /// Gets or sets the primary color of the player.
        /// </summary>
        public string Primary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number color of the player.
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the border color of the player.
        /// </summary>
        public string Border { get; set; } = string.Empty;
    }
}
