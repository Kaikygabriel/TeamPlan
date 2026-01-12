namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Enterprise;

public interface IEnterpriseRepository : IRepository<Entities.Enterprise>
{
    Task<Entities.Enterprise> GetByIdWithTasks(Guid id);
}