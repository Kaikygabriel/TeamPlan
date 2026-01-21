namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Comment;

public interface ICommentRepository : IRepository<Entities.Comment>
{
    Task<Entities.Comment?> GetByIdWithTaskAsync(Guid id);
}