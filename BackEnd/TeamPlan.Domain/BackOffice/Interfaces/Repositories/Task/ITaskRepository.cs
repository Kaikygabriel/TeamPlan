namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Task;

public interface ITaskRepository : IRepository<Entities.Task>
{
    Task<Entities.Task> GetByIdWithTeamWithMember(Guid id);
    Task<IEnumerable<Entities.Task>> GetTasksByTeamId(Guid teamId,int skip =0,int take=25);
}