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
        
        builder.OwnsOne(x => x.Email, x =>
        {
            x.Property(x => x.Address)
                .HasMaxLength(180)
                .HasColumnType("NVARCHAR")
                .HasColumnName("Email")
                .IsRequired(true);
            x.HasIndex(e => e.Address).IsUnique(); 
        });
        
        builder.Property(x => x.Password)
            .HasConversion(x => x.PasswordHash, x => Password.Factory.CreateWithPasswordHashAlready(x).Value)
            .HasMaxLength(100)
            .HasColumnType("NVARCHAR")
            .HasColumnName("Password")
            .IsRequired(true);

    }
}