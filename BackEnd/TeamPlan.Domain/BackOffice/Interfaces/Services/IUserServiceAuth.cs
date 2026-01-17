using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;
using Task = System.Threading.Tasks.Task;

namespace TeamPlan.Domain.BackOffice.Interfaces.Services;

public interface IUserServiceAuth
{
    Task<Result<User>> VerifyUserIsValid(string email, string password);
}   