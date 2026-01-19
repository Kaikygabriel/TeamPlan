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
        IdOwner = owner.Id;
        CreateAt = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public string Name { get; init; }
    public DateTime CreateAt { get; private set; }
    public Owner Owner { get; init; }
    public Guid IdOwner { get;init; }
    public List<Team> Teams { get; private set; } = new();

    public void AddTeam(Team team)
        => Teams.Add(team);
    public void RemoveTeam(Team team)
        => Teams.Remove(team);
    public static class Factories
    {
        public static Result<Enterprise> Create(string name, Owner owner)
        {
            return Result<Enterprise>.Success(new(name, owner));
        }
    }
}