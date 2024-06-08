using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(t => t.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Founded)
                .HasMaxLength(4);

            builder.Property(t => t.IsNationalTeam)
                .IsRequired();

            builder.Property(t => t.Logo)
                .HasMaxLength(200);

            builder.HasOne(t => t.Venue)
                .WithMany()
                .HasForeignKey(t => t.VenueId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(t => t.TeamStats)
                .WithOne(ts => ts.Team)
                .HasForeignKey(ts => ts.TeamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
