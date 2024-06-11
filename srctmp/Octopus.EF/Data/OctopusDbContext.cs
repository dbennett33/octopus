using Microsoft.EntityFrameworkCore;
using Octopus.EF.Data.Configurations;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data
{
    /// <summary>
    /// Represents the database context for the Octopus application.
    /// </summary>
    public class OctopusDbContext : DbContext
    {
        public DbSet<SystemSettings> SystemSettings { get; set; }
        public DbSet<InstallInfo> InstallInfo { get; set; }
        /// <summary>
        /// Gets or sets the DbSet for the Country entity.
        /// </summary>
        public DbSet<Country> Countries { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the League entity.
        /// </summary>
        public DbSet<League> Leagues { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Team entity.
        /// </summary>
        public DbSet<Team> Teams { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Venue entity.
        /// </summary>
        public DbSet<Venue> Venues { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Fixture entity.
        /// </summary>
        public DbSet<Fixture> Fixtures { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Season entity.
        /// </summary>
        public DbSet<Season> Seasons { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the TeamStats entity.
        /// </summary>
        public DbSet<TeamStats> TeamStats { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the StatsFormation entity.
        /// </summary>
        public DbSet<StatsFormation> StatsFormations { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the FixtureStats entity.
        /// </summary>
        public DbSet<FixtureStats> FixtureStats { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Player entity.
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the PlayerStatistics entity.
        /// </summary>
        public DbSet<PlayerStatistics> PlayerStatistics { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the FixtureLineup entity.
        /// </summary>
        public DbSet<FixtureLineup> FixtureLineups { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the StartXI entity.
        /// </summary>
        public DbSet<StartXI> StartXIs { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Substitute entity.
        /// </summary>
        public DbSet<Substitute> Substitutes { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the Coach entity.
        /// </summary>
        public DbSet<Coach> Coaches { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OctopusDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public OctopusDbContext(DbContextOptions<OctopusDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configures the model that was discovered from the entity types exposed in DbSet properties.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entities
            modelBuilder.ApplyConfiguration(new SystemSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new InstallInfoConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new VenueConfiguration());
            modelBuilder.ApplyConfiguration(new FixtureConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonConfiguration());
            modelBuilder.ApplyConfiguration(new CoverageConfiguration());
            modelBuilder.ApplyConfiguration(new TeamStatsConfiguration());
            modelBuilder.ApplyConfiguration(new FixtureStatsConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerStatisticsConfiguration());
            modelBuilder.ApplyConfiguration(new FixtureLineupConfiguration());
            modelBuilder.ApplyConfiguration(new StartXIConfiguration());
            modelBuilder.ApplyConfiguration(new SubstituteConfiguration());
            modelBuilder.ApplyConfiguration(new CoachConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
