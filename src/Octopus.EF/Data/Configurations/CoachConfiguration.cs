using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(l => l.Id).ValueGeneratedNever();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Photo).HasMaxLength(200);
        }
    }
}
