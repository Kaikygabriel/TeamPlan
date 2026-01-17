using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class MarkMap  : IEntityTypeConfiguration<Mark>
{
    public void Configure(EntityTypeBuilder<Mark> builder)
    {
        builder.ToTable("Mark");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(120)
            .HasColumnType("VARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.Descriptor)
            .HasMaxLength(180)
            .HasColumnType("VARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.Percentage)
            .HasColumnType("TINYINT")
            .HasDefaultValue(0)
            .IsRequired(true);
        
        builder.Property(x => x.TaskCountDone)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(0)
            .IsRequired(true);

        builder.Property(x => x.TaskCount)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(0)
            .IsRequired(true);
        
        builder.HasOne(x => x.Team)
            .WithMany(x => x.Marks)
            .HasForeignKey(x => x.TeamId)
            .HasConstraintName("FK_Mark_Team")
            .OnDelete(DeleteBehavior.Cascade);
    }
}