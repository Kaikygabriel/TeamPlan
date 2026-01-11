using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Member : Entity
{
    private Member(User user)
    {
        User = user;
    }
    private Member(string name, User user)
    {
        Name = name;
        User = user;
        Id = Guid.NewGuid();
    }

    public User User { get; set; }
    public string Name { get;private set; }
    public Team Team { get;private set; }
    public Guid TeamId { get;private set; }
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
        public static Result<Member> Create(string name,User user)
        {
            return Result<Member>.Success(new(name,user));
        }
    }
}