using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Infra.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasConversion(x => x.Address, x => Email.Factories.Create(x).Value)
            .HasMaxLength(180)
            .HasColumnType("NVARCHAR")
            .HasColumnName("Email")
            .IsRequired(true);
        
        builder.Property(x => x.Password)
            .HasConversion(x => x.PasswordHash, x => Password.Factory.Create(x).Value)
            .HasMaxLength(100)
            .HasColumnType("NVARCHAR")
            .HasColumnName("Password")
            .IsRequired(true);
    }
}