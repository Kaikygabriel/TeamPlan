using System.Text.Json.Serialization;
using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Commum.Abstraction;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Member : Entity
{
    private Member()
    {
      
    }
    private Member(string name, User user)
    {
        Name = name;
        User = user;
        Id = Guid.NewGuid();
        Role = Roles.Member;
    }

    public User User { get; init; }
    public string Name { get;private set; }
    [JsonIgnore]
    public Team? Team { get;private set; }
    public Guid? TeamId { get;private set; }
    public string Role { get;private set; } 
    [JsonIgnore]
    public List<Task> Tasks { get;private set; } = new();
    [JsonIgnore]
    public Team? ManagedTeam { get;private set; }


    public Result AddTeam(Team team, string role)
    {
        Team = team;
        Role = role;
        return Result.Success();
    }

    public void UpdateForManager(Team team)
    {
        Team = team;
        TeamId = team.Id;
        Role = Roles.Manager;
    } 
    public static class Factories
    {
        public static Result<Member> Create(string name,User user)
        {
            return Result<Member>.Success(new(name,user));
        }
    }
}