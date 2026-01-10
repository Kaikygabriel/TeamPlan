using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Member : Entity
{
    private Member()
    {
        
    }
    private Member(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public string Name { get;private set; }
    public Team Team { get;private set; }
    public string Role { get;private set; }
    public List<Task> Tasks { get;private set; } = new();

    public Result AddTeam(Team team, string role)
    {
        Team = team;
        Role = role;
        return Result.Success();
    }

    public void UpdateRole(string role)
        => Role = role;
    public static class Factories
    {
        public static Result<Member> Create(string name)
        {
            return Result<Member>.Success(new(name));
        }
    }
}