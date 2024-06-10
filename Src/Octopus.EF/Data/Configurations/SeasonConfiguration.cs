namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the Season entity in the database.
    /// </summary>
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        /// <summary>
        /// Configures the Season entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Year).IsRequired().HasMaxLength(9);
            builder.Property(s => s.StartDate).IsRequired();
            builder.Property(s => s.EndDate).IsRequired();
            builder.Property(s => s.Current).IsRequired();

            builder.HasOne(s => s.League)
                   .WithMany(l => l.Seasons)
                   .HasForeignKey(s => s.LeagueId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.SeasonCoverage)
                   .WithOne(c => c.Season)
                   .HasForeignKey<Coverage>(c => c.SeasonId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
