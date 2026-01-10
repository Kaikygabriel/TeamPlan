using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Team : Entity
{
    private Team()
    {
        
    }
    private Team(string name, Member manager)
    {
        Name = name;
        Manager = manager;
        Id = Guid.NewGuid();
    }

    public string Name { get;private set; }
    public Member Manager { get;private set; }
    public Guid ManagerId { get;private set; }
    public List<Member> Members { get; private set; } = new();
    public List<Task>Tasks { get;private set; } = new();
    public Enterprise Enterprise { get;private set; }
    public Guid  EnterpriseId { get; set; }

    public void AddTask(Task task)
        => Tasks.Add(task);

    public void RemoveTask(Task task)
        => Tasks.Remove(task);

    public void AddMember(Member member)
        => Members.Add(member);
    
    public void RemoveMember(Member member)
        => Members.Remove(member);

    public static class Factories
    {
        public static Result<Team> Create(string name, Member manager)
        {
            return Result<Team>.Success(new Team(name,manager));
        }
    }
    
}