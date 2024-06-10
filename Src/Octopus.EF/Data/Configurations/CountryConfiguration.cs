namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the Country entity in the database.
    /// </summary>
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        /// <summary>
        /// Configures the Country entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);


            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Code)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(c => c.Flag)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.HasMany(c => c.Leagues)
                    .WithOne(l => l.Country)
                    .HasForeignKey(l => l.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
