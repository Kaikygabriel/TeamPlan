using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class TeamMap : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Team");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(120)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.PercentageByMonthCurrent)
            .HasColumnType("TINYINT")
            .HasDefaultValue(0)
            .HasColumnName("PercentageByMonthCurrent")
            .IsRequired(true);
        
        builder.HasOne(x => x.Manager)
            .WithOne(x => x.ManagedTeam)
            .HasForeignKey<Team>(x=>x.ManagerId);
        
        builder.HasMany(x => x.Tasks)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId);

        builder.HasOne(x => x.Enterprise)
            .WithMany(x => x.Teams)
            .HasForeignKey(x => x.EnterpriseId);

        builder.HasMany(x => x.Members)
            .WithOne(x => x.Team)
            .HasForeignKey(x => x.TeamId);
    }
}