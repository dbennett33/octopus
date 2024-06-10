namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Configuration class for the <see cref="FixtureLineup"/> entity.
    /// </summary>
    public class FixtureLineupConfiguration : IEntityTypeConfiguration<FixtureLineup>
    {
        /// <summary>
        /// Configures the <see cref="FixtureLineup"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<FixtureLineup> builder)
        {
            builder.HasKey(fl => fl.Id);

            builder.HasOne(fl => fl.Team)
                .WithMany()
                .HasForeignKey(fl => fl.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(fl => fl.Formation).HasMaxLength(10);

            builder.HasMany(fl => fl.StartXI)
                .WithOne(sxi => sxi.FixtureLineup)
                .HasForeignKey(sxi => sxi.FixtureLineupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(fl => fl.Substitutes)
                .WithOne(sub => sub.FixtureLineup)
                .HasForeignKey(sub => sub.FixtureLineupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fl => fl.Coach)
                .WithMany()
                .HasForeignKey(fl => fl.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    /// <summary>
    /// Configuration class for the <see cref="StartXI"/> entity.
    /// </summary>
    public class StartXIConfiguration : IEntityTypeConfiguration<StartXI>
    {
        /// <summary>
        /// Configures the <see cref="StartXI"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<StartXI> builder)
        {
            builder.HasKey(sxi => sxi.Id);

            builder.HasOne(sxi => sxi.Player)
                .WithMany()
                .HasForeignKey(sxi => sxi.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sxi => sxi.FixtureLineup)
                .WithMany(fl => fl.StartXI)
                .HasForeignKey(sxi => sxi.FixtureLineupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    /// <summary>
    /// Configuration class for the <see cref="Substitute"/> entity.
    /// </summary>
    public class SubstituteConfiguration : IEntityTypeConfiguration<Substitute>
    {
        /// <summary>
        /// Configures the <see cref="Substitute"/> entity.
        /// </summary>
        /// <param name="builder">The entity type builder.</param>
        public void Configure(EntityTypeBuilder<Substitute> builder)
        {
            builder.HasKey(sub => sub.Id);

            builder.HasOne(sub => sub.Player)
                .WithMany()
                .HasForeignKey(sub => sub.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sub => sub.FixtureLineup)
                .WithMany(fl => fl.Substitutes)
                .HasForeignKey(sub => sub.FixtureLineupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
