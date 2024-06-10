namespace Octopus.EF.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Octopus.EF.Data.Entities;

    /// <summary>
    /// Represents the configuration for the Coach entity in the database.
    /// </summary>
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        /// <summary>
        /// Configures the Coach entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the Coach entity.</param>
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(l => l.Id).ValueGeneratedNever();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Photo).HasMaxLength(200);
        }
    }
}
