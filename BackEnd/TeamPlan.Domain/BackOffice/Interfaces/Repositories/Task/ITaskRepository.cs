namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Task;

public interface ITaskRepository : IRepository<Entities.Task>
{
    Task<Entities.Task> GetByIdWithTeamWithMember(Guid id);
    Task<IEnumerable<Entities.Task>> GetTasksByTeamid(Guid teamId);
}