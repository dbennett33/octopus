using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configurations
{
    public class SystemSettingsConfiguration : IEntityTypeConfiguration<SystemSettings>
    {
        public void Configure(EntityTypeBuilder<SystemSettings> builder)
        {
            // Table name if different from class name
            builder.ToTable("SystemSettings");

            // Primary key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.CurrentVersion)
                .IsRequired()
                .HasMaxLength(100); // Adjust the length as needed

            // Relationships
            // If there are no relationships to configure from this side, you can leave this section empty
        }
    }
}