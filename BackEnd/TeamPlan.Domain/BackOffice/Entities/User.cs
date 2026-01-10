using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;
using TeamPlan.Domain.BackOffice.ValueObject;

namespace TeamPlan.Domain.BackOffice.Entities;

public class User : Entity
{
    private User()
    {
        
    }
    private User(Email email, string password)
    {
        Email = email;
        Password = password;
        Id = Guid.NewGuid();
    }

    public Email Email { get;private set; }
    public string Password { get;private set; }

    public Result AlterPassword(string password)
    {
        if(PasswordIsInvalid(password))
            return Result.Failure(new("Password.Invalid","Password in user invalid!"));
        Password = password;
        return Result.Success();
    }
    public static class Factories
    {
        public static Result<User> Create(string password,Email email)
        {
            if (PasswordIsInvalid(password))
                return Result<User>.Failure(new("Password.Invalid","Password in user invalid!"));
            return Result<User>.Success(new(email,password));
        }
    }

    private static bool PasswordIsInvalid(string password)
        => string.IsNullOrWhiteSpace(password) || password.Length < 3;
}