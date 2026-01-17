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
            .Include(x=>x.Team)
            .Include(x=>x.Member)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.Task>> GetTasksByTeamid(Guid teamId)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Where(x => x.TeamId == teamId)
            .ToListAsync();
    }
}