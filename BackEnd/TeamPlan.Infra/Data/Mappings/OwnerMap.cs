using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class OwnerMap : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owner");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(170)
            .IsRequired(true);
        
        builder.HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<Owner>(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
      
        builder.HasOne(x => x.Enterprise)
            .WithOne(x => x.Owner)
            .HasForeignKey<Owner>(x=>x.EnterpriseId)
            .HasConstraintName("FK_Owner_Enterprise")
            .OnDelete(DeleteBehavior.Cascade);
    }
}