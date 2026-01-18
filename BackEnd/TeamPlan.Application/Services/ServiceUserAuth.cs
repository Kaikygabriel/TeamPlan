using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities;
using TeamPlan.Domain.BackOffice.Interfaces.Repositories;
using TeamPlan.Domain.BackOffice.Interfaces.Services;

namespace TeamPlan.Application.Services;

internal class ServiceUserAuth:  IUserServiceAuth
{
    private readonly IUnitOfWork _unitOfWork;

    public ServiceUserAuth(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

        
    public async Task<Result<User>> VerifyUserIsValid(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user is null)
            return Result<User>.Failure(new("User.NoExists", "User no exists !"));
        var result = VerifyPassword(user, password);
        if(!result)
            return Result<User>.Failure(new("Password.Invalid", "Password is invalid !"));
        
        return Result<User>.Success(user);
    }

    private async Task<User?> GetUserByEmail(string email)
        => await _unitOfWork.UserRepository.GetByPredicate(x => x.Email.Address == email);

    private bool VerifyPassword(User user, string password)
        => BCrypt.Net.BCrypt.Verify(password,user.Password.PasswordHash);
}