namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the <see cref="Fixture"/> entity in the database.
    /// </summary>
    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        /// <summary>
        /// Configures the <see cref="Fixture"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedNever();

            builder.Property(f => f.Referee).HasMaxLength(100);
            builder.Property(f => f.Timezone).IsRequired().HasMaxLength(100);
            builder.Property(f => f.Date).IsRequired();
            builder.Property(f => f.Timestamp).IsRequired().HasMaxLength(100);
            builder.Property(f => f.StatusLong).HasMaxLength(100);
            builder.Property(f => f.StatusShort).HasMaxLength(10);
            builder.Property(f => f.TimeElapsed).IsRequired();
            builder.Property(f => f.Round).HasMaxLength(50);

            builder.HasOne(f => f.Venue)
                .WithMany()
                .HasForeignKey(f => f.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.League)
                .WithMany()
                .HasForeignKey(f => f.LeagueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Season)
                .WithMany()
                .HasForeignKey(f => f.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.HomeTeam)
                .WithMany()
                .HasForeignKey(f => f.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.AwayTeam)
                .WithMany()
                .HasForeignKey(f => f.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure one-to-one relationship with FixtureStats
            builder.HasOne(f => f.Stats)
                   .WithOne()
                   .HasForeignKey<FixtureStats>(fs => fs.FixtureId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
