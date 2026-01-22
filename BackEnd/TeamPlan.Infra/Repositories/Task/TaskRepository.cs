using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.Task;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.Task;

public class TaskRepository : Repository<Domain.BackOffice.Entities.Task>,ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Domain.BackOffice.Entities.Task?> GetByIdWithTeamWithMember(Guid id)
    {
        return await _context.Tasks
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.Comments.Where(c => c.CommentParentId == null))
                .ThenInclude(x=>x.SubComments)
            .Include(x=>x.Team)
            .Include(x=>x.Member)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Task>> GetTasksByTeamId(Guid teamId,int skip =0,int take=25)
    {
        return await _context.Tasks
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.Comments.Where(c => c.CommentParentId == null))
              .ThenInclude(x=>x.SubComments)
            .Where(x => x.TeamId == teamId)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}