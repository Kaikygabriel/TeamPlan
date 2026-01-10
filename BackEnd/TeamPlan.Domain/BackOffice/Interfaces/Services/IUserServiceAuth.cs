using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities;

namespace TeamPlan.Domain.BackOffice.Interfaces.Services;

public interface IUserServiceAuth
{
    Task<Result> CreateUser(User user);
    Task<Result<User>> VerifyUserIsValid(string email, string password);
}   