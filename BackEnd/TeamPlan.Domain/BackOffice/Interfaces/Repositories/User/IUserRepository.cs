namespace TeamPlan.Domain.BackOffice.Interfaces.Repositories.User;

public interface IUserRepository : IRepository<Entities.User>
{
    Task<bool> GetUserExistsByEmail(string email);
}