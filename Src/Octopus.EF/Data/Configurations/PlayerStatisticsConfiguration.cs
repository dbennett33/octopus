using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configurations
{
    public class PlayerStatisticsConfiguration : IEntityTypeConfiguration<PlayerStatistics>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistics> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.HasOne(ps => ps.Player)
                   .WithMany(p => p.Statistics)
                   .HasForeignKey(ps => ps.PlayerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ps => ps.Team)
                   .WithMany()
                   .HasForeignKey(ps => ps.TeamId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ps => ps.League)
                   .WithMany()
                   .HasForeignKey(ps => ps.LeagueId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(ps => ps.Games);
            builder.OwnsOne(ps => ps.Substitutes);
            builder.OwnsOne(ps => ps.Shots);
            builder.OwnsOne(ps => ps.Goals);
            builder.OwnsOne(ps => ps.Passes);
            builder.OwnsOne(ps => ps.Tackles);
            builder.OwnsOne(ps => ps.Duels);
            builder.OwnsOne(ps => ps.Dribbles);
            builder.OwnsOne(ps => ps.Fouls);
            builder.OwnsOne(ps => ps.Cards);
            builder.OwnsOne(ps => ps.Penalty);
        }
    }
}
