using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configuration
{
    public class TeamStatsConfiguration : IEntityTypeConfiguration<TeamStats>
    {
        public void Configure(EntityTypeBuilder<TeamStats> builder)
        {
            builder.HasKey(ts => ts.Id);

            builder.HasOne(ts => ts.Team)
                   .WithMany(t => t.TeamStats)
                   .HasForeignKey(ts => ts.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ts => ts.League)
                   .WithMany()
                   .HasForeignKey(ts => ts.LeagueId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.Season)
                   .WithMany()
                   .HasForeignKey(ts => ts.SeasonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(ts => ts.Biggest, sb =>
            {
                sb.Property(b => b.BiggestHomeWin).HasMaxLength(50);
                sb.Property(b => b.BiggestAwayWin).HasMaxLength(50);
                sb.Property(b => b.BiggestHomeLoss).HasMaxLength(50);
                sb.Property(b => b.BiggestAwayLoss).HasMaxLength(50);
            });

            builder.OwnsOne(ts => ts.Goals, sg =>
            {
                sg.Property(g => g.HomeAverage).HasPrecision(5, 4);
                sg.Property(g => g.AwayAverage).HasPrecision(5, 4);
                sg.Property(g => g.TotalAverage).HasPrecision(5, 4);
                sg.Property(g => g.HomeTotal).HasColumnType("int");
                sg.Property(g => g.AwayTotal).HasColumnType("int");
                sg.Property(g => g.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.CleanSheets, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.FailedToScore, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.Wins, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.Draws, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.Losses, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.Played, shat =>
            {
                shat.Property(hat => hat.Home).HasColumnType("int");
                shat.Property(hat => hat.Away).HasColumnType("int");
                shat.Property(hat => hat.Total).HasColumnType("int");
            });

            builder.OwnsOne(ts => ts.Penalties, sp =>
            {
                sp.OwnsOne(p => p.Scored);
                sp.OwnsOne(p => p.Missed);
            });

            builder.Property(ts => ts.Form)
                   .HasMaxLength(100);
        }
    }
}
