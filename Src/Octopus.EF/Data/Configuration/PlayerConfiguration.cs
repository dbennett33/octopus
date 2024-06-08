using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Firstname)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Lastname)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Age)
                   .IsRequired();

            builder.Property(p => p.Nationality)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Height)
                   .HasMaxLength(10);

            builder.Property(p => p.Weight)
                   .HasMaxLength(10);

            builder.Property(p => p.Injured)
                   .IsRequired();

            builder.Property(p => p.Photo)
                   .HasMaxLength(200);

            builder.HasMany(p => p.Statistics)
                   .WithOne(ps => ps.Player)
                   .HasForeignKey(ps => ps.PlayerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
