namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Member;

public interface IMemberRepository : IRepository<Entities.Member>
{
    Task<Entities.Member> GetByEmail(string email);
}