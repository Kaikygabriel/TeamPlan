using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class EnterpriseMap : IEntityTypeConfiguration<Enterprise>
{
    public void Configure(EntityTypeBuilder<Enterprise> builder)
    {
        builder.ToTable("Enterprise");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80)
            .IsRequired(true);
        
        builder.Property(x=>x.CreateAt)
            .HasColumnType("DATETIME")
            .IsRequired(true);

    }
}