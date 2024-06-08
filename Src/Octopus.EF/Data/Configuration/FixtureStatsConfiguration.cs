namespace Octopus.EF.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the FixtureStats entity in the database.
    /// </summary>
    public class FixtureStatsConfiguration : IEntityTypeConfiguration<FixtureStats>
    {
        /// <summary>
        /// Configures the FixtureStats entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<FixtureStats> builder)
        {
            // Configure primary key
            builder.HasKey(fs => fs.Id);

            // Configure relationship with Fixture entity
            builder.HasOne(fs => fs.Fixture)
                   .WithOne(f => f.Stats)
                   .HasForeignKey<FixtureStats>(fs => fs.FixtureId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with Team entity
            builder.HasOne(fs => fs.Team)
                   .WithMany()
                   .HasForeignKey(fs => fs.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure properties
            builder.Property(fs => fs.ShotsOnGoal).IsRequired();
            builder.Property(fs => fs.ShotsOffGoal).IsRequired();
            builder.Property(fs => fs.TotalShots).IsRequired();
            builder.Property(fs => fs.BlockedShots).IsRequired();
            builder.Property(fs => fs.ShotsInsideBox).IsRequired();
            builder.Property(fs => fs.ShotsOutsideBox).IsRequired();
            builder.Property(fs => fs.Fouls).IsRequired();
            builder.Property(fs => fs.CornerKicks).IsRequired();
            builder.Property(fs => fs.Offsides).IsRequired();
            builder.Property(fs => fs.BallPossession).IsRequired();
            builder.Property(fs => fs.YellowCards).IsRequired();
            builder.Property(fs => fs.RedCards).IsRequired();
            builder.Property(fs => fs.GoalkeeperSaves).IsRequired();
            builder.Property(fs => fs.TotalPasses).IsRequired();
            builder.Property(fs => fs.PassesAccurate).IsRequired();
            builder.Property(fs => fs.PassesPercentage).IsRequired();
        }
    }
}
