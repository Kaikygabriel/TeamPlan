namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.Owner;

public interface IOwnerRepository : IRepository<Entities.Owner>
{
    Task<Entities.Owner> GetOwnerByEmail(string email);
}