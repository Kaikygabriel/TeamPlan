using System.Reflection;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Owner : Entity
{
    private Owner()
    {
        
    }
    private Owner(string name,User user)
    {
        User = user;
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get;private set; }
    public User User { get;private set; }
    public Enterprise Enterprise  { get;private set; }

    public void CreateEnterprise(Enterprise enterprise)
    {
        Enterprise = enterprise;
    }
    
    public static class Factories
    {
        public static Result<Owner> Create(string name,User user)
        {
            return Result<Owner>.Success(new(name,user));
        }
    }
}