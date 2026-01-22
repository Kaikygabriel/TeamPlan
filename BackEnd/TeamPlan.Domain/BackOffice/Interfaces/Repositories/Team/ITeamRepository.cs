namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Team;

public interface ITeamRepository : IRepository<Entities.Team>
{
    Task<Entities.Team> GetByIdWithManagerAndEnterprise(Guid id);
    Task<Entities.Team> GetByIdWithMember(Guid id);
    Task<IEnumerable<Entities.Task>> GetTasksInMonthByTeamId(Guid id);
}