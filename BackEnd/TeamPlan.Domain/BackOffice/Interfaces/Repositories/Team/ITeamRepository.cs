namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Team;

public interface ITeamRepository : IRepository<Entities.Team>
{
    Task<Entities.Team> GetByIdWithMembersWithTask(Guid id);
    Task<Entities.Team> GetByIdWithMember(Guid id);
    Task<IEnumerable<Entities.Task>> GetTasksInMonthByTaskId(Guid id);
}