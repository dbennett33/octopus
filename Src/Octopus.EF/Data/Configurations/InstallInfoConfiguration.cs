using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.EF.Data.Entities;

namespace Octopus.EF.Data.Configurations;

public class InstallInfoConfiguration : IEntityTypeConfiguration<InstallInfo>
{
    public void Configure(EntityTypeBuilder<InstallInfo> builder)
    {
        builder.ToTable("InstallInfo");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.SystemSettingsId)
            .HasColumnName("SystemSettingsId");

        builder.Property(e => e.Version)
            .HasColumnName("Version")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.InstallDate)
            .HasColumnName("InstallDate")
            .IsRequired();

        builder.Property(e => e.CountriesInstalled)
            .HasColumnName("CountriesInstalled")
            .IsRequired();

        builder.Property(e => e.LeaguesInstalled)
            .HasColumnName("LeaguesInstalled")
            .IsRequired();

        builder.HasOne(e => e.SystemSettings)
            .WithMany()
            .HasForeignKey(e => e.SystemSettingsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}