using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class MemberMap : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Member");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(180)
            .IsRequired(true);

        builder.Property(x => x.Role)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired(true);
       
    }
}