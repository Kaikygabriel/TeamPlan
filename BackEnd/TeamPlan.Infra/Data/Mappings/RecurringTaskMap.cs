using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public sealed class RecurringTaskMap : IEntityTypeConfiguration<RecurringTask>
{
    public void Configure(EntityTypeBuilder<RecurringTask> builder)
    {
        builder.ToTable("RecurringTask");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.DayMonth)
            .HasColumnType("TINYINT")
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasMaxLength(180)
            .HasColumnType("NVARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.Title)
            .HasMaxLength(120)
            .HasColumnType("NVARCHAR")
            .IsRequired(true);

        builder.HasOne(x => x.Team)
            .WithMany(x => x.RecurringTasks)
            .HasForeignKey(x => x.TeamId)
            .HasConstraintName("FK_RecurringTask_Team")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.Priority)
            .HasConversion<string>();
    }
}