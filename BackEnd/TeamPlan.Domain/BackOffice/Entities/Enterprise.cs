using TeamPlan.Domain.BackOffice.Commum;
using TeamPlan.Domain.BackOffice.Entities.Abstraction;

namespace TeamPlan.Domain.BackOffice.Entities;

public class Enterprise: Entity
{
    private Enterprise()
    {
        
    }
    private Enterprise(string name, Owner owner)
    {
        Name = name;
        Owner = owner;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public string Name { get; private set; }
    public DateTime CreateAt { get; private set; }
    public Owner Owner { get; private set; }
    public Guid IdOwner { get;private set; }
    public List<Team> Teams { get; private set; } = new();

    public void AddTeam(Team team)
        => Teams.Add(team);
    
    public static class Factories
    {
        public static Result<Enterprise> Create(string name, Owner owner)
        {
            return Result<Enterprise>.Success(new(name, owner));
        }
    }
}