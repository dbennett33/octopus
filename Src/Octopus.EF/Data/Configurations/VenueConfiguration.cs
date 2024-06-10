namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the Venue entity in the database.
    /// </summary>
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        /// <summary>
        /// Configures the Venue entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            // Configure the primary key
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id).ValueGeneratedNever();

            // Configure properties
            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Address)
                .HasMaxLength(200);

            builder.Property(v => v.City)
                .HasMaxLength(100);

            builder.Property(v => v.Capacity)
                .HasMaxLength(50);

            builder.Property(v => v.Surface)
                .HasMaxLength(50);

            builder.Property(v => v.Image)
                .HasMaxLength(200);
        }
    }
}
