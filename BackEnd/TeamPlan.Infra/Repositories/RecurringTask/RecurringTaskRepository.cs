using Microsoft.EntityFrameworkCore;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories.RecurringTask;
using TeamPlan.Infra.Data.Context;

namespace TeamPlan.Infra.Repositories.RecurringTask;

public class RecurringTaskRepository : Repository<Domain.BackOffice.Entities.RecurringTask>,IRecurringTaskRepository
{
    public RecurringTaskRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Domain.BackOffice.Entities.RecurringTask>> GetAllByDayCurrentWithTeam()
    {
        var dayCurrent = DateTime.Now.Day;
        return await _context.RecurringTasks
            .Include(x=>x.Team)
            .Where(x => x.DayMonth == dayCurrent)
            .ToListAsync();
    }
}