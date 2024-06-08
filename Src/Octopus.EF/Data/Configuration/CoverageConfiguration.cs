namespace Octopus.EF.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the <see cref="Coverage"/> entity in the database.
    /// </summary>
    public class CoverageConfiguration : IEntityTypeConfiguration<Coverage>
    {
        /// <summary>
        /// Configures the <see cref="Coverage"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Coverage> builder)
        {
            // Configure the primary key
            builder.HasKey(c => c.Id);

            // Configure properties
            builder.Property(c => c.SeasonId)
                .IsRequired();

            builder.Property(c => c.Events)
                .IsRequired();

            builder.Property(c => c.Lineups)
                .IsRequired();

            builder.Property(c => c.FixtureStats)
                .IsRequired();

            builder.Property(c => c.PlayerStats)
                .IsRequired();

            builder.Property(c => c.Standings)
                .IsRequired();

            builder.Property(c => c.Players)
                .IsRequired();

            builder.Property(c => c.TopScorers)
                .IsRequired();

            builder.Property(c => c.TopAssists)
                .IsRequired();

            builder.Property(c => c.TopCards)
                .IsRequired();

            builder.Property(c => c.Injuries)
                .IsRequired();

            builder.Property(c => c.Predictions)
                .IsRequired();

            builder.Property(c => c.Odds)
                .IsRequired();

            // Configure the relationship with Season
            builder.HasOne(c => c.Season)
                .WithOne(s => s.SeasonCoverage)
                .HasForeignKey<Coverage>(c => c.SeasonId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: configure cascading delete behavior
        }
    }
}
