using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Infra.Data.Mappings;

public class CommentMap  : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Message)
            .HasMaxLength(250)
            .HasColumnType("NVARCHAR")
            .IsRequired(true);

        builder.Property(x => x.CreateAt)
            .HasColumnType("SMALLDATETIME")
            .IsRequired(true);

        builder.HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .HasConstraintName("FK_Comments_Member")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TaskId)
            .HasConstraintName("FK_Comments_Task")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CommentParent)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.CommentParentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);//null, cuidado pode estar saindo como not null no banco (Não sei pq )

        builder.HasIndex(x => x.TaskId,"IX_Comments_Task");
    }
}