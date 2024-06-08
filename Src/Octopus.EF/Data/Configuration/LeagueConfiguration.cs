namespace Octopus.EF.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the League entity in the database.
    /// </summary>
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        /// <summary>
        /// Configures the League entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the League entity.</param>
        public void Configure(EntityTypeBuilder<League> builder)
        {
            // Configure the primary key
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedNever();

            // Configure properties
            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Type)
                .HasMaxLength(50);

            builder.Property(l => l.Logo)
                .HasMaxLength(200);

            // Configure the relationship with Country
            builder.HasOne(l => l.Country)
                .WithMany(c => c.Leagues)
                .HasForeignKey(l => l.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship with Seasons
            builder.HasMany(l => l.Seasons)
                .WithOne(s => s.League)
                .HasForeignKey(s => s.LeagueId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: configure cascading delete behavior
        }
    }
}
