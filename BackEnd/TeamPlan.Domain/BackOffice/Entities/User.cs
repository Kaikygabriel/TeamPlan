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
    private User(Email email, Password password)
    {
        Email = email;
        Password = password;
        Id = Guid.NewGuid();
    }

    public Email Email { get;init; }
    public Password Password { get;private set; }

    public Result AlterPassword(string password)
        => Password.Update(password);
   
    public static class Factories
    {
        public static Result<User> Create(Password password,Email email)
        {
            return Result<User>.Success(new(email,password));
        }
    }
}