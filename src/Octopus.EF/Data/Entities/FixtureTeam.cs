namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a team in a fixture.
    /// </summary>
    public class FixtureTeam
    {
        /// <summary>
        /// Gets or sets the ID of the team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the logo of the team.
        /// </summary>
        public string Logo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the team is the winner.
        /// </summary>
        public bool Winner { get; set; }
    }
}