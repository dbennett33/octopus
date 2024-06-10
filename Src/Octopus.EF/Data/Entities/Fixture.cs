namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a fixture in a sports league.
    /// </summary>
    public class Fixture
    {
        /// <summary>
        /// Gets or sets the unique identifier of the fixture.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the referee for the fixture.
        /// </summary>
        public string Referee { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timezone of the fixture.
        /// </summary>
        public string Timezone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the fixture.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the fixture.
        /// </summary>
        public string Timestamp { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the venue for the fixture.
        /// </summary>
        public int VenueId { get; set; }

        /// <summary>
        /// Gets or sets the venue for the fixture.
        /// </summary>
        public Venue Venue { get; set; } = new Venue();

        /// <summary>
        /// Gets or sets the long status of the fixture.
        /// </summary>
        public string StatusLong { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the short status of the fixture.
        /// </summary>
        public string StatusShort { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the time elapsed in the fixture.
        /// </summary>
        public int TimeElapsed { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the league for the fixture.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the league for the fixture.
        /// </summary>
        public League League { get; set; } = new League();

        /// <summary>
        /// Gets or sets the unique identifier of the season for the fixture.
        /// </summary>
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season for the fixture.
        /// </summary>
        public Season Season { get; set; } = new Season();

        /// <summary>
        /// Gets or sets the round of the fixture.
        /// </summary>
        public string Round { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the home team for the fixture.
        /// </summary>
        public int HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the home team for the fixture.
        /// </summary>
        public Team HomeTeam { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the unique identifier of the away team for the fixture.
        /// </summary>
        public int AwayTeamId { get; set; }

        /// <summary>
        /// Gets or sets the away team for the fixture.
        /// </summary>
        public Team AwayTeam { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the number of goals scored by the home team in the fixture.
        /// </summary>
        public int GoalsHomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the away team in the fixture.
        /// </summary>
        public int GoalsAwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the home team in the first half of the fixture.
        /// </summary>
        public int GoalsHomeTeamHalfTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the away team in the first half of the fixture.
        /// </summary>
        public int GoalsAwayTeamHalfTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the home team in the full time of the fixture.
        /// </summary>
        public int GoalsHomeTeamFullTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the away team in the full time of the fixture.
        /// </summary>
        public int GoalsAwayTeamFullTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the home team in the extra time of the fixture.
        /// </summary>
        public int GoalsHomeTeamExtraTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the away team in the extra time of the fixture.
        /// </summary>
        public int GoalsAwayTeamExtraTime { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the home team in the penalty shootout of the fixture.
        /// </summary>
        public int GoalsHomeTeamPenalty { get; set; }

        /// <summary>
        /// Gets or sets the number of goals scored by the away team in the penalty shootout of the fixture.
        /// </summary>
        public int GoalsAwayTeamPenalty { get; set; }

        /// <summary>
        /// Gets or sets the statistics for a fixture.
        /// </summary>
        public FixtureStats Stats { get; set; } = new FixtureStats();
    }
}
