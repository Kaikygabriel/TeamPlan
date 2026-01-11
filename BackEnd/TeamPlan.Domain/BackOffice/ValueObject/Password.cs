using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;

namespace TeamPlan.Domain.BackOffice.ValueObject;

public class Password
{
    private Password(string password)
    {
        PasswordHash = CreateHashPassword(password);
    }

    public string PasswordHash { get;private set; }

    public Result Update(string password)
    {
        if (PasswordIsInvalid(password))
            return Result<Password>.Failure(new("Password.Invalid", "Password is invalid"));
        PasswordHash = CreateHashPassword(password);
        return Result.Success();
    }
    
    private string CreateHashPassword(string password)
        =>BCrypt.Net.BCrypt.HashPassword(password);
    
    public static class Factory
    {
        public static Result<Password> Create(string password)
        {
            if (PasswordIsInvalid(password))
                return Result<Password>.Failure(new("Password.Invalid", "Password is invalid"));
            return Result<Password>.Success(new(password));
        }
    }

    private static bool PasswordIsInvalid(string password)
        =>string.IsNullOrEmpty(password) || password.Length < 3;
}