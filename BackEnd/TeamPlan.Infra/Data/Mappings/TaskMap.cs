using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeamPlan.Infra.Data.Mappings;

public class TaskMap : IEntityTypeConfiguration<Domain.BackOffice.Entities.Task>
{
    public void Configure(EntityTypeBuilder<Domain.BackOffice.Entities.Task> builder)
    {
        builder.ToTable("Task");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Percentage)
            .HasColumnType("TINYINT")
            .HasDefaultValue(0)
            .HasColumnName("Percentage")
            .IsRequired(true);

        builder.Property(x => x.Active)
            .HasColumnName("Active")
            .HasColumnType("BIT")
            .HasDefaultValue(true)
            .IsRequired(true);

        builder.Property(x => x.CreateAt)
            .HasColumnName("DATETIME")
            .HasColumnName("CreateAt")
            .IsRequired(true);
        
        builder.Property(x => x.EndDate)
            .HasColumnName("DATETIME")
            .HasColumnName("EndDate")
            .IsRequired(true);

        builder.Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR")
            .IsRequired(true);
        
        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .IsRequired(true);

        builder.HasOne(x => x.Member)
            .WithMany(x => x.Tasks) ;
        
    }
}