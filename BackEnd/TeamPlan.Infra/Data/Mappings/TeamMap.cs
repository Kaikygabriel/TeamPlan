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
            .WithOne(x => x.Team)
            .HasForeignKey<Team>(x=>x.ManagerId)
            .HasConstraintName("FK_Team_Manager")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Manager)
            .WithOne()
            .HasForeignKey<Team>(x => x.ManagerId)
            .HasConstraintName("FK_Team_Manager")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Enterprise)
            .WithMany(x => x.Teams)
            .HasForeignKey(x => x.EnterpriseId)
            .HasConstraintName("FK_Team_Enterprise")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Members)
            .WithOne(x => x.Team) 
            .HasForeignKey(x => x.TeamId)
            .HasConstraintName("FK_Member_Team")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.OwnsMany(x => x.Kanbans, x =>
                {
                    x.HasKey(x => x.Id);
                    x.WithOwner().HasForeignKey("TeamId");
        
                    x.Property(x => x.Title)
                        .HasColumnName("KanbanTitle")
                        .HasColumnType("VARCHAR")
                        .HasMaxLength(120)
                        .IsRequired();
        
                    x.Property(x => x.Order)
                        .HasColumnName("KanbanOrder")
                        .HasColumnType("TINYINT")
                        .IsRequired();
                });

    }
}