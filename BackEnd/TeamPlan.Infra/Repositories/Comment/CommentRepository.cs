using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Comment;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Comment;

public class CommentRepository : Repository<Domain.BackOffice.Entities.Comment>,ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Comment?> GetByIdWithTaskAsync(Guid id)
    {
        return await _context.Comments
            .Include(x=>x.Task)
            .FirstOrDefaultAsync(x=>x.Id == id);
    }
}