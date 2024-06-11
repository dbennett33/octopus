namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a player entity.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the player name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's first name.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's last name.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the player's nationality.
        /// </summary>
        public string Nationality { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's height.
        /// </summary>
        public string Height { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's weight.
        /// </summary>
        public string Weight { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the player is injured.
        /// </summary>
        public bool Injured { get; set; }

        /// <summary>
        /// Gets or sets the player's photo.
        /// </summary>
        public string Photo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's statistics.
        /// </summary>
        public ICollection<PlayerStatistics> Statistics { get; set; } = new List<PlayerStatistics>();
    }

    /// <summary>
    /// Represents the statistics of a player.
    /// </summary>
    public class PlayerStatistics
    {
        /// <summary>
        /// Gets or sets the statistics ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; } = new Player();

        /// <summary>
        /// Gets or sets the team ID.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        public Team Team { get; set; } = new Team();

        /// <summary>
        /// Gets or sets the league ID.
        /// </summary>
        public int LeagueId { get; set; }

        /// <summary>
        /// Gets or sets the league.
        /// </summary>
        public League League { get; set; } = new League();

        /// <summary>
        /// Gets or sets the games statistics.
        /// </summary>
        public Games Games { get; set; } = new Games();

        /// <summary>
        /// Gets or sets the substitutes statistics.
        /// </summary>
        public Substitutes Substitutes { get; set; } = new Substitutes();

        /// <summary>
        /// Gets or sets the shots statistics.
        /// </summary>
        public Shots Shots { get; set; } = new Shots();

        /// <summary>
        /// Gets or sets the goals statistics.
        /// </summary>
        public Goals Goals { get; set; } = new Goals();

        /// <summary>
        /// Gets or sets the passes statistics.
        /// </summary>
        public Passes Passes { get; set; } = new Passes();

        /// <summary>
        /// Gets or sets the tackles statistics.
        /// </summary>
        public Tackles Tackles { get; set; } = new Tackles();

        /// <summary>
        /// Gets or sets the duels statistics.
        /// </summary>
        public Duels Duels { get; set; } = new Duels();

        /// <summary>
        /// Gets or sets the dribbles statistics.
        /// </summary>
        public Dribbles Dribbles { get; set; } = new Dribbles();

        /// <summary>
        /// Gets or sets the fouls statistics.
        /// </summary>
        public Fouls Fouls { get; set; } = new Fouls();

        /// <summary>
        /// Gets or sets the cards statistics.
        /// </summary>
        public Cards Cards { get; set; } = new Cards();

        /// <summary>
        /// Gets or sets the penalty statistics.
        /// </summary>
        public Penalty Penalty { get; set; } = new Penalty();
    }

    /// <summary>
    /// Represents the games statistics.
    /// </summary>
    public class Games
    {  
        /// <summary>
        /// Gets or sets the number of appearances.
        /// </summary>
        public int Appearances { get; set; }

        /// <summary>
        /// Gets or sets the number of lineups.
        /// </summary>
        public int Lineups { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes played.
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Gets or sets the player's number.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's rating.
        /// </summary>
        public string Rating { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the player is a captain.
        /// </summary>
        public bool Captain { get; set; }
    }

    /// <summary>
    /// Represents the substitutes statistics.
    /// </summary>
    public class Substitutes
    {
        /// <summary>
        /// Gets or sets the number of times the player was substituted in.
        /// </summary>
        public int In { get; set; }

        /// <summary>
        /// Gets or sets the number of times the player was substituted out.
        /// </summary>
        public int Out { get; set; }

        /// <summary>
        /// Gets or sets the number of times the player was on the bench.
        /// </summary>
        public int Bench { get; set; }
    }

    /// <summary>
    /// Represents the shots statistics.
    /// </summary>
    public class Shots
    {
        /// <summary>
        /// Gets or sets the total number of shots.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of shots on target.
        /// </summary>
        public int On { get; set; }
    }

    /// <summary>
    /// Represents the goals statistics.
    /// </summary>
    public class Goals
    {
        /// <summary>
        /// Gets or sets the total number of goals scored.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of goals conceded.
        /// </summary>
        public int? Conceded { get; set; }

        /// <summary>
        /// Gets or sets the number of assists.
        /// </summary>
        public int Assists { get; set; }

        /// <summary>
        /// Gets or sets the number of saves made.
        /// </summary>
        public int Saves { get; set; }
    }

    /// <summary>
    /// Represents the passes statistics.
    /// </summary>
    public class Passes
    {
        /// <summary>
        /// Gets or sets the total number of passes.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of key passes.
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Gets or sets the pass accuracy percentage.
        /// </summary>
        public int Accuracy { get; set; }
    }

    /// <summary>
    /// Represents the tackles statistics.
    /// </summary>
    public class Tackles
    {
        /// <summary>
        /// Gets or sets the total number of tackles.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the number of blocks made.
        /// </summary>
        public int Blocks { get; set; }

        /// <summary>
        /// Gets or sets the number of interceptions made.
        /// </summary>
        public int Interceptions { get; set; }
    }

    /// <summary>
    /// Represents the duels statistics.
    /// </summary>
    public class Duels
    {
        /// <summary>
        /// Gets or sets the total number of duels.
        /// </summary>
        public int? Total { get; set; }

        /// <summary>
        /// Gets or sets the number of duels won.
        /// </summary>
        public int? Won { get; set; }
    }

    /// <summary>
    /// Represents the dribbles statistics.
    /// </summary>
    public class Dribbles
    {
        /// <summary>
        /// Gets or sets the number of dribble attempts.
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// Gets or sets the number of successful dribbles.
        /// </summary>
        public int Success { get; set; }

        /// <summary>
        /// Gets or sets the number of dribbles past opponents.
        /// </summary>
        public int? Past { get; set; }
    }

    /// <summary>
    /// Represents the fouls statistics.
    /// </summary>
    public class Fouls
    {
        /// <summary>
        /// Gets or sets the number of fouls drawn.
        /// </summary>
        public int Drawn { get; set; }

        /// <summary>
        /// Gets or sets the number of fouls committed.
        /// </summary>
        public int Committed { get; set; }
    }

    /// <summary>
    /// Represents the cards statistics.
    /// </summary>
    public class Cards
    {
        /// <summary>
        /// Gets or sets the number of yellow cards received.
        /// </summary>
        public int Yellow { get; set; }

        /// <summary>
        /// Gets or sets the number of yellow-red cards received.
        /// </summary>
        public int YellowRed { get; set; }

        /// <summary>
        /// Gets or sets the number of red cards received.
        /// </summary>
        public int Red { get; set; }
    }

    /// <summary>
    /// Represents the penalty statistics.
    /// </summary>
    public class Penalty
    {
        /// <summary>
        /// Gets or sets the number of penalties won.
        /// </summary>
        public int Won { get; set; }

        /// <summary>
        /// Gets or sets the number of penalties committed.
        /// </summary>
        public int? Commited { get; set; }

        /// <summary>
        /// Gets or sets the number of penalties scored.
        /// </summary>
        public int Scored { get; set; }

        /// <summary>
        /// Gets or sets the number of penalties missed.
        /// </summary>
        public int Missed { get; set; }

        /// <summary>
        /// Gets or sets the number of penalties saved.
        /// </summary>
        public int? Saved { get; set; }
    }
}
