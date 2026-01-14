namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.RecurringTask;

public interface IRecurringTaskRepository : IRepository<Entities.RecurringTask>
{
    Task<IEnumerable<Entities.RecurringTask>> GetAllByDayCurrentWithTeam();
}